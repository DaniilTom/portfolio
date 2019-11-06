import { NgModule } from '@angular/core';
import { CreateComponent } from './create.component';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { YMapModule } from '../YMap/ymap.module';

@NgModule({
    imports: [BrowserModule, FormsModule, YMapModule],
    declarations: [CreateComponent],
    exports: [CreateComponent]
})
export class CreateModule {}
