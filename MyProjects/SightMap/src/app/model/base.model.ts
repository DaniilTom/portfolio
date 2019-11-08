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
    public longitude?: number;
    public latitude?: number;
    public albums?: Album[] = [];
}

export class Type {
    constructor(
        public id?: number,
        public name?: string) { }
}

export class Review {
    public id?: number;
    public parentId?: number;
    public itemId?: number;
    public message?: string;
    public authorId?: number;
    public children?: Review[] = [];
}

export class Album {
    public id?: number;
    public itemId?: number;
    public isMain?: boolean;
}

