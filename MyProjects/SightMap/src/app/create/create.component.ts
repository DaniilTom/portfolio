import { Component } from "@angular/core";
import { Sight, Type } from '../model/base.model';
import { NgForm } from '@angular/forms';
import { TypeService } from '../data/types-data.service';
import { SightService } from '../data/sights-data.service';
import { Coordinates } from '../YMap/ymap.component';
import { Subject } from 'rxjs';

@Component(
    {
        selector: 'create-comp',
        templateUrl: './create.component.html',
        styleUrls: ['./create.component.css']
    })
export class CreateComponent {

    removeInitPoint: Subject<void> = new Subject<void>();
    beginUpload: Subject<void> = new Subject<void>();

    newSight: Sight = new Sight();
    newType: Type = new Type();

    referenceId: number;

    types: Type[];

    constructor(public sightService: SightService, public typeService: TypeService) {
        this.newSight.type = new Type();
        typeService.getTypes().then((data: Type[]) => this.types = data);
        this.referenceId = Math.floor((Math.random() * 999999999) + 1);
    }

    addSight(ngform: NgForm) {
        if (ngform.valid) {
            var form = document.forms.namedItem('createSight');
            var formData = new FormData(form);
            this.sightService.addSight(this.newSight).then((data: Sight) => {
                if (data != null)
                    alert("Добавлено с id:" + data.id);
                ngform.resetForm();
                this.removeInitPoint.next();
            });

        }
        else
            alert("Ошибки в форме.")
    }

    addType(ngform: NgForm) {
        if (ngform.valid) {
            var form = document.forms.namedItem('createType');
            var formData = new FormData(form);
            this.typeService.addType(formData).then((data: Type) => {
                if (data != null) {
                    alert("Добавлено с id:" + data.id);
                    this.types.push(data);
                }
            });
        }
        else
            alert("Ошибки в форме.")
    }

    // метод подписки на событие карты coordinateChanged
    setCoordinates(coord: Coordinates) {
        this.newSight.latitude = coord.latitude;
        this.newSight.longitude = coord.longitude;
    }
}
