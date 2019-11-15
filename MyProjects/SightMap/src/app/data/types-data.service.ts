import { Injectable } from '@angular/core';
import { ResultState } from '../model/results.model';
import { TypeFilter } from '../model/filters.model';
import { DataService } from './data.service';
import { Type } from '../model/base.model';

@Injectable()
export class TypeService {

    private basePath: string = "http://localhost:52208/";

    // private apiSightTypes: string = this.basePath + "api/sighttypes/";
    private apiSightTypes: string = "api/sighttypes/";

    getResult: ResultState<Type[]> = new ResultState();
    addResult: ResultState<Type> = new ResultState();
    editResult: ResultState<Type> = new ResultState();
    deleteResult: ResultState<boolean> = new ResultState();

    types: Type[] = [];

    constructor(public dataService: DataService) { }

    async getTypes(filter?: TypeFilter): Promise<Type[]> {

        if (filter == undefined || filter == null) {
            filter = new TypeFilter();
        }

        this.getResult = await this.dataService.getItems<ResultState<Type[]>>(this.apiSightTypes, filter).toPromise();
        if (!this.getResult.isSuccess)
            alert(this.getResult.message);

        return this.getResult.value;
    }

    async addType(type: Type): Promise<Type> {

        this.addResult = await this.dataService.addItem<ResultState<Type>>(this.apiSightTypes, type).toPromise();
        if (!this.addResult.isSuccess)
            alert(this.addResult.message);

        return this.addResult.value;
    }

    async editType(id: number, type: Type): Promise<Type> {
        this.editResult = await this.dataService.editItem<ResultState<Type>>(this.apiSightTypes, type).toPromise();
        if (!this.editResult.isSuccess)
            alert(this.editResult.message);

        return this.editResult.value;
    }

    async deleteType(id: number): Promise<boolean> {
        this.deleteResult = await this.dataService.deleteItem<ResultState<boolean>>(this.apiSightTypes, id).toPromise();
        if (!this.deleteResult.isSuccess)
            alert(this.deleteResult.message);

        return this.deleteResult.value;
    }

    async getTypesCount(filter?: TypeFilter): Promise<number>{

        if (filter == undefined || filter == null) {
            filter = new TypeFilter();
        }

        var getResult = await this.dataService.getCount<ResultState<number>>(this.apiSightTypes, filter).toPromise();
        if (!getResult.isSuccess)
            alert(getResult.message);

        return getResult.value;
    }
}
