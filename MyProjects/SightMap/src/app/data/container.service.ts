import { Injectable } from '@angular/core';

@Injectable()
export class ContainerService {

    objArray: Pair[] = [];

    set(key: string, value: any) {
        if(this.objArray.includes(this.get(key)))
        {
            this.remove(key);
        }
        this.objArray.push(new Pair(key, value));
    }

    get(key: string) {
        return this.objArray.find(pair => pair.key == key).value;
    }

    remove(key: string){
        var index = this.objArray.indexOf(this.get(key));
        this.objArray.splice(index, 1);
    }
}

class Pair {
    constructor(
        public key: string,
        public value: any
    ) { }
}
