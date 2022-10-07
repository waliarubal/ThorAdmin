import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  API_CREATE_ENTRY,
  API_DELETE_ENTRY,
  API_DOWNLOAD_ENTRY,
  API_GET_ENTRIES,
  API_GET_PARENT_ENTRY,
  API_RENAME_ENTRY,
} from '../constants';
import { IFileSystemEntry } from '../models/file-system-entry.model';
import { ServiceBase } from './base.service';

@Injectable()
export class FileSystemService extends ServiceBase {
  constructor(http: HttpClient) {
    super(http);
  }

  GetParentEntry(directory: string) {
    return this.Get<IFileSystemEntry>(
      `${API_GET_PARENT_ENTRY}?directory=${directory}`
    );
  }

  GetEnteries(directory: string) {
    return this.Get<IFileSystemEntry[]>(
      `${API_GET_ENTRIES}?directory=${directory}`
    );
  }

  DeleteEntry(entry: IFileSystemEntry) {
    return this.Delete<boolean>(API_DELETE_ENTRY, entry);
  }

  RenameEntry(entry: IFileSystemEntry, newName: string) {
    return this.Patch<boolean>(`${API_RENAME_ENTRY}?newName=${newName}`, entry);
  }

  CreateEntry(entry: IFileSystemEntry) {
    return this.Post<boolean>(`${API_CREATE_ENTRY}`, entry);
  }

  DownloadEntry(entry: IFileSystemEntry) {
    return this.Http.post(API_DOWNLOAD_ENTRY, entry, {
      responseType: 'arraybuffer',
    });
  }
}
