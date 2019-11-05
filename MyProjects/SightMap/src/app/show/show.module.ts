import { NgModule } from '@angular/core';
import { ShowComponent } from './show.component';
import { BrowserModule } from '@angular/platform-browser';

import { DetailModule } from './detail/detail.module';
import { FilterModule } from './filter/filter.module'
import { RouterModule } from '@angular/router';


@NgModule({
    imports: [BrowserModule, DetailModule, FilterModule, RouterModule],
    declarations: [ShowComponent],
    exports: [ShowComponent]
})
export class ShowModule { }
