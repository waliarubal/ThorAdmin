import { AfterViewInit, Component } from '@angular/core';
import { WordPressService } from '../services/word-press.service';
import { IWordPressInstance } from '../models/word-press-instance';

@Component({
  selector: 'app-blogs',
  templateUrl: './blogs.component.html',
  styleUrls: ['./blogs.component.css'],
  providers: [WordPressService],
})
export class BlogsComponent implements AfterViewInit {
  private _instances: IWordPressInstance[];
  private readonly _columns: string[];

  constructor(private _wpService: WordPressService) {
    this._instances = [];
    this._columns = ['Name', 'Description', 'Created', 'Modified', 'Actions'];
  }

  get Instances(): IWordPressInstance[] {
    return this._instances;
  }

  get Columns(): string[] {
    return this._columns;
  }

  ngAfterViewInit(): void {
    this.GetInstances();
  }

  private GetInstances() {
    let subscription = this._wpService.GetInstances().subscribe((result) => {
      this._instances = result.Data;
      subscription.unsubscribe();
    });
  }
}
