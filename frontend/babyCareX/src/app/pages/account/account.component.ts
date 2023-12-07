import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { ModalChangePasswordComponent, ModalChangePasswordComponentData } from 'src/app/components/modals/modal-change-password/modal-change-password.component';
import { ModalConfirmPasswordComponent, ModalConfirmPasswordComponentData } from 'src/app/components/modals/modal-confirm-password/modal-confirm-password.component';
import { ModalConfirmComponent, ModalConfirmComponentData } from 'src/app/components/modals/modal-confirm/modal-confirm.component';
import { FormAccountComponent, FormAccountComponentData } from 'src/app/componets/forms/form-account/form-account.component';
import { User } from 'src/app/enums/user';
import { Baba } from 'src/app/interfaces/baba.interface';
import { Family } from 'src/app/interfaces/family.interface';
import { FamilyService } from 'src/app/services/family/family.service';
import { LocalStorageService } from 'src/app/services/localStorage/local-storage.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.scss']
})
export class AccountComponent implements OnInit {
  public isFamily = false;
  public isBaba = false;
  private type: string;
  public account$: Observable<Family>;

  constructor(
    private _activatedRoute: ActivatedRoute,
    private _router: Router,
    private _familyService: FamilyService,
    // private _babaServices: babaServices,
    private _localStorageService: LocalStorageService,
    private _dialog: MatDialog,
  ) {
    const type = this._activatedRoute.snapshot.data['type'];
    const access = type == User.Family || type == User.Baba;
    access ? this.type = type : this._router.navigate(['/not-authorized']);
  }

  ngOnInit(): void {
    this.loadAccountInfo();
  }

  private loadAccountInfo() {
    if (this.type == User.Family) {
      this.isFamily = true;
      this.account$ = this._familyService.getById((this._localStorageService.getItem('login') as Family).id)
    };

    if (this.type == User.Baba) {
      this.isBaba = true;
    };
  }

  public openModalEdition(account: Family | Baba) {
    const dialogRef = this._dialog.open(FormAccountComponent, {
      panelClass: 'form-child',
      data: {
        edition: true,
        familyId: this.type == User.Family ? account.id : null,
        babaId: this.type == User.Baba ? account.id : null,
      } as FormAccountComponentData
    });

    dialogRef.afterClosed().subscribe(e => {
      if (e) {
        if (e.changed) this.loadAccountInfo();
      }
    })
  }

  private openModalChangePassword(account: Family | Baba) {
    const dialogRef = this._dialog.open(ModalChangePasswordComponent, {
      panelClass: 'form-child',
      data: {
        type: this.type,
        familyId: this.type == User.Family ? account.id : null,
        babaId: this.type == User.Baba ? account.id : null,
      } as ModalChangePasswordComponentData
    });

    dialogRef.afterClosed().subscribe(e => {
      if (e) {
        if (e.changed) this.loadAccountInfo();
      }
    });
  }

  /**
   * function to user's authentication that allows to changes of data.
   *
   * Open the modal and verify if the user wishes to change info about itself.
   * @param account is current user of the app.
   */
  public authenticationToChangePassword(account: Family | Baba) {
    const dialogRef = this._dialog.open(ModalConfirmComponent, {
      panelClass: 'modal-confirm',
      data: {
        title: 'Mudança de informação de Acesso!',
        message: 'É necessário que utilize a senha novamente para que esta ação seja concluída',
      } as ModalConfirmComponentData
    });

    dialogRef.afterClosed().subscribe(e => {
      if (e) {
        if (e.confirm) this.openModalConfirmPassword(account);
      }
    })
  }

  public openModalConfirmPassword(account: Family | Baba) {
    const dialogRef = this._dialog.open(ModalConfirmPasswordComponent, {
      panelClass: 'modal-confirm',
      data: {
        type: this.type,
        babaId: this.type == User.Baba ? account.id : null,
        familyId: this.type == User.Family ? account.id : null,
      } as ModalConfirmPasswordComponentData
    });

    dialogRef.afterClosed().subscribe(e => {
      if (e) {
        if (e.confirm) this.openModalChangePassword(account);
      }
    })
  }

}
