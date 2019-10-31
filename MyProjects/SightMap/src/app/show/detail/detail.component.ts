import { Component, Input } from "@angular/core";
import { Sight } from "../../model/sight.model";
import { DataService } from '../../data/data.service';

@Component({
    selector: 'detail-comp',
    templateUrl: './detail.component.html'
})
export class DetailComponent {

    renderDetail = false;
    sight : Sight;

    constructor(private dataService: DataService){}

    @Input() set Sight(_sight)
    {
        this.sight = _sight;

        var types = this.dataService.getTypes();
        for(var i = 0; i < types.length; i++ )
        {
            if(types[i].Id == this.sight.Id)
            {
                this.sight.Type = types[i];
                break;
            }
        }
    }
}
