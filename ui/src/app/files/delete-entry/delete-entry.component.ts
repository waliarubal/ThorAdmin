import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ComponentBase } from 'src/app/shared/base.component';
import { IFileSystemEntry } from 'src/app/shared/models/file-system-entry.model';
import { FileSystemService } from 'src/app/shared/services/file-system.service';

@Component({
  selector: 'app-files-delete-entry',
  templateUrl: './delete-entry.component.html',
  providers: [FileSystemService],
})
export class DeleteEntryComponent extends ComponentBase {
  constructor(
    public dialogRef: MatDialogRef<DeleteEntryComponent>,
    @Inject(MAT_DIALOG_DATA) private _entry: IFileSystemEntry,
    private _fsService: FileSystemService
  ) {
    super();
  }

  get Entry() {
    return this._entry;
  }

  DeleteEntry() {
    this.IsBusy = true;
    this.dialogRef.disableClose = true;
    let subscription = this._fsService
      .DeleteEntry(this.Entry)
      .subscribe((result) => {
        this.dialogRef.disableClose = false;
        subscription.unsubscribe();
        this.IsBusy = false;
        this.dialogRef.close(result.Error ? false : result.Data);
      });
  }
}
