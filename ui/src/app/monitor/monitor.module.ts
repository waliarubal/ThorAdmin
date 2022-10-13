import { NgModule } from '@angular/core';
import { MonitorRoutingModule } from './monitor.routing';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { CommonModule } from '@angular/common';

import { ProcessesComponent } from './processes/processes.component';
import { MonitorComponent } from './monitor.component';
import { SharedModule } from '../shared/shared.module';

@NgModule({
  declarations: [MonitorComponent, ProcessesComponent],
  imports: [
    CommonModule,
    MonitorRoutingModule,
    MatIconModule,
    MatToolbarModule,
    MatCardModule,
    MatButtonModule,
    MatTableModule,
    SharedModule,
  ],
})
export class MonitorModule {}
