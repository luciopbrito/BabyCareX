import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './register/register.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { authorizedAsFamilyGuard } from 'src/app/guards/family/authorized-as-family.guard';
import { alreadyAuthorizedGuard } from 'src/app/guards/auth/already-authorized.guard';
import { ChildrenComponent } from './children/children.component';
import { FormChildComponent } from 'src/app/components/forms/form-child/form-child.component';
import { User } from 'src/app/enums/user';

const routes: Routes = [
  {
    pathMatch: 'full',
    path: '',
    redirectTo: 'dashboard'
  },
  {
    path: 'login',
    canMatch: [alreadyAuthorizedGuard],
    loadChildren: () => import('../login/login.module').then(m => m.LoginModule)
  },
  {
    path: 'register',
    canActivate: [alreadyAuthorizedGuard],
    component: RegisterComponent
  },
  {
    path: 'dashboard',
    canActivate: [authorizedAsFamilyGuard],
    component: DashboardComponent,
  },
  {
    path: 'children',
    canActivate: [authorizedAsFamilyGuard],
    component: ChildrenComponent,
  },
  {
    path: 'children/add',
    canActivate: [authorizedAsFamilyGuard],
    component: FormChildComponent
  },
  {
    path: 'account',
    canActivate: [authorizedAsFamilyGuard],
    loadChildren: () => import('../account/account.module').then(m => m.AccountModule),
    data: {
      type: User.Family,
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FamilyRoutingModule { }
