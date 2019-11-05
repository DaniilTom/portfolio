import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { DetailComponent } from "./detail.component";
import { FormsModule } from '@angular/forms';
import { ReviewModule } from './review/review.module';

@NgModule({
    imports: [BrowserModule, FormsModule, ReviewModule],
    declarations: [DetailComponent],
    exports: [DetailComponent]
})
export class DetailModule { }
