import {
  HttpClient,
  HttpErrorResponse,
  HttpEventType,
  HttpHeaders,
} from '@angular/common/http';
import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ComponentBase } from 'src/app/shared/base.component';
import { API_UPLOAD_ENTRY } from 'src/app/shared/constants';
import { IFileSystemEntry } from 'src/app/shared/models/file-system-entry.model';

@Component({
  selector: 'app-files-upload-entry',
  templateUrl: './upload-entry.component.html',
})
export class UploadEntryComponent extends ComponentBase {
  private _file: File | undefined;
  private _progress: number = 0;

  constructor(
    public dialogRef: MatDialogRef<UploadEntryComponent>,
    private _http: HttpClient,
    @Inject(MAT_DIALOG_DATA) private _entry: IFileSystemEntry
  ) {
    super();
  }

  get Entry() {
    return this._entry;
  }

  get Progress() {
    return this._progress;
  }

  FileSelected(files: FileList | null) {
    this._entry.Name = '';
    this._progress = 0;

    if (!files || files.length == 0) return;

    this._file = files[0];
    this._entry.Name = this._file.name;
  }

  UploadEntry() {
    this.IsBusy = true;
    this.dialogRef.disableClose = true;

    let request = new FormData();
    request.append('entry', JSON.stringify(this.Entry));
    request.append('file', <File>this._file, this.Entry.Name);

    this._http
      .post(API_UPLOAD_ENTRY, request, {
        reportProgress: true,
        observe: 'events',
      })
      .subscribe({
        next: (event) => {
          if (event.type === HttpEventType.UploadProgress)
            this._progress = Math.round(
              (100 * event.loaded) / <number>event.total
            );
          else if (event.type === HttpEventType.Response) {
            this.dialogRef.disableClose = false;
            this.IsBusy = false;
            this.dialogRef.close(true);
          }
        },
        error: (error: HttpErrorResponse) => console.log(error),
      });
  }
}
