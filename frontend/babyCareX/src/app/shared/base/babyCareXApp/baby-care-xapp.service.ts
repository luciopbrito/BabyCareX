import { BehaviorSubject } from 'rxjs';
import { LocalStorageService } from './../../../services/localStorage/local-storage.service';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

export type MenuItem = { href: string; name: string };

@Injectable({
  providedIn: 'root',
})
export class BabyCareXAppService {

  private basicMenu: MenuItem[] = [
    {
      href: '/sobre',
      name: 'sobre nos',
    },
  ]

  private menuNotRegister: MenuItem[] = [
    {
      href: `/family/login`,
      name: 'login',
    },
  ];

  private menuLogonFamily: MenuItem[] = [
    {
      href: '/family/dashboard',
      name: 'dashboard',
    },
    {
      href: '/family/children',
      name: 'crianças',
    },
    {
      href: '/family/account',
      name: 'conta',
    },
  ];

  private menuLogonBaba: MenuItem[] = [
    {
      href: '/baba/baba-courses',
      name: 'cursos',
    },
    {
      href: '/baba/baba-capacities',
      name: 'capacidades',
    },
  ];

  public menu = new BehaviorSubject<MenuItem[]>([
    ...this.basicMenu
  ]);

  public account = new BehaviorSubject<any>(null);

  constructor(
    private localStorageService: LocalStorageService,
    private router: Router,
    ) {
    this.menu.next([...this.menu.value, ...this.menuNotRegister].reverse());
  }

  public login<T>(data: T) {
    if (!this.localStorageService.getItem('login')) {
      this.localStorageService.removeItem('login');
    }

    this.flowLogin<T>(data);
  };

  public logout() {
    let routeToRedirect = '';

    this.localStorageService.removeItem('login');
    this.localStorageService.removeItem('preference');
    this.account.next(null)
    this.menu.next(
      [
        ...this.menu.value.filter((e) => e.name == 'sobre nos'),
        ...this.menuNotRegister,
      ].reverse(),
    );

    this.router.navigateByUrl(`/sobre`)
  };

  public hasPreference(): boolean {
    return this.localStorageService.getItem('preference') ? true : false;
  }

  /**
   * function to get the first route in the current url.
   *
   * for example, your current url is 'http://localhost:4200/family/login'.
   *
   * This function return 'family'.
   */
  public getRootRoute(): string {
    return location.href.split('/')[3];
  }

  private flowLogin<T>(value: T) {
    this.localStorageService.setItem('login', value);
    this.account.next(value);
    this.menu.next([...this.menuLogonFamily, ...this.basicMenu]);
  };
}
