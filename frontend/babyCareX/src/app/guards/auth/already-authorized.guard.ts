import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { User } from 'src/app/enums/user';
import { LocalStorageService } from 'src/app/services/localStorage/local-storage.service';
import { BabyCareXAppService } from 'src/app/shared/base/babyCareXApp/baby-care-xapp.service';

export const alreadyAuthorizedGuard: CanActivateFn = (route, state) => {
  const account = inject(LocalStorageService).getItem('login');

  if (account) {
    if (inject(BabyCareXAppService).getRootRoute().includes(User.Family)) inject(Router).navigateByUrl('/family/login');
    if (inject(BabyCareXAppService).getRootRoute().includes(User.Baba)) inject(Router).navigateByUrl('/baba/login');
    return false;
  }
  else {
    return true;
  }
};
