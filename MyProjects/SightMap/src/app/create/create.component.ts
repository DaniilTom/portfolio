import { Component } from "@angular/core";
import { Sight, Type, Album } from '../model/base.model';
import { NgForm } from '@angular/forms';
import { TypeService } from '../data/types-data.service';
import { SightService } from '../data/sights-data.service';
import { Coordinates, Mode } from '../YMap/ymap.component';
import { Subject } from 'rxjs';
import { PluploadFile } from '../plupload/plupload.component';
import { UploaderService } from '../data/uploader.service';

@Component(
    {
        selector: 'create-comp',
        templateUrl: './create.component.html',
        styleUrls: ['./create.component.css']
    })
export class CreateComponent {

    removeInitPoint: Subject<void> = new Subject<void>();
    beginUpload: Subject<string> = new Subject<string>();
    deleteAllFiles: Subject<void> = new Subject<void>();

    newSight: Sight = new Sight();
    newType: Type = new Type();

    referenceId: string;
    isDisabled: boolean = false;

    mode: Mode = Mode.Create;

    types: Type[];

    ngForm: NgForm;

    constructor(public sightService: SightService,
                public typeService: TypeService,
                public uploaderService: UploaderService) {
        this.newSight.type = new Type();
        typeService.getTypes().then((data: Type[]) => this.types = data);
        // uploaderService.getRefId().subscribe(data =>
        //     {
        //         this.referenceId = data;
        //         this.isDisabled = false;
        //     });
        //this.referenceId = (Math.floor((Math.random() * 999999999) + 1)).toString() + 'a';
    }

    beginUploading(ngform: NgForm) {
        if (ngform.valid) {
            this.ngForm = ngform;
            this.isDisabled = true;

            this.uploaderService.getRefId().subscribe(data =>
                {
                    this.referenceId = data;
                    this.beginUpload.next(this.referenceId);
                },
                error => {
                    alert(error);
                    this.isDisabled = false;
                });
            //this.uploaderService.getRefId()
        }
        else
            alert("Ошибки в форме.")
    }

    addSight(fileList: PluploadFile[]) {
        this.newSight.album = fileList.map((value: PluploadFile) => {
            var temp = new Album();
            temp.imageName = value.name;
            temp.isMain = value.isMain;
            temp.state = value.state;
            temp.title = value.title;
            return temp;
        });
        this.newSight.refId = this.referenceId;
        this.sightService.addSight(this.newSight).then((data: Sight) => {
            if (data != null) {
                alert("Добавлено с id:" + data.id);
                this.ngForm.resetForm();
                this.removeInitPoint.next();
            }
            else
                alert("Ошибка.");
                
            this.deleteAllFiles.next();
            this.isDisabled = false;
        });
    }

    addType(ngform: NgForm) {
        if (ngform.valid) {
            this.typeService.addType(this.newType).then((data: Type) => {
                if (data != null) {
                    alert("Добавлено с id:" + data.id);
                    this.types.push(data);
                }
            });
        }
        else
            alert("Ошибки в форме.")
    }

    // метод подписки на событие карты coordinateChanged
    setCoordinates(coord: Coordinates) {
        this.newSight.latitude = coord.latitude;
        this.newSight.longitude = coord.longitude;
    }

    onDeleteAllFiles() {
        this.deleteAllFiles.next();
    }
}