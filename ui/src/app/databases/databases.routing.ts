import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DatabasesComponent } from './databases.component';

const routes: Routes = [
  {
    path: '',
    component: DatabasesComponent
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DatabasesRoutingModule {}