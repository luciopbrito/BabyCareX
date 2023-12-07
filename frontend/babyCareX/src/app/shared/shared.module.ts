import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSelectModule } from "@angular/material/select";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { MatFormFieldModule } from "@angular/material/form-field";

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ChoosePreferenceComponent } from '../components/base/choose-preference/choose-preference.component';
import { ModalConfirmComponent } from '../components/modals/modal-confirm/modal-confirm.component';
import { MatDialogModule } from '@angular/material/dialog';
import { FormChildComponent } from '../components/forms/form-child/form-child.component';
import { FormAccountComponent } from '../componets/forms/form-account/form-account.component';
import { ModalChangePasswordComponent } from '../components/modals/modal-change-password/modal-change-password.component';
import { ModalConfirmPasswordComponent } from '../components/modals/modal-confirm-password/modal-confirm-password.component';

@NgModule({
  declarations: [ChoosePreferenceComponent, ModalConfirmComponent, FormChildComponent, FormAccountComponent, ModalChangePasswordComponent, ModalConfirmPasswordComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatDialogModule,
    MatIconModule,
    MatTooltipModule,
    MatSelectModule,
    MatFormFieldModule,
    MatInputModule,
  ],
  exports: [
    MatIconModule,
    MatButtonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
    ChoosePreferenceComponent,
  ],
})
export class SharedModule {}
