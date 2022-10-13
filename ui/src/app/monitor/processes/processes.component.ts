import { AfterViewInit, Component, EventEmitter, Output } from '@angular/core';
import { ComponentBase } from 'src/app/shared/base.component';
import { IProcessInfo } from 'src/app/shared/models/process-info.model';
import { MachineInfoService } from 'src/app/shared/services/machine-info.service';

@Component({
  selector: 'app-monitor-processes',
  templateUrl: './processes.component.html',
  styleUrls: ['./processes.component.css'],
  providers: [MachineInfoService],
})
export class ProcessesComponent extends ComponentBase implements AfterViewInit {
  private _processes: IProcessInfo[];
  private readonly _columns: string[];

  @Output() readonly OnGetProcesses: EventEmitter<number>;

  constructor(private _machineInfoService: MachineInfoService) {
    super();
    this._processes = [];
    this._columns = ['Id', 'Name', 'Memory', 'Cpu', 'Actions'];
    this.OnGetProcesses = new EventEmitter();
    this.IsBusy = true;
  }

  get Processes() {
    return this._processes;
  }

  get Columns(): string[] {
    return this._columns;
  }

  ngAfterViewInit(): void {
    this.GetProcesses();
  }

  GetProcesses() {
    this.IsBusy = true;
    let sub = this._machineInfoService.GetProcesses().subscribe((response) => {
      this._processes = response.Data;
      sub.unsubscribe();
      this.OnGetProcesses.emit(this.Processes.length);
      this.IsBusy = false;
    });
  }

  KillProcess(id: number) {
    this.IsBusy = true;
    let sub = this._machineInfoService.KillProcess(id).subscribe((response) => {
      sub.unsubscribe();
      if (response.Data) this.GetProcesses();
      else this.IsBusy = false;
    });
  }
}
