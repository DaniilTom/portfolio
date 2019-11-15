import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { DetailComponent } from "./detail.component";
import { FormsModule } from '@angular/forms';
import { ReviewModule } from './review/review.module';
import { YMapModule } from 'src/app/YMap/ymap.module';
import { AlbumModule } from './album/album.module';
import { PluploadModule } from 'src/app/plupload/plupload.module';

@NgModule({
    imports: [BrowserModule, FormsModule, ReviewModule, YMapModule, AlbumModule, PluploadModule],
    declarations: [DetailComponent],
    exports: [DetailComponent]
})
export class DetailModule { }
