import { Component, OnInit, Output, EventEmitter, Input } from "@angular/core";
import ymaps from 'ymaps';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';

// import * as ymaps from 'yandex-maps';
//import { IBehavior, Map } from 'yandex-maps';


@Component({
    selector: 'yan-map',
    templateUrl: './ymap.component.html',
    styleUrls: ['./ymap.component.css']
})
export class YMapComponent implements OnInit {

    @Input() isReadOnly: boolean = false;

    maps: any;
    myMap: any;

    myPlacemark: any;

    coordinates: Coordinates = new Coordinates(0, 0);

    @Output() coordinateChanged = new EventEmitter<Coordinates>();
    @Input() switchEditMode:Subject<boolean>;

    constructor(public activeRoute: ActivatedRoute) {
        var currentMode = activeRoute.snapshot.url[0].path;
        if (currentMode != "create")
            this.isReadOnly = true;
    }

    async ngOnInit() {
        //await ymaps.ready();
        this.maps = await ymaps.load();
        this.myMap = new this.maps.Map('map', {
            center: [59.939095, 30.315868],
            zoom: 7
        });

        var bindCtr = EditCreateBehavior.bind(null, this);

        this.maps.behavior.storage.add('createEditBehavior', bindCtr);
        //Включаем поведение
        if (!this.isReadOnly)
            this.myMap.behaviors.enable('createEditBehavior');

        this.switchEditMode.subscribe((value: boolean) => this.switchEditing(value));    
    }
    

    switchEditing(value: boolean) {
        this.isReadOnly = value;
        if (!this.isReadOnly)
            this.myMap.behaviors.enable('createEditBehavior');
        else
            this.myMap.behaviors.disable('createEditBehavior');
    }

    private setCoordinates(lt, lg) {
        this.coordinates.latitude = lt;
        this.coordinates.longitude = lg;
        this.coordinateChanged.emit(new Coordinates(lt, lg));

        this.myMap.geoObjects.remove(this.myPlacemark);
        this.myPlacemark = new this.maps.Placemark([lt, lg]);
        this.myMap.geoObjects.add(this.myPlacemark);
    }
}

export class Coordinates {
    constructor(
        public latitude: number,
        public longitude: number
    ) { }
}



function EditCreateBehavior(yMapComp) {
    // Определим свойства класса
    this.yMapComp = yMapComp;
    this.options = new window.ymaps.option.Manager(); // Менеджер опций
    this.events = new window.ymaps.event.Manager(); // Менеджер событий
}

// Определим методы.
EditCreateBehavior.prototype = {
    constructor: EditCreateBehavior,
    // Когда поведение будет включено, добавится событие щелчка на карту
    enable: function () {
        /*
        this._parent - родителем для поведения является менеджер поведений;
        this._parent.getMap() - получаем ссылку на карту;
        this._parent.getMap().events.add - добавляем слушатель события на карту.
        */
        this._parent.getMap().events.add('click', this._onClick, this);
    },
    disable: function () {
        this._parent.getMap().events.remove('click', this._onClick, this);
    },
    // Устанавливает родителя для исходного поведения.
    setParent: function (parent) { this._parent = parent; },
    // Получает родителя
    getParent: function () { return this._parent; },
    // При щелчке на карте происходит ее центрирование по месту клика.
    _onClick: function (e) {
        //alert(this.coord.latitude);
        var coords = e.get('coords');
        this.yMapComp.setCoordinates(coords[0], coords[1]);
    }
}