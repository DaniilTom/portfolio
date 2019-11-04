import { Component, Input, OnInit } from "@angular/core";
import { Sight, Review, Type } from "../../model/base.model";
import { DataService } from '../../data/data.service';
import { SightResult } from '../../model/results.model';
import { NgForm } from '@angular/forms';
import { SightService } from '../../data/sights-data.service';
import { TypeService } from '../../data/types-data.service';

@Component({
    selector: 'detail-comp',
    templateUrl: './detail.component.html'
})
export class DetailComponent {

    isReadOnly = true;
    renderDetail = false;
    sight: Sight;
    reviews: Review[] = [];

    constructor(private sightService: SightService, private typeService: TypeService) {
        typeService.getTypes().then((data: Type[]) => this.types = data);
    }

    @Input() set Sight(_sight: Sight) {
        this.sight = _sight;
    }

    types: Type[];

    prepareImgPreview($event) {
        var input = $event.target;
        var reader = new FileReader();

        reader.onloadend = function (e) {
            var imgObj = document.images.namedItem('preview');
            imgObj.src = reader.result.toString();
        }

        reader.readAsDataURL(input.files[0]);
    }

    switchEditBlock() {
        this.isReadOnly = !this.isReadOnly;
    }

    editSight(ngform: NgForm, id: number) {
        if (ngform.valid) {
            var form = document.forms.namedItem('editForm');
            var formData = new FormData(form);
            this.sightService.editSight(id, formData).then((data: Sight) => {
                if (data != null)
                    alert(`Успешно (id: ${data.id})`);
            });
        }
        else
            alert("Ошибки в форме.")
    }

    deleteSight(id: number) {
        this.sightService.deleteSight(id).then((data: boolean) => {
            if (data)
                alert(`Удалено: ${data})`);
        });
    }
}
