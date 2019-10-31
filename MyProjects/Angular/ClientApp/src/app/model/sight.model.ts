import { Type } from "./type.model";

export class Sight{
    Id?: number;
    Name?: string;
    FullDescription?: string;
    ShortDescription?: string;
    AuthorId?: number;
    CreateDate?: Date;
    UpdateDate?: Date;
    SightTypeId?: number;
    Type?: Type;
}