import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-blogs-create-blog',
  templateUrl: './create-blog.component.html',
})
export class CreateBlogComponent {
  InstanceName: string = '';

  constructor(public dialogRef: MatDialogRef<CreateBlogComponent>) {}

  CreateInstance() {
    this.dialogRef.close();
  }
}
