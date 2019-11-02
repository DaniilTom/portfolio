import { Component } from "@angular/core";
import { DataService } from '../data/data.service';
import { Sight, Type } from '../model/base.model';

@Component(
    {
        selector: 'create-comp',
        templateUrl: './create.component.html'
    })
export class CreateComponent {

    newSight: Sight = new Sight();

    constructor(public dataService: DataService) { this.newSight.type = new Type(); }

    addNewSight(sight: Sight) {
        //alert(sight.name);
        this.dataService.addSight(sight);
    }
}
