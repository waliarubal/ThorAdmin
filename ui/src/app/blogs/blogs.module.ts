import { NgModule } from '@angular/core';
import { BlogsRoutingModule } from './blogs.routing';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { MatSortModule } from '@angular/material/sort';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';

import { BlogsComponent } from './blogs.component';
import { CreateBlogComponent } from './create-blog/create-blog.component';

@NgModule({
  declarations: [BlogsComponent, CreateBlogComponent],
  imports: [
    CommonModule,
    FormsModule,
    BlogsRoutingModule,
    MatSortModule,
    MatTableModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatTooltipModule,
    MatDialogModule,
    MatInputModule,
    MatFormFieldModule,
  ],
  providers: [],
})
export class BlogsModule {}
