import { Component, OnInit, Output, EventEmitter, Input } from "@angular/core";
import ymaps from 'ymaps';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';
import { EditCreateBehavior } from './EditCreateBehavior';

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

    currentMode: string;

    @Output() coordinateChanged = new EventEmitter<Coordinates>();
    @Input() switchEditMode:Subject<boolean>;

    constructor(public activeRoute: ActivatedRoute) {
        this.currentMode = activeRoute.snapshot.url[0].path;
        if (this.currentMode != "create")
            this.isReadOnly = true;
    }

    async ngOnInit() {
        //await ymaps.ready();
        this.maps = await ymaps.load();
        this.myMap = new this.maps.Map('map', {
            center: [59.939095, 30.315868],
            zoom: 7
        });

        if(this.currentMode != 'showmap')
        {
            var bindCtr = EditCreateBehavior.bind(null, this);
            this.maps.behavior.storage.add('createEditBehavior', bindCtr);
            //Включаем поведение
            if (!this.isReadOnly)
            this.myMap.behaviors.enable('createEditBehavior');
            this.switchEditMode.subscribe((value: boolean) => this.switchEditing(value));    
        }
    }
    

    switchEditing(value: boolean) {
        this.isReadOnly = value;
        if (!this.isReadOnly)
            this.myMap.behaviors.enable('createEditBehavior');
        else
            this.myMap.behaviors.disable('createEditBehavior');
    }

        // вызывается в поведении EditCreateBehavior
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