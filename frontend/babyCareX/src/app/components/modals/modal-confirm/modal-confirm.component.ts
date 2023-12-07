import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog'

export type ModalConfirmComponentData = {
  title: string,
  message: string,
  cancelMessage?: string,
  confirmMessage?: string,
}

@Component({
  selector: 'app-modal-confirm',
  templateUrl: './modal-confirm.component.html',
  styleUrls: ['./modal-confirm.component.scss']
})
export class ModalConfirmComponent {
  public title: string;
  public message: string;
  public cancelMessage = 'Cancelar';
  public confirmMessage = 'Confirmar';

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: ModalConfirmComponentData,
    private matDialogRef: MatDialogRef<ModalConfirmComponent>,
  ) {
    this.title = data.title
    this.message = data.message
    if (data.cancelMessage) this.cancelMessage = data.cancelMessage;
    if (data.confirmMessage) this.confirmMessage = data.confirmMessage;
  }

  public confirm() {
    this.matDialogRef.close({confirm: true});
  }

  public cancel() {
    this.matDialogRef.close({confirm: false});
  }
}
