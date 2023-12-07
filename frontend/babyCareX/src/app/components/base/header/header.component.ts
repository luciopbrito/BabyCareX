import { BabyCareXAppService } from './../../../shared/base/babyCareXApp/baby-care-xapp.service';
import { Component, OnInit } from '@angular/core';
import { identifyObject } from 'src/app/shared/helpers/identify-objects';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {
  public isLogon = false;
  public name = null

  constructor(
    private babyCareXAppService: BabyCareXAppService,
  ) { }

  ngOnInit(): void {
    this.babyCareXAppService.account.asObservable().subscribe(e => {
      if (e) {
        this.isLogon = true;
        this.name = identifyObject.isFamilyOBject(e) ? this.showPrefixFamily(e.familyName) : e.name
      }
      else {
        this.isLogon = false;
        this.name = null
      }
    });
  }

  public showPrefixFamily(familyName: string) {
    const arrayTest = [
      familyName.toLowerCase().includes('family'),
      familyName.toLowerCase().includes('família'),
      familyName.toLowerCase().includes('familia')
    ];

    if (arrayTest.some(e => e == true)) {
      return familyName
    }
    else {
      return `Família ${familyName}`
    }
  }

  public logout() {
    this.babyCareXAppService.logout();
  }

}
