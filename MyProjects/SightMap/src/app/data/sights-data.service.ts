import { Injectable } from '@angular/core';
import { ResultState } from '../model/results.model';
import { SightFilter } from '../model/filters.model';
import { DataService } from './data.service';
import { Sight } from '../model/base.model';

@Injectable()
export class SightService {

    private basePath: string = "http://localhost:52208/";

    private apiSights: string = this.basePath + "api/sights/";

    getResult: ResultState<Sight[]> = new ResultState();
    addResult: ResultState<Sight> = new ResultState();
    editResult: ResultState<Sight> = new ResultState();
    deleteResult: ResultState<boolean> = new ResultState();

    sights: Sight[] = [];

    constructor(public dataService: DataService) { }

    async getSights(filter?: SightFilter): Promise<Sight[]>{

        if (filter == undefined || filter == null) {
            filter = new SightFilter();
        }

        this.getResult = await this.dataService.getItems<ResultState<Sight[]>>(this.apiSights, filter).toPromise();
        if (!this.getResult.isSuccess)
            alert(this.getResult.message);

        return this.getResult.value;
    }

    async addSight(form: FormData): Promise<Sight> {

        this.addResult = await this.dataService.addItem<ResultState<Sight>>(this.apiSights, form).toPromise();
        if (!this.addResult.isSuccess)
            alert(this.addResult.message);

        return this.addResult.value;
    }

    async editSight(id: number, form: FormData): Promise<Sight> {
        this.editResult = await this.dataService.editItem<ResultState<Sight>>(this.apiSights, id, form).toPromise();
        if (!this.editResult.isSuccess)
            alert(this.editResult.message);

        return this.editResult.value;
    }

    async deleteSight(id: number): Promise<boolean> {
        this.deleteResult = await this.dataService.deleteItem<ResultState<boolean>>(this.apiSights, id).toPromise();
        if (!this.deleteResult.isSuccess)
            alert(this.deleteResult.message);

        return this.deleteResult.value;
    }

    async getSightsCount(filter?: SightFilter): Promise<number>{

        if (filter == undefined || filter == null) {
            filter = new SightFilter();
        }

        var getResult = await this.dataService.getCount<ResultState<number>>(this.apiSights, filter).toPromise();
        if (!getResult.isSuccess)
            alert(getResult.message);

        return getResult.value;
    }
}
