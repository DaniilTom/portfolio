import { Component, Input, OnInit } from "@angular/core";
import { Sight, Review, Type, Album } from "../../model/base.model";
import { NgForm } from '@angular/forms';
import { SightService } from '../../data/sights-data.service';
import { TypeService } from '../../data/types-data.service';
import { ReviewService } from '../../data/reviews-data.service';
import { ReviewFilter } from '../../model/filters.model';
import { ContainerService } from '../../data/container.service';
import { Subject } from 'rxjs';
import { Coordinates } from 'src/app/YMap/ymap.component';

@Component({
    selector: 'detail-comp',
    templateUrl: './detail.component.html',
    styleUrls: ['./detail.component.css']
})
export class DetailComponent implements OnInit {

    ngOnInit(): void { }

    isReadOnly = true;
    sight: Sight;
    reviews: Review[] = [];
    newReview: Review = new Review();
    types: Type[] = [];
    album: Album[] = [];

    mainImgDiv: HTMLDivElement;

    switchEditMode:Subject<boolean> = new Subject();

    constructor(
        private sightService: SightService,
        private typeService: TypeService,
        private reviewsService: ReviewService,
        private container: ContainerService) {

        typeService.getTypes().then((data: Type[]) => this.types = data);
        this.sight = container.get("sight");
        this.album = this.sight.album.copyWithin(0, 0);
        this.reviewsService.getReviews(new ReviewFilter(0, 0, this.sight.id)).then((data: Review[]) => this.reviews = data);

        var mainPreviewDiv = document.getElementsByClassName('main-preview')[0];
        
    }

    getInitPoint(): Coordinates {
        return new Coordinates(this.sight.latitude, this.sight.longitude);
    }



    addReview() {
        this.newReview.itemId = this.sight.id;

        this.reviewsService.addReview(this.newReview).then(data => this.reviews.push(data));
    }

    editSight(ngform: NgForm, id: number) {
        this.isReadOnly = true;
        if (ngform.valid) {
            this.sightService.editSight(this.sight).then((data: Sight) => {
                if (data != null)
                    alert(`Успешно (id: ${data.id})`);
            });
        }
        else
            alert("Ошибки в форме.")
    }

    deleteSight(id: number) {
        this.sightService.deleteSight(id).then((data: boolean) => {
            if (data)
                alert(`Удалено: ${data})`);
        });
        this.ngOnInit();
    }

    setCoordinates(coord: Coordinates){
        this.sight.latitude = coord.latitude;
        this.sight.longitude = coord.longitude;
    }

    switchEdit(show: boolean){
        this.newReview = new Review();
        show = !show;
    }
}
