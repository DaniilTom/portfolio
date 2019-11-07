import { Component, OnInit } from '@angular/core';
import { Sight } from "../model/base.model";
import { SightFilter } from '../model/filters.model';
import { SightService } from '../data/sights-data.service';
import 'gasparesganga-jquery-loading-overlay';
import * as $ from 'jquery';
import { ContainerService } from '../data/container.service';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'show-comp',
    templateUrl: './show.component.html'
})
export class ShowComponent implements OnInit {


    selectedSight: Sight = null;
    sightArray: Sight[] = [];

    useMap: boolean = false;

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
        this.sightService.getSights(filter).then((value: Sight[]) => { this.sightArray = value; $('#container').LoadingOverlay("hide"); });
    }
}
