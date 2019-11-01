import { Component, Input, OnInit } from "@angular/core";
import { Sight } from "../../model/sight.model";
import { DataService, ISight, IType, ITypeResult } from '../../data/data.service';

@Component({
    selector: 'detail-comp',
    templateUrl: './detail.component.html'
})
export class DetailComponent implements OnInit {

    isReadOnly = true;
    renderDetail = false;
    sight: ISight;
    typeResult: ITypeResult;

    ngOnInit(): void { }

    constructor(private dataService: DataService)
    {
        this.load();
    }

    private async load() {
        this.typeResult = await this.dataService.getTypesFromServer().toPromise();
    }

    @Input() set Sight(_sight)
    {
        this.sight = _sight;
    }

    switchEditBlock() {
        this.isReadOnly = !this.isReadOnly;
    }

    applyChanges(_sight: ISight) {
        this.dataService.changeSight(this.sight);
    }
}
