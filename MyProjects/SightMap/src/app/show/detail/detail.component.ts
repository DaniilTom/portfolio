import { Component, Input, OnInit } from "@angular/core";
import { Sight } from "../../model/base.model";
import { DataService } from '../../data/data.service';
import { TypeResult } from '../../model/results.model';

@Component({
    selector: 'detail-comp',
    templateUrl: './detail.component.html'
})
export class DetailComponent implements OnInit {

    isReadOnly = true;
    renderDetail = false;
    sight: Sight;
    typeResult: TypeResult = new TypeResult();

    ngOnInit(): void { }

    constructor(private dataService: DataService)
    {
        this.load();
    }

    private async load() {
        this.typeResult = await this.dataService.getTypesFromServer().toPromise();
    }

    @Input() set Sight(_sight: Sight)
    {
        this.sight = _sight;
    }

    switchEditBlock() {
        this.isReadOnly = !this.isReadOnly;
    }

    applyChanges(_sight: Sight) {
        this.dataService.addSight(this.sight);
    }
}
