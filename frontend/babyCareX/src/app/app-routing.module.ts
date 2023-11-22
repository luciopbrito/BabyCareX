import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ErrorRouteComponent } from './components/base/error-route/error-route.component';
import { BaseStructureComponent } from './components/base/base-structure/base-structure.component';

const routes: Routes = [
  {
    path: '',
    component: BaseStructureComponent,
    children: [
      // {
      //   path: 'home',
      //   canActivate:
      //   component: ,
      // }
    ]
  },
  {
    path: "**",
    component: ErrorRouteComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
