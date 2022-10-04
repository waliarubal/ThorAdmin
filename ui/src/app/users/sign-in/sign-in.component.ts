import { Component } from '@angular/core';
import { ComponentBase } from 'src/app/shared/base.component';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-users-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css'],
})
export class SignInComponent extends ComponentBase {
  UserName: string = '';
  Password: string = '';
  IsPasswordVisible: boolean = false;

  constructor(private _userService: UserService) {
    super();
  }

  LogIn() {
    this._userService.LogIn(this.UserName, this.Password);
  }
}
