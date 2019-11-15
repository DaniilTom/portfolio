import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { Observable } from 'rxjs';

@Injectable()
export class UploaderService {

    private basePath: string = "http://localhost:52208/";

    private apiUploader: string = this.basePath + "api/uploader/";

    lastResult: string;

    constructor(public dataService: DataService) { }

    getRefId(): Observable<string> {
        return this.dataService.getItems<string>(this.apiUploader);
    }
}
