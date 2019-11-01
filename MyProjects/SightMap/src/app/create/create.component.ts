import { Component } from "@angular/core";
import { DataService, ISight } from '../data/data.service';
import { Sight } from '../model/sight.model';

@Component(
    {
        selector: 'create-comp',
        templateUrl: './create.component.html'
    })
export class CreateComponent { 
    constructor(public dataService: DataService) { }
}
