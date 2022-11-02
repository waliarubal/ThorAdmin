import { AfterViewInit, Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ComponentBase } from 'src/app/shared/base.component';
import { IWordPressInstance } from 'src/app/shared/models/word-press-instance.model';
import { WordPressService } from 'src/app/shared/services/word-press.service';

@Component({
  selector: 'app-blogs-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css'],
  providers: [WordPressService],
})
export class UsersComponent extends ComponentBase implements AfterViewInit {
  private _instance: IWordPressInstance;

  constructor(
    private _route: ActivatedRoute,
    private _wpService: WordPressService
  ) {
    super();
    this._instance = <IWordPressInstance>{};
    this.IsBusy = true;
  }

  get Instance() {
    return this._instance;
  }

  ngAfterViewInit(): void {
    let id = <string>this._route.snapshot.paramMap.get('id');

    let sub = this._wpService.GetInstance(id).subscribe((response) => {
      this._instance = response.Data;
      sub.unsubscribe();
      this.GetUsers();
    });
  }

  GetUsers() {
    this.IsBusy = true;
  }
}
