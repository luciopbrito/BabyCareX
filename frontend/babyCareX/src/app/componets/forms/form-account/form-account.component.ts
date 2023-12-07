import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FamilyService } from 'src/app/services/family/family.service';
import { Family } from 'src/app/interfaces/family.interface';
import { Baba } from 'src/app/interfaces/baba.interface';
import { ModalConfirmComponent, ModalConfirmComponentData } from 'src/app/components/modals/modal-confirm/modal-confirm.component';
import { ModalConfirmPasswordComponent, ModalConfirmPasswordComponentData } from 'src/app/components/modals/modal-confirm-password/modal-confirm-password.component';
import { User } from 'src/app/enums/user';
import { NotificationService } from 'src/app/services/notification/notification.service';

export type FormAccountComponentData = {
  familyId?: number,
  babaId?: number,
  edition: boolean;
};

@Component({
  selector: 'app-form-account',
  templateUrl: './form-account.component.html',
  styleUrls: ['./form-account.component.scss']
})
export class FormAccountComponent implements OnInit{
  public isFamily = true;
  public isModal: boolean;
  public edition: boolean;
  public security: boolean;
  public oldStateOfAccount: Family | Baba

  public formFamily = new FormGroup({
    familyName: new FormControl<string | null>(null, [Validators.required]),
    fatherName: new FormControl<string | null>(null),
    motherName: new FormControl<string | null>(null),
    phoneNumber: new FormControl<number | null>(null, [Validators.required]),
    email: new FormControl<string | null>(null, [Validators.required]),
  });

  constructor(
    private _familyService: FamilyService,
    private _dialog: MatDialog,
    private _notificationService: NotificationService,
    @Inject(MAT_DIALOG_DATA) public _dataFromModal: FormAccountComponentData,
    private _dialogRef: MatDialogRef<FormAccountComponent>,
  ) {
    if (_dataFromModal.edition) {
      this.isModal = true;
      this.edition = _dataFromModal.edition;
    }
  }

  ngOnInit(): void {
    if (this.edition && this.isModal) {
      this._familyService.getById(this._dataFromModal.familyId!).subscribe(e => {
        this.oldStateOfAccount = e;
        this.formFamily.patchValue(e);
      })
    }
  }

  private loadFormToSend() {
    if (this.formFamily.valid && this.isModal && this.edition && this.security) {
      const body = {
        ...this.oldStateOfAccount,
        ...this.formFamily.getRawValue()
      } as Family

      this._familyService.update(body.id, body).subscribe({
        next: (resp) => {
          this._notificationService.success('alteração salva com sucesso')
          this._dialogRef.close({changed: true})
        },
        error: (respError) => {
          this._familyService.tooltipErrorMessage(respError);
        }
      })
    }

    if (this.formFamily.valid && !this.isModal && !this.edition && this.security) {

    }
  }

  public saveForm() {
    this.securityChanged();
  }

  public closeModal() {
    this._dialogRef.close()
  }

  /**
   * function to handle the updates of account information.
   * @returns true if accept modal of confirmation and modal confirms the password returns true too.
   */
  public securityChanged() {
    if (this.formFamily.value.email != this.oldStateOfAccount.email) {
      const dialogRef = this._dialog.open(ModalConfirmComponent, {
        panelClass: 'modal-confirm',
        data: {
          title: 'Mudança de informação de Acesso!',
          message: 'É necessário que utilize a senha novamente para que esta ação seja concluída',
        } as ModalConfirmComponentData
      });

      dialogRef.afterClosed().subscribe(e => {
        if (e) {
          if (e.confirm) this.modalConfirmPassword();
        }
      })
    }
    else {
      this.security = true;
      this.loadFormToSend()
    }
  }

  /**
   * function to confirm the password and validate changes of data
   * @returns true if password was confirmed.
   */
  public modalConfirmPassword(): boolean {
    let isValid = false;

    const dialogRef = this._dialog.open(ModalConfirmPasswordComponent, {
      panelClass: 'modal-confirm',
      data: {
        type: User.Family,
        familyId: this._dataFromModal.familyId,
      } as ModalConfirmPasswordComponentData
    })

    dialogRef.afterClosed().subscribe(e => {
      if (e) {
        if (e.confirm) {
          this.security = true;
          this.loadFormToSend();
        }
      }
    })

    return isValid;
  }
}
