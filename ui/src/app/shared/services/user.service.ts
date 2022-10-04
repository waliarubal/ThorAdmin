import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ServiceBase } from './base.service';
import { Bool } from '../constants';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class UserService extends ServiceBase {
  constructor(http: HttpClient, private _router: Router) {
    super(http);
  }

  get IsAuthenticated(): boolean {
    return sessionStorage.getItem('IsAuthenticated') == Bool.Yes;
  }

  async LogIn(userName: string, password: string) {
    sessionStorage.setItem('IsAuthenticated', Bool.Yes);
    await this._router.navigateByUrl('/blogs');
  }

  async LogOff() {
    sessionStorage.removeItem('IsAuthenticated');
    await this._router.navigateByUrl('/users/sign-in');
  }
}
