import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './not-found/not-found.component';
import { AuthGuard } from './shared/auth.guard';

const routes: Routes = [
  { path: '', redirectTo: '/users/sign-in', pathMatch: 'full' },
  {
    path: 'blogs',
    loadChildren: () =>
      import('./blogs/blogs.module').then((m) => m.BlogsModule),
    canActivate: [AuthGuard],
  },
  {
    path: 'databases',
    loadChildren: () =>
      import('./databases/databases.module').then((m) => m.DatabasesModule),
    canActivate: [AuthGuard],
  },
  {
    path: 'files',
    loadChildren: () =>
      import('./files/files.module').then((m) => m.FilesModule),
    canActivate: [AuthGuard],
  },
  {
    path: 'users',
    loadChildren: () =>
      import('./users/users.module').then((m) => m.UsersModule),
  },
  {
    path: '**',
    component: NotFoundComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
