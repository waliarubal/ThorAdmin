import { Component } from '@angular/core';
import { ComponentBase } from './shared/base.component';
import { UserService } from './shared/services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent extends ComponentBase {
  constructor(private _userService: UserService) {
    super();
  }

  get IsAuthenticated(): boolean {
    return this._userService.IsAuthenticated;
  }
}
