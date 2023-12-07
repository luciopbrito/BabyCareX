import { NotificationService } from 'src/app/services/notification/notification.service';
import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { User } from 'src/app/enums/user';
import { FamilyService } from 'src/app/services/family/family.service';

export type ModalConfirmPasswordComponentData = {
  familyId?: number,
  babaId?: number,
  type: string
}

@Component({
  selector: 'app-modal-confirm-password',
  templateUrl: './modal-confirm-password.component.html',
  styleUrls: ['./modal-confirm-password.component.scss']
})
export class ModalConfirmPasswordComponent {
  public type: string;
  public form = new FormGroup({
    password: new FormControl<string | null>(null, [Validators.required])
  })

  constructor(
    private _familyService: FamilyService,
    private _notificationService: NotificationService,
    private _dialogRef: MatDialogRef<ModalConfirmPasswordComponent>,
    @Inject(MAT_DIALOG_DATA) private _dataFromModal: ModalConfirmPasswordComponentData
  ) {
    this.type = this._dataFromModal.type
  }

  public save() {
    if (this.type == User.Family && this._dataFromModal.familyId) {
      this._familyService.getById(this._dataFromModal.familyId!).subscribe(e => {
        this.form.value.password == e.password ? this._dialogRef.close({confirm: true}) : this.resolveError()
      })
    }
  }

  public resolveError() {
    this._notificationService.error('senha incorreta');
    this.form.value.password = null;
  }
}
