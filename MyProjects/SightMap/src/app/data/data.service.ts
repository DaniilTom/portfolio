import { Sight, Type } from '../model/base.model';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SightResult, TypeResult } from '../model/results.model';

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

    getSightsFromServer(): Observable<SightResult> {
        return this.client.get<SightResult>(this.apiSights);
    }

    getTypesFromServer(): Observable<TypeResult> {
        return this.client.get<TypeResult>(this.apiSightTypes);
    }

    addSight() {
        var form = document.forms.namedItem('sightForm');
        var formData = new FormData(form);
        this.client.post(this.apiSights, formData).subscribe((data: SightResult) => alert(data.isSuccess));
    }

    updateSight() {
        var form = document.forms.namedItem('updateForm');
        var formData = new FormData(form);
        this.client.put(this.apiSights, formData).subscribe((data: SightResult) => alert(data.isSuccess));
    }

    deleteSight(id: number) {
        this.client.delete(this.apiSights + "?id=" + id).subscribe((data: SightResult) => alert(data.isSuccess));
    }
}
