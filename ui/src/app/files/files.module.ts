import { NgModule } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { MatSortModule } from '@angular/material/sort';
import { FormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';

import { FilesComponent } from './files.component';
import { FilesRoutingModule } from './files.routing';
import { FileSizePipe } from '../shared/pipes/file-size.pipe';
import { DeleteEntryComponent } from './delete-entry.component.ts/delete-entry.component';
import { RenameEntryComponent } from './rename-entry/rename-entry.component';

@NgModule({
  declarations: [
    FilesComponent,
    FileSizePipe,
    DeleteEntryComponent,
    RenameEntryComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    FilesRoutingModule,
    MatIconModule,
    MatToolbarModule,
    MatButtonModule,
    MatSortModule,
    MatTableModule,
    MatDialogModule,
    MatInputModule,
    MatFormFieldModule,
  ],
  providers: [],
})
export class FilesModule {}
