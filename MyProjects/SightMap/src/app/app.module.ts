import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { CreateModule } from './create/create.module';
import { ShowModule } from './show/show.module';

import { DataService } from './data/data.service';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
      AppRoutingModule,
      FormsModule,
      CreateModule, ShowModule
  ],
  providers: [DataService],
  bootstrap: [AppComponent]
})
export class AppModule { }
