import { Bool } from './constants';

export abstract class ComponentBase {
  protected get IsBusy(): boolean {
    return sessionStorage.getItem('IsBusy') == Bool.Yes;
  }

  protected set IsBusy(value: boolean) {
    sessionStorage.setItem('IsBusy', value ? Bool.Yes : Bool.No);
  }
}
