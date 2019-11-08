import { Component } from "@angular/core";
import { Sight, Type } from '../model/base.model';
import { NgForm } from '@angular/forms';
import { TypeService } from '../data/types-data.service';
import { SightService } from '../data/sights-data.service';
import { Coordinates } from '../YMap/ymap.component';

@Component(
    {
        selector: 'create-comp',
        templateUrl: './create.component.html'
    })
export class CreateComponent {

    newSight: Sight = new Sight();
    newType: Type = new Type();

    types: Type[];

    constructor(public sightService: SightService, public typeService: TypeService) {
        this.newSight.type = new Type();
        typeService.getTypes().then((data: Type[]) => this.types = data);
    }

    prepareImgPreview($event) {
        var input = $event.target;
        var reader = new FileReader();

        reader.onloadend = function (e) {
            var imgObj = document.images.namedItem('preview');
            imgObj.src = reader.result.toString();
        }

        reader.readAsDataURL(input.files[0]);
    }

    addSight(ngform: NgForm) {
        if (ngform.valid) {
            var form = document.forms.namedItem('createSight');
            var formData = new FormData(form);
            this.sightService.addSight(formData).then((data: Sight) => {
                if (data != null)
                    alert("Добавлено с id:" + data.id);
            });
            ngform.resetForm();
        }
        else
            alert("Ошибки в форме.")
    }

    addType(ngform: NgForm) {
        if (ngform.valid) {
            var form = document.forms.namedItem('createType');
            var formData = new FormData(form);
            this.typeService.addType(formData).then((data: Type) =>
            {
                if (data != null) {
                    alert("Добавлено с id:" + data.id);
                    this.types.push(data);
                }
            });
        }
        else
            alert("Ошибки в форме.")
    }

    setCoordinates(coord: Coordinates){
        this.newSight.latitude = coord.latitude;
        this.newSight.longitude = coord.longitude;
    }
}
