import { Injectable } from '@angular/core';

@Injectable()
export class ContainerService {

    objArray: Pair[] = [];

    set(key: string, value: any) {
        this.objArray.push(new Pair(key, value));
    }

    get(key: string) {
        return this.objArray.find(pair => pair.key == key).value;
    }
}

class Pair {
    constructor(
        public key: string,
        public value: any
    ) { }
}
