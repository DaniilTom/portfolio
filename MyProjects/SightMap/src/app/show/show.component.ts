import { Component, OnInit } from '@angular/core';
import { DataService } from "../data/data.service";
import { Sight } from "../model/base.model";
import { SightResult, TypeResult } from '../model/results.model';
import { SightFilter } from '../model/filters.model';

@Component({
    selector: 'show-comp',
    templateUrl: './show.component.html'
})
export class ShowComponent implements OnInit {
    constructor(public dataservice: DataService) { }

    renderDetail = false;
    selectedSight: Sight = null;
    sightResult: SightResult = new SightResult();

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
        this.sightResult = await this.dataservice.getSightsFromServer().toPromise();
    }

    getSights(filter?: SightFilter) {
        this.dataservice.getSightsFromServer(filter).subscribe((data: SightResult) => this.sightResult = data);
    }

    updateSights(filter: SightFilter) {
        this.getSights(filter);
    }
}
