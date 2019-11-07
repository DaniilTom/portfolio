import { NgModule } from '@angular/core';
import { ShowComponent } from './show.component';
import { BrowserModule } from '@angular/platform-browser';

import { DetailModule } from './detail/detail.module';
import { FilterModule } from './filter/filter.module'
import { RouterModule } from '@angular/router';
import { MapFilterModule } from './mapfilter/mapfilter.module';
import { YMapModule } from '../YMap/ymap.module';


@NgModule({
    imports: [BrowserModule, DetailModule, FilterModule, RouterModule, MapFilterModule, YMapModule],
    declarations: [ShowComponent],
    exports: [ShowComponent]
})
export class ShowModule { }
