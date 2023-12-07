import { BabyCareXAppService } from './../../shared/base/babyCareXApp/baby-care-xapp.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from 'src/app/enums/user';
import { Family } from 'src/app/interfaces/family.interface';
import { FamilyService } from 'src/app/services/family/family.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  public form = this.formBuilder.group({
    email: ['', Validators.required],
    password: ['', Validators.required],
  });

  private type: User;
  public registerRoute = '';

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private familyService: FamilyService,
    private babyCareXAppService: BabyCareXAppService,
    private router: Router
  ) { }

  ngOnInit(): void {
    if (this.babyCareXAppService.getRootRoute() == User.Family) this.type = User.Family;

    if (this.babyCareXAppService.getRootRoute() == User.Baba) this.type = User.Baba;

    this.registerRoute = `/${this.babyCareXAppService.getRootRoute()}/register`;
  }

  public save() {
    if (this.form.valid) {
      if (this.type == User.Family) {
        this.familyService
          .login(this.form.value.email!, this.form.value.password!)
          .subscribe({
            next: (resp) => {
              this.babyCareXAppService.login<Family>(resp);

              const redirect = this.route.snapshot.queryParamMap.get('redirect');

              if (redirect) {
                this.babyCareXAppService.hasPreference() ? this.router.navigateByUrl(redirect) : this.router.navigate(['/preference'], {queryParams: { type: User.Family, redirect: redirect}});
              }
              else {
                this.babyCareXAppService.hasPreference() ? this.router.navigateByUrl('family/dashboard') : this.router.navigate(['/preference'], {queryParams: { type: User.Family}});
              }

            },
            error: (respError) =>
              this.familyService.tooltipErrorMessage(respError),
          });
      }

      if (this.type == User.Baba) {
        // TODO: implement flow baba
      }
    }
  };

  public redirectRegister() {
    this.router.navigateByUrl(this.registerRoute, { state: { type: this.type }})
  }
}
