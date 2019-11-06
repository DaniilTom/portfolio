import { NgModule } from "@angular/core";
import { BrowserModule } from '@angular/platform-browser';
import { YMapComponent } from './ymap.component';

@NgModule({
    imports:[BrowserModule],
    declarations:[YMapComponent],
    exports:[YMapComponent]
})
export class YMapModule{}