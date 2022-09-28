import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { ComponentBase } from 'src/app/shared/base.component';
import { WordPressService } from 'src/app/shared/services/word-press.service';

@Component({
  selector: 'app-blogs-create-blog',
  templateUrl: './create-blog.component.html',
  providers: [WordPressService],
})
export class CreateBlogComponent extends ComponentBase {
  InstanceName: string = '';

  constructor(
    public dialogRef: MatDialogRef<CreateBlogComponent>,
    private _wpService: WordPressService
  ) {
    super();
  }

  CreateInstance() {
    this.IsBusy = true;
    this.dialogRef.disableClose = true;
    let subscription = this._wpService
      .CreateInstance(this.InstanceName)
      .subscribe((result) => {
        this.dialogRef.disableClose = false;
        subscription.unsubscribe();
        this.IsBusy = false;
        this.dialogRef.close(result.Error ? false : true);
      });
  }
}
