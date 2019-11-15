import { Component, OnInit } from '@angular/core';
import { Sight } from "../model/base.model";
import { SightFilter } from '../model/filters.model';
import { SightService } from '../data/sights-data.service';
import 'gasparesganga-jquery-loading-overlay';
import * as $ from 'jquery';
import { ContainerService } from '../data/container.service';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';
import { Mode } from '../YMap/ymap.component';

@Component({
    selector: 'show-comp',
    templateUrl: './show.component.html'
})
export class ShowComponent implements OnInit {

    sightsReady: Subject<Sight[]> = new Subject<Sight[]>();
    selectedSight: Sight = null;
    sightArray: Sight[] = [];

    useMap: boolean = false;

    useCommonFilter: boolean = true;
    useMapFilter: boolean = false;

    commonFilter: SightFilter = new SightFilter();
    mapFilter: SightFilter = new SightFilter();

    constructor(public sightService: SightService, private container: ContainerService, public activeRoute: ActivatedRoute) {
        var currentMode = activeRoute.snapshot.url[0].path;
        if (currentMode == "showmap")
            this.useMap = true;
    }

    pushData(sight: Sight) {
        this.container.set("sight", sight);
    }

    async ngOnInit() {
        this.getSights();
    }

    getSights(filter?: SightFilter) {
        $('#container').LoadingOverlay("show");
        this.sightService.getSights(filter).then((value: Sight[]) => {
            this.sightArray = value; $('#container').LoadingOverlay("hide");
            this.sightsReady.next(this.sightArray);
        });
    }

    getDataFromCommonFilter(filter?: SightFilter) {
        this.commonFilter = filter;

        let newFilter = new SightFilter();
        newFilter.id = this.commonFilter.id;
        newFilter.name = this.commonFilter.name;
        newFilter.sightTypeId = this.commonFilter.sightTypeId;
        // newFilter.size = this.commonFilter.size;
        // newFilter.offset = this.commonFilter.offset;
        newFilter.createBeforeDate = this.commonFilter.createBeforeDate;
        newFilter.createAfterDate = this.commonFilter.createAfterDate;
        newFilter.updateBeforeDate = this.commonFilter.createBeforeDate;
        newFilter.updateAfterDate = this.commonFilter.createAfterDate;

        if (this.useMapFilter) {
            newFilter.latitudeMax = this.mapFilter.latitudeMax;
            newFilter.latitudeMin = this.mapFilter.latitudeMin;
            newFilter.longitudeMax = this.mapFilter.longitudeMax;
            newFilter.longitudeMin = this.mapFilter.longitudeMin;

            newFilter.offset = 0;
            newFilter.size = 0;
        }

        console.log("Фильтр получен:");
        console.dir(newFilter);

        this.getSights(newFilter);
    }

    getDataFromMap(filter?: SightFilter) {
        this.mapFilter = filter;

        let newFilter = new SightFilter();
        newFilter.latitudeMax = this.mapFilter.latitudeMax;
        newFilter.latitudeMin = this.mapFilter.latitudeMin;
        newFilter.longitudeMax = this.mapFilter.longitudeMax;
        newFilter.longitudeMin = this.mapFilter.longitudeMin;

        if (this.useCommonFilter) {
            newFilter.id = this.commonFilter.id;
            newFilter.name = this.commonFilter.name;
            newFilter.sightTypeId = this.commonFilter.sightTypeId;
            // filter.size = this.commonFilter.size;
            // filter.offset = this.commonFilter.offset;
            newFilter.createBeforeDate = this.commonFilter.createBeforeDate;
            newFilter.createAfterDate = this.commonFilter.createAfterDate;
            newFilter.updateBeforeDate = this.commonFilter.createBeforeDate;
            newFilter.updateAfterDate = this.commonFilter.createAfterDate;
        }
        this.getSights(newFilter);
    }

    // cloneFilter(filter: SightFilter): SightFilter{
    //     var newFilter = new SightFilter();

    //     newFilter.id = this.commonFilter.id;
    //     newFilter.name = this.commonFilter.name;
    //     newFilter.sightTypeId = this.commonFilter.sightTypeId;
    //     // newFilter.size = this.commonFilter.size;
    //     // newFilter.offset = this.commonFilter.offset;
    //     newFilter.createBeforeDate = this.commonFilter.createBeforeDate;
    //     newFilter.createAfterDate = this.commonFilter.createAfterDate;
    //     newFilter.updateBeforeDate = this.commonFilter.createBeforeDate;
    //     newFilter.updateAfterDate = this.commonFilter.createAfterDate;
    //     newFilter.latitudeMax = this.mapFilter.latitudeMax;
    //     newFilter.latitudeMin = this.mapFilter.latitudeMin;
    //     newFilter.longitudeMax = this.mapFilter.longitudeMax;
    //     newFilter.longitudeMin = this.mapFilter.longitudeMin;

    //     return newFilter;
    // }

}
