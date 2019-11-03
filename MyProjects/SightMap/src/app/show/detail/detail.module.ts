import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { DetailComponent } from "./detail.component";
import { FormsModule } from '@angular/forms';

@NgModule({ 
    imports: [BrowserModule, FormsModule],
    declarations: [DetailComponent],
    exports: [DetailComponent]
})
export class DetailModule { }
