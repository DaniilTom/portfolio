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

    prepareImgPrevie($event) {
        var input = $event.target;
        var reader = new FileReader();

        reader.onloadend = function (e) {
            var imgObj = document.images.namedItem('preview');
            imgObj.src = reader.result.toString();
            imgObj.removeAttribute('hidden');
        }

        reader.readAsDataURL(input.files[0]);
    }

    addNewSight(sight: Sight) {
        //alert(sight.name);
        this.dataService.addSight();
    }
}
