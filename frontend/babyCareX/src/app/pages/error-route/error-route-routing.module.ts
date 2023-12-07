import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ErrorRouteComponent } from './error-route.component';

const routes: Routes = [
  {
    path: '',
    component: ErrorRouteComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ErrorRouteRoutingModule { }
