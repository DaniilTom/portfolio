import { Sight, Type } from '../model/base.model';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SightResult, TypeResult } from '../model/results.model';

@Injectable()
export class DataService {

    private basePath: string = "http://localhost:52208/";

    private apiSights: string = "api/sights";
    private apiSightTypes: string = "api/sighttypes";
    private apiReviews: string = "api/reviews";

    private typeResult: TypeResult = new TypeResult();

    getTypes(): Type[] {
        //alert("get Types");
        return this.typeResult.value;
    }


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

    addSight(_sight: Sight) {
        //var httpOptions = {
        //    headers: new HttpHeaders({
        //        'Content-Type': 'multipart/form-data'
        //    })
        //};
        var form = document.forms.namedItem('sightForm');
        var formData = new FormData(form);
        //formData..append('object', JSON.stringify(_sight));
        this.client.post(this.basePath + this.apiSights, formData).subscribe((data: SightResult) => alert(data.isSuccess));
    }
}
