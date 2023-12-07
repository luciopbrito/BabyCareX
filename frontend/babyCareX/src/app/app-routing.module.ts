import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BaseStructureComponent } from './components/base/base-structure/base-structure.component';

const routes: Routes = [
  {
    pathMatch: 'full',
    path: '',
    redirectTo: 'sobre'
  },
  {
    path: 'family',
    component: BaseStructureComponent,
    loadChildren: () => import('./pages/family/family.module').then(m => m.FamilyModule)
  },
  {
    path: 'not-authorized',
    loadChildren: () => import('./pages/error-route/error-route.module').then(m => m.ErrorRouteModule),
    data: {
      message: "Acesso Restrito"
    }
  },
  {
    path: 'preference',
    loadChildren: () => import('./pages/preference/preference.module').then(m => m.PreferenceModule),
  },
  {
    path: 'sobre',
    component: BaseStructureComponent,
    loadChildren: () => import('./pages/about/about.module').then(m => m.AboutModule)
  },
  {
    path: "**",
    loadChildren: () => import('./pages/error-route/error-route.module').then(m => m.ErrorRouteModule),
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
