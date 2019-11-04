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

    private typeResult: TypeResult<Type[]> = new TypeResult();

    getTypes(): Type[] {
        return this.typeResult.value;
    }


    constructor(private client: HttpClient) { }

    getItems<T>(path: string, filter?: any): Observable<T> {
        var params = "";

        if ((filter != undefined) && (filter != null))
            params = this.getQueryString(filter);

        return this.client.get<T>(path + params);
    }

    addItem<T>(path: string, form: FormData): Observable<T> {
        return this.client.post<T>(path, form);
    }

    editItem<T>(path: string, id: number, form: FormData): Observable<T> {
        return this.client.post<T>(path + id, form);
    }

    deleteItem<T>(path: string, id: number): Observable<T> {
        return this.client.delete<T>(path + `?id=${id}`);
    }

    deleteSight(id: number): Observable<SightResult<Sight>> {
        return this.client.delete<SightResult<Sight>>(this.apiSights + `?id=${id}`);
    }

    private getQueryString(obj: any): string {
        var keys = Object.keys(obj);
        var query = keys.map(function (key) {

            var temp = obj[key];
            if ((temp != undefined) && (temp != null) && (temp != 0))
                return encodeURIComponent(key) + '=' + encodeURIComponent(obj[key]);
                //return key + '=' + obj[key];
            else
                return "";

        }).filter(str => str != "").join('&');
        return '?' + query;
    }
}
