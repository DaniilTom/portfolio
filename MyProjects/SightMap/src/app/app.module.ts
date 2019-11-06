import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { CreateModule } from './create/create.module';
import { ShowModule } from './show/show.module';

import { DataService } from './data/data.service';
import { SightService } from './data/sights-data.service';
import { TypeService } from './data/types-data.service';
import { ReviewService } from './data/reviews-data.service';
import { ContainerService } from './data/container.service';

import { YaCoreModule }  from 'angular2-yandex-maps';

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        HttpClientModule,
        AppRoutingModule,
        CreateModule, ShowModule, YaCoreModule.forRoot()
    ],
    providers: [DataService, SightService, TypeService, ReviewService, ContainerService],
    bootstrap: [AppComponent]
})
export class AppModule { }
