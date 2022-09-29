import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  API_CREATE_INSTANCE,
  API_DELETE_INSTANCE,
  API_GET_INSTANCE,
  API_GET_INSTANCES,
} from '../constants';
import { IWordPressInstance } from '../models/word-press-instance.model';
import { ServiceBase } from './base.service';

@Injectable()
export class WordPressService extends ServiceBase {
  constructor(http: HttpClient) {
    super(http);
  }

  GetInstances() {
    return this.Get<IWordPressInstance[]>(API_GET_INSTANCES);
  }

  GetInstance(id: string) {
    return this.Get<IWordPressInstance>(`${API_GET_INSTANCE}/${id}`);
  }

  CreateInstance(instanceName: string) {
    return this.Post<boolean>(`${API_CREATE_INSTANCE}/${instanceName}`, {});
  }

  DeleteInstance(id: string) {
    return this.Delete<boolean>(`${API_DELETE_INSTANCE}/${id}`);
  }
}
