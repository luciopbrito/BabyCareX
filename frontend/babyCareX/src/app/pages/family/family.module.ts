import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FamilyRoutingModule } from './family-routing.module';
import { RegisterComponent } from './register/register.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { BabaInfoComponent } from 'src/app/components/baba-info/baba-info.component';
import { ChildrenComponent } from './children/children.component';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';


@NgModule({
  declarations: [
    RegisterComponent,
    DashboardComponent,
    BabaInfoComponent,
    ChildrenComponent,
  ],
  imports: [
    CommonModule,
    FamilyRoutingModule,
    SharedModule
  ],
  providers: [
    {
      provide: MatDialogRef, useValue: {}
    },
    {
      provide: MAT_DIALOG_DATA, useValue: {}
    }
  ]
})
export class FamilyModule { }
