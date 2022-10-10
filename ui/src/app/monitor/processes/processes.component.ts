import { Component } from '@angular/core';
import { ComponentBase } from 'src/app/shared/base.component';

@Component({
  selector: 'app-monitor-processes',
  templateUrl: './processes.component.html',
})
export class ProcessesComponent extends ComponentBase {
  constructor() {
    super();
  }
}
