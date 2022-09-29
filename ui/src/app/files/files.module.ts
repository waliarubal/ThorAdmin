import { NgModule } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';

import { FilesComponent } from './files.component';
import { FilesRoutingModule } from './files.routing';
import { FileSizePipe } from '../shared/pipes/file-size.pipe';

@NgModule({
  declarations: [FilesComponent, FileSizePipe],
  imports: [
    FilesRoutingModule,
    MatIconModule,
    MatToolbarModule,
    MatButtonModule,
    MatTableModule,
    MatDialogModule,
    CommonModule,
  ],
  providers: [],
})
export class FilesModule {}
