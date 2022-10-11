import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { API_GET_PROCESSES, API_KILL_PROCESSES } from '../constants';
import { IProcessInfo } from '../models/process-info.model';
import { ServiceBase } from './base.service';

@Injectable()
export class MachineInfoService extends ServiceBase {
  constructor(http: HttpClient) {
    super(http);
  }

  GetProcesses() {
    return this.Get<IProcessInfo[]>(API_GET_PROCESSES);
  }

  KillProcess(id: number) {
    return this.Delete<boolean>(`${API_KILL_PROCESSES}/${id}`);
  }
}
