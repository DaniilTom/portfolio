import { Component, Input, OnInit, ViewChild, TemplateRef, ElementRef, Output } from "@angular/core";
import { Sight, Review, Type, Album, State } from "../../model/base.model";
import { NgForm } from '@angular/forms';
import { SightService } from '../../data/sights-data.service';
import { TypeService } from '../../data/types-data.service';
import { ReviewService } from '../../data/reviews-data.service';
import { ReviewFilter } from '../../model/filters.model';
import { ContainerService } from '../../data/container.service';
import { Subject } from 'rxjs';
import { Coordinates } from 'src/app/YMap/ymap.component';
import { UploaderService } from 'src/app/data/uploader.service';
import { PluploadFile } from 'src/app/plupload/plupload.component';

@Component({
    selector: 'detail-comp',
    templateUrl: './detail.component.html',
    styleUrls: ['./detail.component.css']
})
export class DetailComponent implements OnInit {

    ngOnInit(): void { }

    isDisabled = true;
    isReviewEdit: boolean = false;
    sight: Sight;
    reviews: Review[] = [];
    newReview: Review = new Review();
    types: Type[] = [];
    
    referenceId: string;
    switchEditMode: Subject<boolean> = new Subject<boolean>();
    beginUpload: Subject<string> = new Subject<string>();
    resetMainImageInUpload: Subject<void> = new Subject<void>();

    constructor(
        private sightService: SightService,
        private typeService: TypeService,
        private reviewsService: ReviewService,
        private container: ContainerService,
        private uploaderService: UploaderService) {

        typeService.getTypes().then((data: Type[]) => this.types = data);
        this.sight = container.get("sight");
        this.reviewsService.getReviews(new ReviewFilter(0, 0, this.sight.id)).then((data: Review[]) => this.reviews = data);
    }

    getInitPoint(): Coordinates {
        return new Coordinates(this.sight.latitude, this.sight.longitude);
    }



    addReview() {
        this.newReview.itemId = this.sight.id;

        this.reviewsService.addReview(this.newReview).then(data => this.reviews.push(data));
    }

    beginUploading(ngform: NgForm) {
        if (ngform.valid) {
            this.isDisabled = true;

            this.uploaderService.getRefId().subscribe(data => {
                this.referenceId = data;
                this.beginUpload.next(data);
            },
                error => {
                    alert(error);
                    this.isDisabled = false;
                });
        }
        else
            alert("Ошибки в форме.")
    }

    editSight(fileList: PluploadFile[]) {
        fileList.forEach((value: PluploadFile) => {
            var temp = new Album();
            temp.imageName = value.name;
            temp.isMain = value.isMain;
            temp.state = value.state;
            temp.title = value.title;
            this.sight.album.push(temp);
        });

        this.sight.refId = this.referenceId;
        this.sightService.editSight(this.sight).then((data: Sight) => {
            if (data != null)
            {
                alert(`Успешно (id: ${data.id})`);
                this.sight = data;
            }
            else
                alert("Ошибка");

            this.isDisabled = false;
        });
    }

    deleteSight(id: number) {
        this.sightService.deleteSight(id).then((data: boolean) => {
            if (data)
                alert(`Удалено: ${data})`);
        });
        this.ngOnInit();
    }

    setCoordinates(coord: Coordinates) {
        this.sight.latitude = coord.latitude;
        this.sight.longitude = coord.longitude;
    }

    switchEditReview(show: boolean) {
        this.newReview = new Review();
        this.isReviewEdit = !this.isReviewEdit;
    }

    switchEditForm() {
        this.isDisabled = !this.isDisabled;
        this.switchEditMode.next(this.isDisabled);
    }

    mainImageSetInUpload() {
        let oldMain = this.sight.album.find(page => page.isMain == true);
        oldMain.isMain = false;
        oldMain.state = State.Edit;
    }

    mainImageSetInAlbum() {
        this.resetMainImageInUpload.next();
    }
}
