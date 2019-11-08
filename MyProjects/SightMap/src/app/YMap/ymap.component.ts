import { Component, OnInit, Output, EventEmitter, Input } from "@angular/core";
import ymaps from 'ymaps';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';
import { EditCreateBehavior } from './EditCreateBehavior';
import { ShowCollectionBehavior } from './ShowCollectionBehavior';
import { SightFilter } from '../model/filters.model';
import { Sight } from '../model/base.model';

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

    objectManager: any;

    @Output() boundsChanged = new EventEmitter<Bounds>();
    @Output() coordinateChanged = new EventEmitter<Coordinates>();
    @Input() switchEditMode: Subject<boolean>;
    @Input() setCollection: Subject<JSONCollection>;
    @Input() initPoint: Coordinates;

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

        if (this.currentMode != 'showmap') {
            var bindCtr = EditCreateBehavior.bind(null, this);
            this.maps.behavior.storage.add(BehaviorType.createEditBehavior, bindCtr);
            //Включаем поведение
            if (!this.isReadOnly)
                this.myMap.behaviors.enable(BehaviorType.createEditBehavior);
            this.switchEditMode.subscribe((value: boolean) => this.switchEditing(value));

            if (this.initPoint != null) {
                this.myPlacemark = new this.maps.Placemark([this.initPoint.latitude, this.initPoint.longitude]);
                this.myMap.geoObjects.add(this.myPlacemark);
            }
        }
        else {
            this.objectManager = new this.maps.ObjectManager();
            this.myMap.geoObjects.add(this.objectManager);

            this.setCollection.subscribe((collection: JSONCollection) => {
                this.objectManager.removeAll()
                this.objectManager.add(collection);
            });

            var bindCtr = ShowCollectionBehavior.bind(null, this);
            this.maps.behavior.storage.add(BehaviorType.showCollectionBehavior, bindCtr);
            this.myMap.behaviors.enable(BehaviorType.showCollectionBehavior);
        }
    }

    switchEditing(value: boolean) {
        this.isReadOnly = value;
        if (!this.isReadOnly)
            this.myMap.behaviors.enable(BehaviorType.createEditBehavior);
        else
            this.myMap.behaviors.disable(BehaviorType.createEditBehavior);
    }

    private onBoundsChanged(bounds: Bounds) {
        this.boundsChanged.emit(bounds);
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
        public longitude: number) { }
}

export class Bounds {
    constructor(
        public minBounds: Coordinates,
        public maxBounds: Coordinates) { }
}

export class JSONCollection {
    type: string = "FeatureCollection";
    features: YMapJsonSight[] = [];
}

export class YMapJsonSight {
    type: string = "Feature";
    id: number;
    geometry: Geometry;
    properties: Properties;
}

export class Geometry {
    type: string = "Point";
    coordinates: number[];
}

export class Properties {
    hintContent: string;
}

enum BehaviorType {
    createEditBehavior = 'createEditBehavior',
    showCollectionBehavior = 'showCollectionBehavior'
}