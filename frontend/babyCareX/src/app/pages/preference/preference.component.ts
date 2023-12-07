import { LocalStorageService } from 'src/app/services/localStorage/local-storage.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { OptionsView } from 'src/app/components/base/choose-preference/choose-preference.component';
import { User } from 'src/app/enums/user';

export type Preference = {
  emergencyBaba: boolean,
  qualifiesBabas: boolean,
}

@Component({
  selector: 'app-preference',
  templateUrl: './preference.component.html',
  styleUrls: ['./preference.component.scss']
})
export class PreferenceComponent implements OnInit {
  public type: string | null = null;
  public isFamily = false;
  public isBaba = false;
  public familyOptions: OptionsView[] = [
    {
      question: "seus filhos precisam de uma baba com urgência?",
      btnYes: () => {
        this.setEmergencyBaba(true);
      },
      btnNot: () => {
        this.setEmergencyBaba(false);
      },
    },
    {
      question: "deseja babas com qualificações para crianças especiais?",
      btnYes: () => {
        this.setQualifiesBabas(true);
        this.redirectFlowFamily()
      },
      btnNot: () => {
        this.setQualifiesBabas(false);
        this.redirectFlowFamily()
      }
    }
  ]

  constructor(
    private _activatedRoute: ActivatedRoute,
    private _router: Router,
    private _localStorageService: LocalStorageService,
  ) {
    const type = this._activatedRoute.snapshot.queryParamMap.get('type');
    const access = type == User.Family || type == User.Baba;
    access ? this.type = this._activatedRoute.snapshot.queryParamMap.get('type') : this._router.navigate(['/not-authorized'])
  }

  ngOnInit(): void {
      if (this.type == User.Family) this.isFamily = true;
      if (this.type == User.Baba) this.isBaba = true;
  }

  private setQualifiesBabas(setup: boolean) {
    const preferenceFromLocalStorage = this._localStorageService.getItem('preference');
    if (preferenceFromLocalStorage) {
      const pref = preferenceFromLocalStorage as Preference
      this._localStorageService.setItem('preference', { ...pref, qualifiesBabas: setup } as Preference);
    }
    else {
      this._localStorageService.setItem('preference', { qualifiesBabas: setup } as Preference);
    }
  }

  private setEmergencyBaba(setup: boolean) {
    const preferenceFromLocalStorage = this._localStorageService.getItem('preference');
    if (preferenceFromLocalStorage) {
      const pref = preferenceFromLocalStorage as Preference
      this._localStorageService.setItem('preference', { ...pref, emergencyBaba: setup } as Preference);
    }
    else {
      this._localStorageService.setItem('preference', { emergencyBaba: setup } as Preference);
    }
  }

  private redirectFlowFamily() {
    const redirect = this._activatedRoute.snapshot.queryParamMap.get('redirect');
    if (redirect) {
      this._router.navigateByUrl(redirect);
    }
    else {
      this._router.navigateByUrl('/family/dashboard')
    }
  }
}
