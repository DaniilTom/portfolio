import { Sight, Review, Type } from './base.model'

export class SightResult<T> {
    public isSuccess?: boolean;
    public value?: T;
    public message?: string;
}

export class TypeResult<T> {
    public isSuccess?: boolean;
    public value?: T;
    public message?: string;
}

export class ReviewResult {
    public isSuccess?: boolean;
    public value?: Review[];
    public message?: string;
}
