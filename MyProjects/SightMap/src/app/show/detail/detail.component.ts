import { Component, Input, OnInit } from "@angular/core";
import { Sight } from "../../model/base.model";
import { DataService } from '../../data/data.service';
import { TypeResult, SightResult } from '../../model/results.model';
import { NgForm } from '@angular/forms';

@Component({
    selector: 'detail-comp',
    templateUrl: './detail.component.html'
})
export class DetailComponent {

    isReadOnly = true;
    renderDetail = false;
    sight: Sight;

    constructor(private dataService: DataService) { }

    @Input() set Sight(_sight: Sight) {
        this.sight = _sight;
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



    switchEditBlock() {
        this.isReadOnly = !this.isReadOnly;
    }

    updateSight(form: NgForm, id: number) {
        if (form.valid) {
            this.switchEditBlock();
            this.dataService.updateSight(id).subscribe((data: SightResult) => alert(data.isSuccess));
        }
        else
            alert("Ошибки в форме.");
    }

    deleteSight(id: number) {
        this.dataService.deleteSight(id).subscribe((data: SightResult) => alert(data.isSuccess));
    }
}
