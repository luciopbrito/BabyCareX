import { BabyCareXAppService, MenuItem } from './../../../shared/base/babyCareXApp/baby-care-xapp.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss'],
})
export class FooterComponent implements OnInit {
  public menu: MenuItem[] = [];
  constructor(private babyCareXAppService: BabyCareXAppService) { }

  ngOnInit(): void {
    this.babyCareXAppService.menu.asObservable().subscribe(e => this.menu = e)
  }

  public yearCurrent() {
    return new Date().getFullYear();
  };
}
