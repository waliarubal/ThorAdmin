import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { API_GET_ENTRIES, API_GET_PARENT_ENTRY } from '../constants';
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
}
