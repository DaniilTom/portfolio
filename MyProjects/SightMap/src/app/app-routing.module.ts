import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreateComponent } from './create/create.component';
import { ShowComponent } from './show/show.component';
import { DetailComponent } from './show/detail/detail.component';


const routes: Routes = [
    { path: 'create', component: CreateComponent },
    {
        path: 'show', component: ShowComponent,
        //children: [
        //    { path: ':id', component: DetailComponent }
        //]
    },
    { path: 'show/:id', component: DetailComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
