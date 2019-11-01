import { Type } from "./type.model";
import { ISight } from '../data/data.service';

export class Sight {
    constructor(
        public Id?: number,
        public Name?: string,
        public FullDescription?: string,
        public ShortDescription?: string,
        public AuthorId?: number,
        public CreateDate?: Date,
        public UpdateDate?: Date,
        public SightTypeId?: number,
        public Type?: Type) { }
}
