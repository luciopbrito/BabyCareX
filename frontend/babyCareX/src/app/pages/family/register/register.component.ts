import { BabyCareXAppService } from './../../../shared/base/babyCareXApp/baby-care-xapp.service';
import { NotificationService } from './../../../services/notification/notification.service';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Family, FamilyBase } from 'src/app/interfaces/family.interface';
import { FamilyService } from 'src/app/services/family/family.service';
import { HttpErrorResponse } from '@angular/common/http';
import { User } from 'src/app/enums/user';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  public type: User;
  public isFamily = false;
  public isBaba = false;

  public formFamily = new FormGroup({
    familyName: new FormControl('', [Validators.required]),
    fatherName: new FormControl<null | string>(null),
    motherName: new FormControl<null | string>(null),
    phoneNumber: new FormControl<number | null>(null, [Validators.required]),
    email: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
    // children: Child[],
    // schedules: Schedule[]
  })

  public formBaba = new FormGroup({

  })


  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private familyService: FamilyService,
    private notificationService: NotificationService,
    private babyCareXAppService: BabyCareXAppService,
  ) { }

  ngOnInit(): void {
    if(this.babyCareXAppService.getRootRoute() == User.Family) {
      this.isFamily = true;
      this.type = User.Family;
    }
    if (this.babyCareXAppService.getRootRoute() == User.Baba) {
      this.isBaba = true;
      this.type = User.Family;
    }
  }

  public saveFormFamily() {
    if (this.formFamily.valid) {
      let form = {
        ...this.formFamily.getRawValue(),
        children: [],
        schedules: []
      } as FamilyBase

      this.familyService.create(form).subscribe({
        next: (resp) => {
          this.notificationService.success('cadastro efetuado com sucesso');
          this.babyCareXAppService.login<Family>(resp);
          this.babyCareXAppService.hasPreference() ? this.router.navigateByUrl('/family/feed') : this.router.navigateByUrl('/preference', { state: { type: User.Family } })
        },
        error: (respError: HttpErrorResponse) => {
          this.familyService.tooltipErrorMessage(respError)
        },
      })
    }
  }
}
