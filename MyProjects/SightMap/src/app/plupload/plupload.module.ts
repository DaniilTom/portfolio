import { NgModule } from "@angular/core";
import { BrowserModule } from '@angular/platform-browser';
import { PluploadComponent } from './plupload.component';

@NgModule({
    imports:[BrowserModule],
    declarations:[PluploadComponent],
    exports:[PluploadComponent]
})
export class PluploadModule{}