import { NgModule } from '@angular/core';
import { MapFilterComponent } from './mapfilter.component';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { YMapModule } from 'src/app/YMap/ymap.module';


@NgModule({
    imports: [BrowserModule, FormsModule, YMapModule],
    declarations: [MapFilterComponent],
    exports: [MapFilterComponent]
})
export class MapFilterModule { }
