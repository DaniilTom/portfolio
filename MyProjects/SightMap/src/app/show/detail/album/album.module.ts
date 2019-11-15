import { NgModule } from "@angular/core";
import { AlbumComponent } from './album.component';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { PluploadModule } from 'src/app/plupload/plupload.module';

@NgModule({
    imports: [BrowserModule, FormsModule, PluploadModule],
    declarations: [AlbumComponent],
    exports: [AlbumComponent]
})
export class AlbumModule{}