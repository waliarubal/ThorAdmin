import { NgModule } from '@angular/core';
import { BlogsRoutingModule } from './blogs.routing';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { MatSortModule } from '@angular/material/sort';

import { BlogsComponent } from './blogs.component';

@NgModule({
  declarations: [BlogsComponent],
  imports: [
    CommonModule,
    BlogsRoutingModule,
    MatSortModule,
    MatTableModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
  ],
  providers: [],
})
export class BlogsModule {}
