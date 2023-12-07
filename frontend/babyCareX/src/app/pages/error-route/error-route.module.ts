import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ErrorRouteRoutingModule } from './error-route-routing.module';
import { ErrorRouteComponent } from './error-route.component';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
  declarations: [
    ErrorRouteComponent
  ],
  imports: [
    CommonModule,
    ErrorRouteRoutingModule,
    SharedModule,
  ]
})
export class ErrorRouteModule { }
