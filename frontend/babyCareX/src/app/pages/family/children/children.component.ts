import { NotificationService } from 'src/app/services/notification/notification.service';
import { FamilyService } from 'src/app/services/family/family.service';
import { LocalStorageService } from 'src/app/services/localStorage/local-storage.service';
import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Child } from 'src/app/interfaces/child.interface';
import { Family } from 'src/app/interfaces/family.interface';
import { MatDialog } from '@angular/material/dialog';
import { ModalConfirmComponentData, ModalConfirmComponent } from 'src/app/components/modals/modal-confirm/modal-confirm.component';
import { FormChildComponent, FormChildComponentData } from 'src/app/components/forms/form-child/form-child.component';

@Component({
  selector: 'app-children',
  templateUrl: './children.component.html',
  styleUrls: ['./children.component.scss']
})
export class ChildrenComponent implements OnInit {
  public children$: Observable<Child[]>
  public hasChildren: boolean;

  constructor(
    private localStorageService: LocalStorageService,
    private familyService: FamilyService,
    private notificationService: NotificationService,
    private dialog: MatDialog,
  ) {}

  ngOnInit(): void {
    this.loadingChildren((this.localStorageService.getItem('login') as Family).id);
  }

  /**
   * function to load info about the children.
   * @param familyId as `Number` identifier of family
   */
  public loadingChildren(familyId: number) {
    this.familyService.getById(familyId).subscribe(e => {
      this.children$ = of(e.children)
      this.hasChildren = e.children.length > 0;
    });
  }

  /**
   * function to show correctly child's gender
   * @param sexNumber {@link Child} from child to identifier sex
   */
  public showSex(sexNumber: number): string {
    let gender = '';
    if (sexNumber == 1) gender = 'Masculino';
    if (sexNumber == 2) gender = 'Feminino';
    if (sexNumber == 3) gender = 'Não definido'

    return gender;
  }

  /**
   * function to handle children edition. It open a modal and show info about child
   * @param familyId as `number`
   * @param childId as `number`
   */
  public openChildEditModal(familyId: number, childId: number) {
    const dialogRef = this.dialog.open(FormChildComponent, {
      panelClass: 'form-child',
      data: {
        childId: childId,
        familyId: familyId,
        edition: true,
      } as FormChildComponentData
    });

    dialogRef.afterClosed().subscribe(e => {
      if (e) {
        if (e.changed) this.loadingChildren(familyId);
      }
    })
  }

  /**
   * function to handle delete children
   * @param child as {@link Child}
   */
  public openChildrenDeleteModal(child: Child) {
    const dialogRef = this.dialog.open(ModalConfirmComponent, {
      panelClass: 'modal-confirm',
      data: {
        title: `Deletar ${child.name}?`,
        message: 'Ao efetuar essa ação não será possível reverter.',
      } as ModalConfirmComponentData
    })

    dialogRef.afterClosed().subscribe(e => {
      if (e.confirm) {
        debugger
        this.familyService.deleteChildById(child.familyId, child.id).subscribe({
          next: (resp) => {
            debugger
            this.loadingChildren(child.familyId);
            this.notificationService.success();
          },
          error: (respError) => {
            this.familyService.tooltipErrorMessage(respError);
          }
        });
      }
    })
  }

  public openModalAddChild() {
    const id = (this.localStorageService.getItem('login') as Family).id
    const dialogRef = this.dialog.open(FormChildComponent, {
      panelClass: 'form-child',
      data: {
        familyId: id,
        edition: false,
      } as FormChildComponentData
    });

    dialogRef.afterClosed().subscribe(e => {
      if (e) {
        if (e.changed) this.loadingChildren(id);
      }
    })
  }
}
