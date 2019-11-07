import { Component, Input, OnInit } from "@angular/core";
import { Sight, Review, Type } from "../../model/base.model";
import { NgForm } from '@angular/forms';
import { SightService } from '../../data/sights-data.service';
import { TypeService } from '../../data/types-data.service';
import { ReviewService } from '../../data/reviews-data.service';
import { ReviewFilter } from '../../model/filters.model';
import { ContainerService } from '../../data/container.service';
import { Subject } from 'rxjs';

@Component({
    selector: 'detail-comp',
    templateUrl: './detail.component.html'
})
export class DetailComponent implements OnInit {

    ngOnInit(): void { }

    isReadOnly = true;
    sight: Sight;
    reviews: Review[] = [];
    review: Review;
    types: Type[] = [];

    switchEditMode:Subject<boolean> = new Subject();

    constructor(
        private sightService: SightService,
        private typeService: TypeService,
        private reviewsService: ReviewService,
        private container: ContainerService) {

        typeService.getTypes().then((data: Type[]) => this.types = data);
        this.sight = container.get("sight");
        this.reviewsService.getReviews(new ReviewFilter(0, 0, this.sight.id)).then((data: Review[]) => this.reviews = data);
    }

    @Input() set Sight(_sight: Sight) {
        this.sight = _sight;
        this.reviewsService.getReviews(new ReviewFilter(0, 0, this.sight.id)).then((data: Review[]) => this.reviews = data);
    }

    prepareImgPreview($event) {
        var input = $event.target;
        var reader = new FileReader();

        reader.onloadend = function (e) {
            var imgObj = document.images.namedItem('preview');
            imgObj.src = reader.result.toString();
        }

        reader.readAsDataURL(input.files[0]);
    }

    switchEditBlock() {
        this.isReadOnly = !this.isReadOnly;
        this.switchEditMode.next(this.isReadOnly);
    }

    addReview() {
        var form = document.forms.namedItem("newReview");
        var formData = new FormData(form);
        this.reviewsService.addReview(formData).then(data => this.reviews.push(data));
    }

    editSight(ngform: NgForm, id: number) {
        this.isReadOnly = true;
        if (ngform.valid) {
            var form = document.forms.namedItem('editForm');
            var formData = new FormData(form);
            this.sightService.editSight(id, formData).then((data: Sight) => {
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
}
