import { Component, OnInit } from "@angular/core";
import { DataService, ISight, ISightResult, ITypeResult } from "../data/data.service";
import { Sight } from "../model/sight.model";
import { SightResult } from '../model/sightresult.model';

@Component({
    selector: 'show-comp',
    templateUrl: './show.component.html'
})
export class ShowComponent implements OnInit {
    constructor(public dataservice: DataService) { }

    renderDetail = false;

    selectedSight: ISight = null;

    resultSights: ISightResult;
    resultTypes: ITypeResult;

    SwitchDetail(_sight) {
        if (_sight != this.selectedSight) {
            this.renderDetail = true;
            this.selectedSight = _sight;
        }
        else {
            this.renderDetail = !this.renderDetail;
        }
    }

    ngOnInit() {
        this.getSights();
        this.getTypes();
    }

    getSights() {
        this.dataservice.getSightsFromServer().subscribe((data: ISightResult) => this.resultSights = data);
    }

    getTypes() {
        this.dataservice.getTypesFromServer().subscribe((data: ITypeResult) => this.resultTypes = data);
    }
}
