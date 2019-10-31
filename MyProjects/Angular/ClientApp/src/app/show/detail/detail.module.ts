import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { DetailComponent } from "./detail.component";

@NgModule({ 
    imports: [BrowserModule],
    declarations: [DetailComponent],
    exports: [DetailComponent]
})
export class DetailModule { }