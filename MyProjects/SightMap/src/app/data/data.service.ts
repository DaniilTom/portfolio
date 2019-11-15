import { Type } from '../model/base.model';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ResultState } from '../model/results.model';
import 'rxjs/add/operator/map';

@Injectable()
export class DataService {

    // private basePath: string = "http://localhost:52208/";

    // private apiSights: string = this.basePath + "api/sights/";
    // private apiSightTypes: string = this.basePath + "api/sighttypes/";
    // private apiReviews: string = this.basePath + "api/reviews/";

    private ResultState: ResultState<Type[]> = new ResultState();

    getTypes(): Type[] {
        return this.ResultState.value;
    }


    constructor(private client: HttpClient) { }

    getItems<T>(path: string, filter?: any): Observable<T> {
        var params = "";

        if ((filter != undefined) && (filter != null))
            params = this.getQueryString(filter);

        return this.client.get<T>(path + params);
    }

    getCount<T>(path: string, filter?: any): Observable<T> {
        var params = "";

        if ((filter != undefined) && (filter != null))
            params = this.getQueryString(filter);

        return this.client.get<T>(path + 'count/' + params);
    }

    addItem<T>(path: string, item: any): Observable<T> {        
        let headers = new HttpHeaders();
        headers = headers.set('Content-Type', 'application/json; charset=utf-8');
        return this.client.post<T>(path, item, { headers });
    }

    editItem<T>(path: string, item: any): Observable<T> {
        let headers = new HttpHeaders();
        headers = headers.set('Content-Type', 'application/json; charset=utf-8');
        return this.client.put<T>(path, JSON.stringify(item), { headers });
    }

    deleteItem<T>(path: string, id: number): Observable<T> {
        return this.client.delete<T>(path + `?id=${id}`);
    }

    private getQueryString(obj: any): string {
        var keys = Object.keys(obj);
        var query = keys.map(function (key) {

            var temp = obj[key];
            if ((temp != undefined) && (temp != null) && (temp != 0))
                return encodeURIComponent(key) + '=' + encodeURIComponent(obj[key]);
            else
                return "";

        }).filter(str => str != "").join('&');
        if (query != "")
            query = '?' + query;
        return query;
    }
}
