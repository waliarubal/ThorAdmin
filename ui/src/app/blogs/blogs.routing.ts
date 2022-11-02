import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { BlogsComponent } from './blogs.component';
import { UsersComponent } from './users/users.component';

const routes: Routes = [
  {
    path: '',
    component: BlogsComponent,
  },
  {
    path: ':id/users',
    component: UsersComponent
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BlogsRoutingModule { }
