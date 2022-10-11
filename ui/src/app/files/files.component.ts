import { AfterViewInit, Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { saveAs } from 'file-saver';
import { ComponentBase } from '../shared/base.component';
import { PATH_SEPARATOR } from '../shared/constants';
import { IFileSystemEntry } from '../shared/models/file-system-entry.model';
import { FileSystemService } from '../shared/services/file-system.service';
import { CreateEntryComponent } from './create-entry/create-entry.component';
import { DeleteEntryComponent } from './delete-entry/delete-entry.component';
import { RenameEntryComponent } from './rename-entry/rename-entry.component';
import { UploadEntryComponent } from './upload-entry/upload-entry.component';

@Component({
  selector: 'app-files',
  templateUrl: './files.component.html',
  styleUrls: ['./files.component.css'],
  providers: [FileSystemService],
})
export class FilesComponent extends ComponentBase implements AfterViewInit {
  readonly PATH_SEPARATOR = PATH_SEPARATOR;

  private _entries: IFileSystemEntry[];
  private _entry: IFileSystemEntry | null;
  private readonly _columns: string[];

  constructor(
    private _fsService: FileSystemService,
    private _dialogService: MatDialog
  ) {
    super();
    this._entry = null;
    this._entries = [];
    this._columns = ['Name', 'Created', 'Modified', 'Type', 'Size', 'Actions'];
    this.IsBusy = true;
  }

  get Entry() {
    return this._entry;
  }

  get Entries(): IFileSystemEntry[] {
    return this._entries;
  }

  get DirectoryCount(): number {
    return this._entries.filter((r) => r.IsDirectory).length;
  }

  get FileCount(): number {
    return this._entries.length - this.DirectoryCount;
  }

  get Columns(): string[] {
    return this._columns;
  }

  ngAfterViewInit(): void {
    this.GetEntries(this.Entry);
  }

  GetParentEntry() {
    if (!this.Entry || !this.Entry.IsDirectory || !this.Entry.Path) return;

    this.IsBusy = true;
    let subscription = this._fsService
      .GetParentEntry(this.Entry.Path)
      .subscribe((result) => {
        let entry = result.Data;
        subscription.unsubscribe();
        this.GetEntries(entry);
      });
  }

  GetEntries(entry: IFileSystemEntry | null) {
    if (entry && !entry.IsDirectory) return;

    this._entry = entry;
    let path = entry == null ? '' : entry.Path;

    this.IsBusy = true;
    let subscription = this._fsService.GetEnteries(path).subscribe((result) => {
      this._entries = result.Data;
      subscription.unsubscribe();
      this.IsBusy = false;
    });
  }

  DeleteEntry(entry: IFileSystemEntry) {
    let dialogRef = this._dialogService.open(DeleteEntryComponent, {
      data: entry,
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result == true) this.GetEntries(this.Entry);
    });
  }

  RenameEntry(entry: IFileSystemEntry) {
    let dialogRef = this._dialogService.open(RenameEntryComponent, {
      data: entry,
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result == true) this.GetEntries(this.Entry);
    });
  }

  CreateEntry() {
    let dialogRef = this._dialogService.open(CreateEntryComponent, {
      data: <IFileSystemEntry>{
        IsDirectory: false,
        Name: '',
        Path: this.Entry ? this.Entry.Path : PATH_SEPARATOR,
      },
      minWidth: '24em',
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result == true) this.GetEntries(this.Entry);
    });
  }

  DownloadFile(entry: IFileSystemEntry) {
    this._fsService.DownloadEntry(entry).subscribe((result) => {
      let blob = new Blob([<any>result]);
      saveAs(blob, entry.Name);
    });
  }

  UploadEntry() {
    let dialogRef = this._dialogService.open(UploadEntryComponent, {
      data: <IFileSystemEntry>{
        IsDirectory: false,
        Name: '',
        Path: this.Entry ? this.Entry.Path : PATH_SEPARATOR,
      },
      minWidth: '24em',
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result == true) this.GetEntries(this.Entry);
    });
  }
}
