import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { Family } from 'src/app/interfaces/family.interface';
import { LocalStorageService } from 'src/app/services/localStorage/local-storage.service';
import { BabyCareXAppService } from 'src/app/shared/base/babyCareXApp/baby-care-xapp.service';
import { identifyObject } from 'src/app/shared/helpers/identify-objects';

export const authorizedAsFamilyGuard: CanActivateFn = (route, state) => {
  const userFamily = inject(LocalStorageService).getItem('login');

  if (userFamily) {
    const authorized = identifyObject.isFamilyOBject(userFamily)
    if (!authorized) {
      inject(Router).navigateByUrl('/not-authorized');
    }
    else {
      inject(BabyCareXAppService).login<Family>(userFamily as Family);
    }

    return authorized;
  }
  else {
    inject(Router).navigate(['/family/login'], { queryParams: { redirect: state.url}});
    return false
  }
};
