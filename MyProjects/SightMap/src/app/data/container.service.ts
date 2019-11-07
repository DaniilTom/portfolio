import { Injectable } from '@angular/core';

@Injectable()
export class ContainerService {

    objArray: Pair[] = [];

    set(key: string, value: any) {
        if(this.get(key) != null)
        {
            this.remove(key);
        }
        this.objArray.push(new Pair(key, value));
    }

    get(key: string): any {
        try{
            return this.objArray.find(pair => pair.key == key).value;
        }
        catch{
            return null;
        }
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
