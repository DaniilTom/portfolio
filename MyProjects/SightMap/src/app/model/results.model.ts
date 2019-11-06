import { Sight, Review, Type } from './base.model'

export class ReviewResult {
    public isSuccess?: boolean;
    public value?: Review[];
    public message?: string;
}

export class ResultState <T> {
    public isSuccess?: boolean;
    public value?: T;
    public message?: string;
}
