import { NgModule } from "@angular/core";
import { BrowserModule } from '@angular/platform-browser';
import { PluploadComponent } from './plupload.component';
import { FormsModule } from '@angular/forms';

@NgModule({
    imports:[BrowserModule, FormsModule],
    declarations:[PluploadComponent],
    exports:[PluploadComponent]
})
export class PluploadModule{}