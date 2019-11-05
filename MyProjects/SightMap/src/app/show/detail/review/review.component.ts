import { Component, Input } from "@angular/core";
import { ReviewService } from '../../../data/reviews-data.service';
import { NgForm } from '@angular/forms';
import { Review } from '../../../model/base.model';

@Component({
    selector: 'review-comp',
    templateUrl: './review.component.html'
})
export class ReviewComponent {

    isReadOnly = true;
    renderDetail = false;
    reviews: Review[];

    constructor(private reviewsService: ReviewService) { }

    @Input() set Reviews(reviews: Review[]) {
        this.reviews = reviews;
    }
}
