import { AfterViewInit, Component } from '@angular/core';
import { WordPressService } from '../services/word-press.service';
import { IWordPressInstance } from '../models/word-press-instance';
import { MatDialog } from '@angular/material/dialog';
import { CreateBlogComponent } from './create-blog/create-blog.component';

@Component({
  selector: 'app-blogs',
  templateUrl: './blogs.component.html',
  styleUrls: ['./blogs.component.css'],
  providers: [WordPressService],
})
export class BlogsComponent implements AfterViewInit {
  private _instances: IWordPressInstance[];
  private readonly _columns: string[];

  constructor(
    private _wpService: WordPressService,
    private _dialogService: MatDialog
  ) {
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

  GetInstances() {
    let subscription = this._wpService.GetInstances().subscribe((result) => {
      this._instances = result.Data;
      subscription.unsubscribe();
    });
  }

  CreateInstance() {
    let dialogRef = this._dialogService.open(CreateBlogComponent);
    dialogRef.afterClosed().subscribe((result) => {});
  }
}
