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
import { MatRadioModule } from '@angular/material/radio';

import { FilesComponent } from './files.component';
import { FilesRoutingModule } from './files.routing';
import { DeleteEntryComponent } from './delete-entry/delete-entry.component';
import { RenameEntryComponent } from './rename-entry/rename-entry.component';
import { CreateEntryComponent } from './create-entry/create-entry.component';
import { UploadEntryComponent } from './upload-entry/upload-entry.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [
    FilesComponent,
    DeleteEntryComponent,
    RenameEntryComponent,
    CreateEntryComponent,
    UploadEntryComponent,
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
    MatRadioModule,
    SharedModule,
  ],
  providers: [],
})
export class FilesModule {}
