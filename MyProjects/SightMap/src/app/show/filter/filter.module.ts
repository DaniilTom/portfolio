import { NgModule } from '@angular/core';
import { FilterComponent } from './filter.component';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';


@NgModule({
    imports: [BrowserModule, FormsModule],
    declarations: [FilterComponent],
    exports: [FilterComponent]
})
export class FilterModule { }
