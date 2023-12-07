import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PreferenceRoutingModule } from './preference-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { PreferenceComponent } from './preference.component';


@NgModule({
  declarations: [
    PreferenceComponent
  ],
  imports: [
    CommonModule,
    PreferenceRoutingModule,
    SharedModule,
  ],
})
export class PreferenceModule { }
