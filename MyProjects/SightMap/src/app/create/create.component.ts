import { Component } from "@angular/core";
import { DataService } from '../data/data.service';
import { Sight } from '../model/base.model';

@Component(
    {
        selector: 'create-comp',
        templateUrl: './create.component.html'
    })
export class CreateComponent { 
    constructor(public dataService: DataService) { }
}
