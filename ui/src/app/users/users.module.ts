import { NgModule } from '@angular/core';
import { SignInComponent } from './sign-in/sign-in.component';
import { UsersComponent } from './users.component';
import { UsersRoutingModule } from './users.routing';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';

@NgModule({
  declarations: [UsersComponent, SignInComponent],
  imports: [
    UsersRoutingModule,
    MatFormFieldModule,
    MatCardModule,
    MatDividerModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    CommonModule,
    FormsModule,
  ],
  providers: [],
})
export class UsersModule {}
