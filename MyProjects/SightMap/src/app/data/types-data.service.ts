import { Injectable } from '@angular/core';
import { TypeResult } from '../model/results.model';
import { TypeFilter } from '../model/filters.model';
import { DataService } from './data.service';
import { Type } from '../model/base.model';

@Injectable()
export class TypeService {

    private basePath: string = "http://localhost:52208/";

    private apiSightTypes: string = this.basePath + "api/sighttypes/";

    getResult: TypeResult<Type[]> = new TypeResult();
    addResult: TypeResult<Type> = new TypeResult();
    editResult: TypeResult<Type> = new TypeResult();
    deleteResult: TypeResult<boolean> = new TypeResult();

    types: Type[] = [];

    constructor(public dataService: DataService) { }

    async getTypes(filter?: TypeFilter): Promise<Type[]> {

        if (filter == undefined || filter == null) {
            filter = new TypeFilter();
        }

        this.getResult = await this.dataService.getItems<TypeResult<Type[]>>(this.apiSightTypes, filter).toPromise();
        if (!this.getResult.isSuccess)
            alert(this.getResult.message);

        return this.getResult.value;
    }

    async addType(form: FormData): Promise<Type> {

        this.addResult = await this.dataService.addItem<TypeResult<Type>>(this.apiSightTypes, form).toPromise();
        if (!this.addResult.isSuccess)
            alert(this.addResult.message);

        return this.addResult.value;
    }

    async editType(id: number, form: FormData): Promise<Type> {
        this.editResult = await this.dataService.editItem<TypeResult<Type>>(this.apiSightTypes, id, form).toPromise();
        if (!this.editResult.isSuccess)
            alert(this.editResult.message);

        return this.editResult.value;
    }

    async deleteType(id: number): Promise<boolean> {
        this.deleteResult = await this.dataService.deleteItem<TypeResult<boolean>>(this.apiSightTypes, id).toPromise();
        if (!this.deleteResult.isSuccess)
            alert(this.deleteResult.message);

        return this.deleteResult.value;
    }
}
