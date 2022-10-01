import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ComponentBase } from 'src/app/shared/base.component';
import { IWordPressInstance } from 'src/app/shared/models/word-press-instance.model';
import { WordPressService } from 'src/app/shared/services/word-press.service';

@Component({
  selector: 'app-blogs-delete-blog',
  templateUrl: './delete-blog.component.html',
  providers: [WordPressService],
})
export class DeleteBlogComponent extends ComponentBase {
  constructor(
    public dialogRef: MatDialogRef<DeleteBlogComponent>,
    @Inject(MAT_DIALOG_DATA) private _instance: IWordPressInstance,
    private _wpService: WordPressService
  ) {
    super();
  }

  get Instance() {
    return this._instance;
  }

  DeleteInstance() {
    this.IsBusy = true;
    this.dialogRef.disableClose = true;
    let subscription = this._wpService
      .DeleteInstance(this.Instance.Id)
      .subscribe((result) => {
        this.dialogRef.disableClose = false;
        subscription.unsubscribe();
        this.IsBusy = false;
        this.dialogRef.close(result.Error ? false : result.Data);
      });
  }
}
