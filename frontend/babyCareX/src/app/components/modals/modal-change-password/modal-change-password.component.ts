import { NotificationService } from 'src/app/services/notification/notification.service';
import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { User } from 'src/app/enums/user';
import { FamilyService } from 'src/app/services/family/family.service';
import { Family } from 'src/app/interfaces/family.interface';
import { Baba } from 'src/app/interfaces/baba.interface';

export type ModalChangePasswordComponentData = {
  type: string,
  familyId?: number,
  babaId?: number,
}

@Component({
  selector: 'app-modal-change-password',
  templateUrl: './modal-change-password.component.html',
  styleUrls: ['./modal-change-password.component.scss']
})
export class ModalChangePasswordComponent implements OnInit{
  public hide = true;
  public oldState: Family | Baba;

  public form = new FormGroup({
    password: new FormControl<string | null>(null, [Validators.required]),
    passwordConfirm: new FormControl<string | null>(null, [Validators.required]),
  })

  constructor(
    private _familyService: FamilyService,
    private _notificationService: NotificationService,
    private _dialogRef: MatDialogRef<ModalChangePasswordComponent>,
    @Inject(MAT_DIALOG_DATA) private _dataFromModal: ModalChangePasswordComponentData
  ) {}

  ngOnInit(): void {
    if (this._dataFromModal.familyId) this._familyService.getById(this._dataFromModal.familyId!).subscribe(e => { this.oldState = e});
    // if (this._dataFromModal.babaId) this._babaService.getById(this._dataFromModal.babaId!).subscribe(e => { this.oldPassword = e.password});
  }

  public save() {
    if (this.form.valid && this._dataFromModal.type == User.Family && this.confirmPassword()) {
      const body = {
        ...this.oldState,
        password: this.form.value.password,
      } as Family

      this._familyService.update(body.id, body).subscribe({
        next: (resp) => {
          this.messageSuccess();
          this._dialogRef.close({changed: true});
        },
        error: (respError) => {
          this._familyService.tooltipErrorMessage(respError);
        }
      });
    }

    if (this.form.valid && this._dataFromModal.type == User.Baba && this.confirmPassword()) {
      const body = {
        ...this.oldState,
        password: this.form.value.password,
      } as Family
    }
  }

  public confirmPassword(): boolean {
    if (this.form.value.password != this.form.value.passwordConfirm) {
      this._notificationService.error('senhas não são correspondentes');
      return false;
    }

    if (this.form.value.password == this.oldState.password) {
      this._notificationService.error('a senha precisa ser diferente atual');
      return false;
    }

    return true;
  }

  public cancel() {
    this._dialogRef.close()
  }

  private messageSuccess() {
    this._notificationService.success('senha alterada com sucesso!')
  }
}
