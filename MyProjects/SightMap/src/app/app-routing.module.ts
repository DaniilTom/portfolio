import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreateComponent } from './create/create.component';
import { ShowComponent } from './show/show.component';
import { DetailComponent } from './show/detail/detail.component';


const routes: Routes = [
    { path: 'create', component: CreateComponent },
    { path: 'show', component: ShowComponent },
    { path: 'show/:id', component: DetailComponent },
    { path: 'showmap', component: ShowComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
