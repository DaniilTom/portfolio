import { NgModule } from '@angular/core';
import { CreateComponent } from './create.component';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { YMapModule } from '../YMap/ymap.module';
import { PluploadModule } from '../plupload/plupload.module';

@NgModule({
    imports: [BrowserModule, FormsModule, YMapModule, PluploadModule],
    declarations: [CreateComponent],
    exports: [CreateComponent]
})
export class CreateModule {}
