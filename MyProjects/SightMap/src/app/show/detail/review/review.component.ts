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

    newReview: Review = new Review();

    constructor(private reviewsService: ReviewService) { }

    @Input() set Reviews(reviews: Review[]) {
        this.reviews = reviews;
    }

    addReview(parentId: number, itemId: number) {
        this.newReview.itemId = itemId;
        this.newReview.parentId = parentId;
        this.reviewsService.addReview(this.newReview).then(data => this.reviews.find(r => r.id == data.parentId).children.push(data));
        this.newReview = new Review();
    }

    deleteReview(review: Review) {
        review.message = "Комментарий удален."
        this.reviewsService.editReview(review).then(data => {
            review = data;
        });
    }

    switchEdit(show: boolean){
        this.newReview = new Review();
        show = !show;
    }
}
