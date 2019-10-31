import { Component } from "@angular/core";
import { DataService } from "../data/data.service";
import { Sight } from "../model/sight.model";

@Component({
    selector: 'show-comp',
    templateUrl: './show.component.html'
})
export class ShowComponent {
    constructor(public dataservice: DataService){}

    renderDetail = false;

    selectedSight : Sight = null;

    SwitchDetail(_sight)
    {
        if(_sight != this.selectedSight)
        {
            this.renderDetail = true;
            this.selectedSight = _sight;
        }
        else
        {
            this.renderDetail = !this.renderDetail;
        }
    }
}
