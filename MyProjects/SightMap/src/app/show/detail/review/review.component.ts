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

    addReview(ngform: NgForm, formId: number) {
        //if (ngform.valid)   {
        var form = document.forms.namedItem("formN" + formId);
        var formData = new FormData(form);
        this.reviewsService.addReview(formData).then(data => this.reviews.find(r => r.id == data.parentId).children.push(data));
        //}
        //else
        //    alert("Сообщение не может быть пустым.");
    }

    deleteReview(review: Review) {
        //alert(`Id отзыва: ${id}; Id формы: ${formId}`);
        //var form = document.forms.namedItem("formN" + formId);
        var formData = new FormData();
        formData.set("message", "Комментарий был удален.");
        formData.set("id", review.id.toString());
        formData.set("parentId", review.parentId.toString());
        formData.set("itemId", review.itemId.toString());
        formData.set("authorId", review.authorId.toString());
        this.reviewsService.editReview(review.id, formData).then(data => {
            review = data;
        });
    }
}
