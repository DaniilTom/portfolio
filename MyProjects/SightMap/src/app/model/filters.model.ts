export class SightFilter {
    constructor(
        public offset: number = 1,
        public size: number = 10,
        public id: number = 0,
        public name: string = "",
        public authorId: number = 0,
        public createBeforeDate: Date = null,
        public createAfterDate: Date = null,
        public updateBeforeDate: Date = null,
        public updateAfterDate: Date = null,
        public sightTypeId: number = 0,
        public latitudeMax: number = null,
        public latitudeMin: number = null,
        public longitudeMax: number = null,
        public longitudeMin: number = null) { }
}

export class TypeFilter {
    public id: number = 0;
    public name: string = "";
}

export class ReviewFilter {
    constructor(
        public id: number = 0,
        public parentId: number = 0,
        public itemId: number = 0
    ) { }
}

export class AlbumFilter {
    public id: number;
    public itemId: number;
    public isMain: boolean;
}
