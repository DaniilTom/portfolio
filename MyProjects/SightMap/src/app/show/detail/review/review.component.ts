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

    deleteReview(id: number, formId: number) {
        var form = document.forms.namedItem("formN" + formId);
        var formData = new FormData(form);
        formData.set("message", "Комментарий был удален.");
        this.reviewsService.editReview(id, formData).then(data => {

            let old = this.reviews.find(r => r.id == data.parentId)
                            .children.find(r => r.id == data.id);
            old = data;
        });
    }
}
