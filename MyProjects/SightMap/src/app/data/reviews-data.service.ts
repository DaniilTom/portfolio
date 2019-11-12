import { Injectable } from '@angular/core';
import { ResultState } from '../model/results.model';
import { TypeFilter, ReviewFilter } from '../model/filters.model';
import { DataService } from './data.service';
import { Review } from '../model/base.model';

@Injectable()
export class ReviewService {

    private basePath: string = "http://localhost:52208/";

    private apiReviews: string = this.basePath + "api/reviews/";

    getResult: ResultState<Review[]> = new ResultState();
    addResult: ResultState<Review> = new ResultState();
    editResult: ResultState<Review> = new ResultState();
    deleteResult: ResultState<boolean> = new ResultState();

    reviews: Review[] = [];

    constructor(public dataService: DataService) { }

    async getReviews(filter?: ReviewFilter): Promise<Review[]> {

        if (filter == undefined || filter == null) {
            filter = new ReviewFilter();
        }

        this.getResult = await this.dataService.getItems<ResultState<Review[]>>(this.apiReviews, filter).toPromise();
        if (!this.getResult.isSuccess)
            alert(this.getResult.message);

        return this.getResult.value;
    }

    async addReview(review: Review): Promise<Review> {

        this.addResult = await this.dataService.addItem<ResultState<Review>>(this.apiReviews, review).toPromise();
        if (!this.addResult.isSuccess)
            alert(this.addResult.message);

        return this.addResult.value;
    }

    async editReview(review: Review): Promise<Review> {
        this.editResult = await this.dataService.editItem<ResultState<Review>>(this.apiReviews, review).toPromise();
        if (!this.editResult.isSuccess)
            alert(this.editResult.message);

        return this.editResult.value;
    }

    async deleteReview(id: number): Promise<boolean> {
        this.deleteResult = await this.dataService.deleteItem<ResultState<boolean>>(this.apiReviews, id).toPromise();
        if (!this.deleteResult.isSuccess)
            alert(this.deleteResult.message);

        return this.deleteResult.value;
    }
}
