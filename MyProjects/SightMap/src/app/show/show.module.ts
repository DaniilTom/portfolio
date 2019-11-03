import { NgModule } from '@angular/core';
import { ShowComponent } from './show.component';
import { BrowserModule } from '@angular/platform-browser';

import { DetailModule } from './detail/detail.module';
import { FilterModule } from './filter/filter.module'


@NgModule({
    imports: [BrowserModule, DetailModule, FilterModule],
    declarations: [ShowComponent],
    exports: [ShowComponent]
})
export class ShowModule { }
