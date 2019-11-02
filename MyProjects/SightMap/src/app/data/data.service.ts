import { Sight, Type } from '../model/base.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SightResult, TypeResult } from '../model/results.model';

@Injectable()
export class DataService {

    private basePath: string = "http://localhost:52208/";

    private apiSights: string = "api/sights";
    private apiSightTypes: string = "api/sighttypes";
    private apiReviews: string = "api/reviews";


    //public sightsData: ISight[] = [{ Id: 1, Name: "Sight #1", ShortDescription: "Short Desc #1", SightTypeId: 1 },
    //{ Id: 2, Name: "Sight #2", ShortDescription: "Short Desc #2", SightTypeId: 2 },
    //{ Id: 3, Name: "Sight #3", ShortDescription: "Short Desc #3", SightTypeId: 3 }];

    //private typesData: IType[] = [{ Id: 1, Name: "Type #1" },
    //{ Id: 2, Name: "Test #2" },
    //{ Id: 3, Name: "Test #3" }];

    typeResult: TypeResult = new TypeResult();

    constructor(private client: HttpClient) {
        this.SyncLoad();
    }

    private async SyncLoad() {
        this.typeResult = await this.getTypesFromServer().toPromise();
    }

    getSightsFromServer(): Observable<SightResult> {
        return this.client.get<SightResult>(this.basePath + this.apiSights);
    }

    getTypesFromServer(): Observable<TypeResult> {
        return this.client.get<TypeResult>(this.basePath + this.apiSightTypes);
    }

    changeSight(_sight: Sight) {

    }
}
