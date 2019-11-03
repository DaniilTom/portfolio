import { Component } from "@angular/core";
import { DataService } from '../data/data.service';
import { Sight, Type } from '../model/base.model';
import { NgForm } from '@angular/forms';
import { SightResult, TypeResult } from '../model/results.model';

@Component(
    {
        selector: 'create-comp',
        templateUrl: './create.component.html'
    })
export class CreateComponent {

    newSight: Sight = new Sight();
    newType: Type = new Type();

    constructor(public dataService: DataService) { this.newSight.type = new Type(); }

    prepareImgPreview($event) {
        var input = $event.target;
        var reader = new FileReader();

        reader.onloadend = function (e) {
            var imgObj = document.images.namedItem('preview');
            imgObj.src = reader.result.toString();
            imgObj.removeAttribute('hidden');
        }

        reader.readAsDataURL(input.files[0]);
    }

    addNewSight(form: NgForm) {
        if (form.valid)
            this.dataService.addSight().subscribe((data: SightResult) => alert(data.isSuccess));
        else
            alert("Ошибки в форме.")
    }

    addNewType(form: NgForm) {
        if (form.valid)
            this.dataService.addType().subscribe((data: TypeResult) => alert(data.isSuccess));
        else
            alert("Ошибки в форме.")
    }
}
