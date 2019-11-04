import { Component, OnInit } from '@angular/core';
import { DataService } from "../data/data.service";
import { Sight } from "../model/base.model";
import { SightResult, TypeResult } from '../model/results.model';
import { SightFilter } from '../model/filters.model';
import { SightService } from '../data/sights-data.service';

@Component({
    selector: 'show-comp',
    templateUrl: './show.component.html'
})
export class ShowComponent implements OnInit {
    constructor(public sightService: SightService) { }

    renderDetail = false;
    selectedSight: Sight = null;
    sightArray: Sight[] = [];

    SwitchDetail(_sight) {
        if (_sight != this.selectedSight) {
            this.renderDetail = true;
            this.selectedSight = _sight;
        }
        else {
            this.renderDetail = !this.renderDetail;
        }
    }

    async ngOnInit() {
        this.getSights();
    }

    getSights(filter?: SightFilter) {
        this.sightService.getSights(filter).then((value: Sight[]) => this.sightArray = value);
    }
}
