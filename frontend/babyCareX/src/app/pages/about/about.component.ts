import { identifyObject } from './../../shared/helpers/identify-objects';
import { LocalStorageService } from './../../services/localStorage/local-storage.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.scss']
})
export class AboutComponent {
  public showChooseUser = true;

  constructor(
    private _localStorageService: LocalStorageService,
  ) {
    const login = this._localStorageService.getItem('login');
    if (login != null) {
      const isFamily = identifyObject.isFamilyOBject(login);
      const isBaba = identifyObject.isFamilyOBject(login);
      if (isFamily || isBaba) {
        this.showChooseUser = false
      }
    }
  }
}
