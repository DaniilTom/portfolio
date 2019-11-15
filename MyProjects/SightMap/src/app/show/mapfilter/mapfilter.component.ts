import { Component, Output, EventEmitter, Input, OnInit } from '@angular/core';
import { SightFilter } from '../../model/filters.model';
import { TypeService } from '../../data/types-data.service';
import { Sight } from '../../model/base.model'
import { Bounds, JSONCollection, YMapJsonSight, Geometry, Properties, Mode } from 'src/app/YMap/ymap.component';
import { Subject } from 'rxjs';

@Component({
    selector: 'mapfilter-comp',
    templateUrl: './mapfilter.component.html'
})
export class MapFilterComponent implements OnInit {

    filter: SightFilter = new SightFilter();
    sights: Sight[];
    currentCollection: JSONCollection;

    mode: Mode = Mode.Filter;

    @Output() onApplyFilter = new EventEmitter<SightFilter>();
    @Input() acceptorSights: Subject<Sight[]>;
    setCollection: Subject<JSONCollection> = new Subject<JSONCollection>();

    constructor() { }

    ngOnInit() {
        this.acceptorSights.subscribe( (sights: Sight[]) =>
        {
            this.sights = sights;
            this.currentCollection = this.serializeSights(this.sights);
            this.setCollection.next(this.currentCollection);
        });

        this.filter = new SightFilter();
    }

    applyFilter(bounds: Bounds) {
        this.filter.latitudeMin = bounds.minBounds.latitude;
        this.filter.longitudeMin = bounds.minBounds.longitude;
        this.filter.latitudeMax = bounds.maxBounds.latitude;
        this.filter.longitudeMax = bounds.maxBounds.longitude;
        console.dir(this.filter);
        this.onApplyFilter.emit(this.filter);
    }

    serializeSights(sights: Sight[]) {
        var collection = new JSONCollection();
        for (var i = 0; i < sights.length; i++) {
            let temp = new YMapJsonSight();
            temp.id = sights[i].id;

            var geom = new Geometry();
            geom.coordinates = [sights[i].latitude, sights[i].longitude];
            temp.geometry = geom;

            var prop = new Properties();
            prop.hintContent = sights[i].name;
            temp.properties = prop;

            collection.features.push(temp);
        }
        return collection;
    }
}
