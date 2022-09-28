import { Component } from '@angular/core';
import { ComponentBase } from '../shared/base.component';

@Component({
  selector: 'app-not-found',
  templateUrl: './not-found.component.html',
})
export class NotFoundComponent extends ComponentBase {
  constructor(){
    super();
    this.IsBusy = false;
  }
}