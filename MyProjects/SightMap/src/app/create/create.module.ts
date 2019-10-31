import { NgModule } from "@angular/core";
import { CreateComponent } from "./create.component";
import { BrowserModule } from "@angular/platform-browser";

@NgModule({
    imports: [BrowserModule],
    declarations: [CreateComponent],
    exports: [CreateComponent]
})
export class CreateModule {}