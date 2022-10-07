import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ComponentBase } from 'src/app/shared/base.component';
import { IFileSystemEntry } from 'src/app/shared/models/file-system-entry.model';
import { FileSystemService } from 'src/app/shared/services/file-system.service';

@Component({
  selector: 'app-files-create-entry',
  templateUrl: './create-entry.component.html',
  providers: [FileSystemService],
})
export class CreateEntryComponent extends ComponentBase {
  constructor(
    public dialogRef: MatDialogRef<CreateEntryComponent>,
    private _fsService: FileSystemService,
    @Inject(MAT_DIALOG_DATA) private _entry: IFileSystemEntry
  ) {
    super();
  }

  get Entry() {
    return this._entry;
  }

  CreateEntry() {
    this.IsBusy = true;
    this.dialogRef.disableClose = true;
    let subscription = this._fsService
      .CreateEntry(this.Entry)
      .subscribe((result) => {
        this.dialogRef.disableClose = false;
        subscription.unsubscribe();
        this.IsBusy = false;
        this.dialogRef.close(result.Error ? false : result.Data);
      });
  }
}
