export class SightFilter {
    constructor(
        public offset: number = 0,
        public size: number = 10,
        public id: number = 0,
        public name: string = "",
        public authorId: number = 0,
        public createUpDate: Date = null,
        public createDownDate: Date = null,
        public updateUpDate: Date = null,
        public updateDownDate: Date = null,
        public sightTypeId: number = 0) { }
}
