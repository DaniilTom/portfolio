import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { ReviewComponent } from "./review.component";
import { FormsModule } from '@angular/forms';

@NgModule({ 
    imports: [BrowserModule, FormsModule],
    declarations: [ReviewComponent],
    exports: [ReviewComponent]
})
export class ReviewModule { }
