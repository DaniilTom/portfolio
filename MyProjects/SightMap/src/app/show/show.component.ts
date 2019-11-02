import { Component, OnInit } from '@angular/core';
import { DataService } from "../data/data.service";
import { Sight } from "../model/base.model";
import { SightResult, TypeResult } from '../model/results.model';

@Component({
    selector: 'show-comp',
    templateUrl: './show.component.html'
})
export class ShowComponent implements OnInit {
    constructor(public dataservice: DataService) { }

    renderDetail = false;

    selectedSight: Sight = null;

    sightResult: SightResult = new SightResult();
    //typeResult: TypeResult = new TypeResult();

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
        //this.typeResult = await this.dataservice.getTypesFromServer().toPromise();
    }

    getSights() {
        this.dataservice.getSightsFromServer().subscribe((data: SightResult) => this.sightResult = data);
    }

    //getTypes() {
    //    this.dataservice.getTypesFromServer().subscribe((data: TypeResult) => this.typeResult = data);
    //}
}
