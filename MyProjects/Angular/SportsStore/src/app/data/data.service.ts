import { Sight } from '../model/sight.model';
import { Type } from '../model/type.model';
import { Injectable } from '@angular/core';

@Injectable()
export class DataService {

    private sightsData: Sight[] = [ { Id: 1, Name: "Sight #1", ShortDescription: "Short Desc #1", SightTypeId: 1 },
                                { Id: 2, Name: "Sight #2", ShortDescription: "Short Desc #2", SightTypeId: 2 },
                                { Id: 3, Name: "Sight #3", ShortDescription: "Short Desc #3", SightTypeId: 3 }];

    private typesData: Type[] = [   { Id: 1, Name: "Type #1" },
                                { Id: 2, Name: "Test #2" },
                                { Id: 3, Name: "Test #3" }];

    getSights() : Sight[] {
        return this.sightsData;
    }

    getTypes() : Type[] {
        return this.typesData;
    }
}