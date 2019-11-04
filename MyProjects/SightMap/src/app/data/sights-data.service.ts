import { Injectable } from '@angular/core';
import { SightResult } from '../model/results.model';
import { SightFilter } from '../model/filters.model';
import { DataService } from './data.service';
import { Sight } from '../model/base.model';

@Injectable()
export class SightService {

    private basePath: string = "http://localhost:52208/";

    private apiSights: string = this.basePath + "api/sights/";

    getResult: SightResult<Sight[]> = new SightResult();
    addResult: SightResult<Sight> = new SightResult();
    editResult: SightResult<Sight> = new SightResult();
    deleteResult: SightResult<boolean> = new SightResult();

    sights: Sight[] = [];

    constructor(public dataService: DataService) { }

    async getSights(filter?: SightFilter): Promise<Sight[]>{

        if (filter == undefined || filter == null) {
            filter = new SightFilter();
        }

        this.getResult = await this.dataService.getItems<SightResult<Sight[]>>(this.apiSights, filter).toPromise();
        if (!this.getResult.isSuccess)
            alert(this.getResult.message);

        return this.getResult.value;
    }

    async addSight(form: FormData): Promise<Sight> {

        this.addResult = await this.dataService.addItem<SightResult<Sight>>(this.apiSights, form).toPromise();
        if (!this.addResult.isSuccess)
            alert(this.addResult.message);

        return this.addResult.value;
    }

    async editSight(id: number, form: FormData): Promise<Sight> {
        this.editResult = await this.dataService.editItem<SightResult<Sight>>(this.apiSights, id, form).toPromise();
        if (!this.editResult.isSuccess)
            alert(this.editResult.message);

        return this.editResult.value;
    }

    async deleteSight(id: number): Promise<boolean> {
        this.deleteResult = await this.dataService.deleteItem<SightResult<boolean>>(this.apiSights, id).toPromise();
        if (!this.deleteResult.isSuccess)
            alert(this.deleteResult.message);

        return this.deleteResult.value;
    }
}
