import { Component, Output, EventEmitter, Input, OnInit } from '@angular/core';
import { SightFilter } from '../../model/filters.model';
import { TypeService } from '../../data/types-data.service';
import { Sight } from '../../model/base.model'
import { Bounds, JSONCollection, YMapJsonSight, Geometry, Properties } from 'src/app/YMap/ymap.component';
import { Subject } from 'rxjs';

@Component({
    selector: 'mapfilter-comp',
    templateUrl: './mapfilter.component.html'
})
export class MapFilterComponent implements OnInit {

    filter: SightFilter = new SightFilter();
    sights: Sight[];
    currentCollection: JSONCollection;

    @Output() onApplyFilter = new EventEmitter<SightFilter>();
    @Input() acceptorSights: Subject<Sight[]>;
    setCollection: Subject<JSONCollection> = new Subject<JSONCollection>();

    constructor() { }

    ngOnInit() {
        this.acceptorSights.subscribe( (sights: Sight[]) =>
        {
            this.sights = sights;
            this.serializeSights();
            this.setCollection.next(this.currentCollection);
        });
    }

    applyFilter(bounds: Bounds) {
        this.filter.latitudeMin = bounds.minBounds.latitude;
        this.filter.longitudeMin = bounds.minBounds.longitude;
        this.filter.latitudeMax = bounds.maxBounds.latitude;
        this.filter.longitudeMax = bounds.maxBounds.longitude;
        this.onApplyFilter.emit(this.filter);
    }

    serializeSights() {
        var collection = new JSONCollection();
        for (var i = 0; i < this.sights.length; i++) {
            let temp = new YMapJsonSight();
            temp.id = this.sights[i].id;

            var geom = new Geometry();
            geom.coordinates = [this.sights[i].latitude, this.sights[i].longitude];
            temp.geometry = geom;

            var prop = new Properties();
            prop.hintContent = this.sights[i].name;
            temp.properties = prop;

            collection.features.push(temp);
        }
        this.currentCollection = collection;
        console.dir(collection);
    }
}
