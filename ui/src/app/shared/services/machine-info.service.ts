import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ServiceBase } from './base.service';

@Injectable()
export class MachineInfoService extends ServiceBase {
  constructor(http: HttpClient) {
    super(http);
  }
}
