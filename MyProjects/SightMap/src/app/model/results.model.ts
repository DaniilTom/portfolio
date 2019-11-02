import { Sight } from './base.model'
import { Type } from './base.model';

export class SightResult {
    public isSuccess?: boolean;
    public value?: Sight[];
    public message?: string;
}

export class TypeResult {
    public isSuccess?: boolean;
    public value?: Type[];
    public message?: string;
}
