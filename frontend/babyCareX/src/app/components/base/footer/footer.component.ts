import { Component } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss'],
})
export class FooterComponent {
  public menu: {href: string, name: string}[] = [
    {
      href: "/inicio",
      name: "inicio"
    },
    {
      href: "/login",
      name: "login"
    },
    {
      href: "/login",
      name: "login"
    },
    {
      href: "/sobre",
      name: "sobre nos"
    },
    {
      href: "/conta",
      name: "conta"
    },
  ]

  public yearCurrent = () => {
    return new Date().getFullYear();
  };
}
