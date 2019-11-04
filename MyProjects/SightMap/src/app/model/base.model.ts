export class Sight {
    public id?: number;
    public name?: string;
    public fullDescription?: string;
    public shortDescription?: string;
    public authorId?: number;
    public createDate: Date;
    public updateDate: Date;
    public photoPath?: string;
    public sightTypeId?: number;
    public type?: Type;
}

export class Type {
    constructor(
        public id?: number,
        public name?: string) { }
}

export class Review {
    public parentId?: number;
    public itemId?: number;
    public message?: string;
    public authorId?: number;
    public children?: Review[] = [];
}

