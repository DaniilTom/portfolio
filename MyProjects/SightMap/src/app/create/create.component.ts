import { Component } from "@angular/core";
import { DataService } from '../data/data.service';

@Component(
    {
        selector: 'create-comp',
        templateUrl: './create.component.html'
    })
export class CreateComponent { 
    constructor(public dataService: DataService) {}
}
