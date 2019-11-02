import { NgModule } from '@angular/core';
import { CreateComponent } from './create.component';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

@NgModule({
    imports: [BrowserModule, FormsModule],
    declarations: [CreateComponent],
    exports: [CreateComponent]
})
export class CreateModule {}
