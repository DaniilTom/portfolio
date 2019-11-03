import { Sight, Type } from '../model/base.model';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SightResult, TypeResult } from '../model/results.model';
import { SightFilter } from '../model/filters.model';

@Injectable()
export class DataService {

    private basePath: string = "http://localhost:52208/";

    private apiSights: string = this.basePath + "api/sights/";
    private apiSightTypes: string = this.basePath + "api/sighttypes/";
    private apiReviews: string = this.basePath + "api/reviews/";

    private typeResult: TypeResult = new TypeResult();

    getTypes(): Type[] {
        return this.typeResult.value;
    }


    constructor(private client: HttpClient) {
        this.SyncLoad();
    }

    private async SyncLoad() {
        this.typeResult = await this.getTypesFromServer().toPromise();
    }

    getSightsFromServer(filter?: SightFilter): Observable<SightResult> {
        if (filter == undefined)
            filter = new SightFilter();
        var params = this.GetQueryString(filter);
        //Query
        return this.client.get<SightResult>(this.apiSights + params);
    }

    getTypesFromServer(): Observable<TypeResult> {
        return this.client.get<TypeResult>(this.apiSightTypes);
    }

    addSight(): Observable<SightResult> {
        var form = document.forms.namedItem('sightForm');
        var formData = new FormData(form);
        return this.client.post<SightResult>(this.apiSights, formData);
    }

    updateSight(id: number): Observable<SightResult> {
        var form = document.forms.namedItem('updateForm');
        var formData = new FormData(form);
        return this.client.post<SightResult>(this.apiSights + `${id}`, formData);
    }

    deleteSight(id: number): Observable<SightResult> {
        return this.client.delete<SightResult>(this.apiSights + `?id=${id}`);
    }

    addType() {
        var form = document.forms.namedItem('createType');
        var formData = new FormData(form);
        return this.client.post<TypeResult>(this.apiSightTypes, formData);
    }

    GetQueryString(obj: any): string {
        var keys = Object.keys(obj);
        var query = keys.map(function (key) {

            var temp = obj[key];
            if ((temp != undefined) && (temp != null) && (temp != 0))
                return encodeURIComponent(key) + '=' + encodeURIComponent(obj[key]);
            else
                return "";

        }).filter( str => str != "").join('&');
        return '?' + query;
    }
}
