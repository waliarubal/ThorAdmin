import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IApiResponse } from '../models/api-response.model';

@Injectable()
export abstract class ServiceBase {
  constructor(private _http: HttpClient) {}

  protected Get<TData>(url: string) {
    return this._http.get<IApiResponse<TData>>(url);
  }

  protected Post<TData>(url: string, payload: any) {
    return this._http.post<IApiResponse<TData>>(url, payload);
  }

  protected Put<TData>(url: string, payload: any) {
    return this._http.put<IApiResponse<TData>>(url, payload);
  }

  protected Delete<TData>(url: string) {
    return this._http.delete<IApiResponse<TData>>(url);
  }
}
