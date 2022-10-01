import { AfterViewInit, Component } from '@angular/core';
import { WordPressService } from '../shared/services/word-press.service';
import { IWordPressInstance } from '../shared/models/word-press-instance.model';
import { MatDialog } from '@angular/material/dialog';
import { CreateBlogComponent } from './create-blog/create-blog.component';
import { ComponentBase } from '../shared/base.component';
import { DeleteBlogComponent } from './delete-blog/delete-blog.component';

@Component({
  selector: 'app-blogs',
  templateUrl: './blogs.component.html',
  styleUrls: ['./blogs.component.css'],
  providers: [WordPressService],
})
export class BlogsComponent extends ComponentBase implements AfterViewInit {
  private _instances: IWordPressInstance[];
  private readonly _columns: string[];

  constructor(
    private _wpService: WordPressService,
    private _dialogService: MatDialog
  ) {
    super();
    this._instances = [];
    this._columns = ['Name', 'Description', 'Created', 'Modified', 'Actions'];
    this.IsBusy = true;
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
    this.IsBusy = true;
    let subscription = this._wpService.GetInstances().subscribe((result) => {
      this._instances = result.Data;
      subscription.unsubscribe();
      this.IsBusy = false;
    });
  }

  CreateInstance() {
    let dialogRef = this._dialogService.open(CreateBlogComponent);
    dialogRef.afterClosed().subscribe((result) => {
      if (result == true) this.GetInstances();
    });
  }

  DeleteInstance(instance: IWordPressInstance) {
    let dialogRef = this._dialogService.open(DeleteBlogComponent, {
      data: instance,
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result == true) this.GetInstances();
    });
  }
}
