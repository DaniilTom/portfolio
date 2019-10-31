import { Sight } from '../model/sight.model';
import { Type } from '../model/type.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class DataService {

    private basePath: string = "http://localhost:52208/";

    private apiSights: string = "api/sights";
    private apiSightTypes: string = "api/sighttypes";
    private apiReviews: string = "api/reviews";


    private sightsData: Sight[] = [ { Id: 1, Name: "Sight #1", ShortDescription: "Short Desc #1", SightTypeId: 1 },
                                { Id: 2, Name: "Sight #2", ShortDescription: "Short Desc #2", SightTypeId: 2 },
                                { Id: 3, Name: "Sight #3", ShortDescription: "Short Desc #3", SightTypeId: 3 }];

    private typesData: Type[] = [   { Id: 1, Name: "Type #1" },
                                { Id: 2, Name: "Test #2" },
        { Id: 3, Name: "Test #3" }];

    constructor(private client: HttpClient) { }

    getSights() : Sight[] {
        return this.sightsData;
    }

    getTypes() : Type[] {
        return this.typesData;
    }

    getSightsFromServe(): Observable<Sight[]> {
        return this.client.get<Sight[]>(this.basePath + this.apiSights);
    }
}
