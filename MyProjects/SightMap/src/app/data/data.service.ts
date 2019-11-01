import { Sight } from '../model/sight.model';
import { Type } from '../model/type.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SightResult } from '../model/sightresult.model';

@Injectable()
export class DataService {

    private basePath: string = "http://localhost:52208/";

    private apiSights: string = "api/sights";
    private apiSightTypes: string = "api/sighttypes";
    private apiReviews: string = "api/reviews";


    public sightsData: ISight[] = [{ Id: 1, Name: "Sight #1", ShortDescription: "Short Desc #1", SightTypeId: 1 },
    { Id: 2, Name: "Sight #2", ShortDescription: "Short Desc #2", SightTypeId: 2 },
    { Id: 3, Name: "Sight #3", ShortDescription: "Short Desc #3", SightTypeId: 3 }];

    private typesData: IType[] = [{ Id: 1, Name: "Type #1" },
    { Id: 2, Name: "Test #2" },
    { Id: 3, Name: "Test #3" }];

    typeResult: ITypeResult;

    constructor(private client: HttpClient) {
        this.SyncLoad();
    }

    private async SyncLoad() {
        this.typeResult = await this.getTypesFromServer().toPromise();
    }

    getSightsFromServer(): Observable<ISightResult> {
        return this.client.get<ISightResult>(this.basePath + this.apiSights);
    }

    getTypesFromServer(): Observable<ITypeResult> {
        return this.client.get<ITypeResult>(this.basePath + this.apiSightTypes);
    }

    changeSight(_sight: ISight) {

    }
}

export interface ISight {
    Id?: number;
    Name?: string;
    FullDescription?: string;
    ShortDescription?: string;
    AuthorId?: number;
    CreateDate?: Date;
    UpdateDate?: Date;
    SightTypeId?: number;
    Type?: IType;
}

export interface IType {
    Id?: number;
    Name?: string;
}

export interface ISightResult {
    IsSuccess?: boolean,
    Value?: ISight[],
    Message?: string
}

export interface ITypeResult {
    IsSuccess?: boolean,
    Value?: IType[],
    Message?: string
}
