import { Component, Input } from "@angular/core";
import { DataService } from '../../../data/data.service';
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

    constructor(private dataService: DataService) { }

    @Input() set Review(reviews: Review[]) {
        this.reviews = reviews;
    }
}
