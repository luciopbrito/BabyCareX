import { NotificationService } from 'src/app/services/notification/notification.service';
import { FamilyService } from 'src/app/services/family/family.service';
import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Child, ChildBase } from 'src/app/interfaces/child.interface';
import { Gender } from 'src/app/enums/gender';
import { Router } from '@angular/router';
import { LocalStorageService } from 'src/app/services/localStorage/local-storage.service';
import { Family } from 'src/app/interfaces/family.interface';

export type FormChildComponentData = {
  familyId: number,
  childId?: number
  edition: boolean;
}

@Component({
  selector: 'app-form-child',
  templateUrl: './form-child.component.html',
  styleUrls: ['./form-child.component.scss']
})
export class FormChildComponent implements OnInit {
  @Input() public childId: number;
  @Input() public familyId: number;

  public child: Child
  public isModal = false;
  public edition = false;
  public genderSelectOptions: {option: string, value: number}[] = [
    {
      option: "Masculino",
      value: Gender.Masculine,
    },
    {
      option: "Feminino",
      value: Gender.Feminine,
    },
    {
      option: "Preservar",
      value: Gender.Preserve,
    },
  ]

  public form = new FormGroup({
    name: new FormControl<string | null>(null, [Validators.required]),
    age: new FormControl<number | null>(null, [Validators.required]),
    sex: new FormControl<number | null>(null, [Validators.required]),
    isSpecialChild: new FormControl<boolean | null>(null),
    // personalData: new FormControl<boolean | null>(null),
  });

  constructor(
    private familyService: FamilyService,
    private dialogRef: MatDialogRef<FormChildComponent>,
    private notificationService: NotificationService,
    private localStorageService: LocalStorageService,
    private router: Router,
    @Inject(MAT_DIALOG_DATA) public dataFromModal?: FormChildComponentData,
  ) {
    if (dataFromModal?.familyId != null) {
      this.isModal = true;
      this.edition = dataFromModal.edition;
    }


  }

  ngOnInit(): void {
    if (this.edition && this.isModal) {
      this.familyService.getChildById(this.dataFromModal!.familyId, this.dataFromModal!.childId!).subscribe({
        next: (e) => {
          this.form.patchValue(e);
          this.child = e;
        },
        error: (respError) => {
          this.familyService.tooltipErrorMessage(respError)
        }
      });
    }

    if (this.edition && !this.isModal) {
      this.familyService.getChildById(this.familyId, this.childId).subscribe({
        next: (e) => {
          this.form.patchValue(e);
          this.child = e;
        },
        error: (respError) => {
          this.familyService.tooltipErrorMessage(respError)
        }
      });
    }
  }

  public saveForm() {
    let formValues = this.form.getRawValue();

    if (this.form.valid && this.isModal && this.edition) {
      const body = {
        ...this.child,
        ...formValues
      } as Child;

      console.log('before obj: ', this.child)

      console.log('CASE 1: body that was send: ', body)

      this.familyService.updateChild(this.dataFromModal!.familyId, body).subscribe({
        next: (resp) => {
          this.messageSuccessUpdate();
          this.dialogRef.close({changed: true})
        },
        error: (respError) => {
          this.familyService.tooltipErrorMessage(respError);
        }
      })
    }
    else if (this.form.valid && this.isModal) {
      const body = {
        ...formValues,
        isSpecialChild: formValues.isSpecialChild == null ? false : formValues.isSpecialChild,
        familyId: this.dataFromModal!.familyId
      } as ChildBase;

      console.log('CASE 2: body that was send: ', body)

      this.familyService.createChild(body.familyId, body).subscribe({
        next: (resp) => {
          this.messageSuccessCreation();
          this.dialogRef.close({changed: true});
        },
        error: (respError) => {
          this.familyService.tooltipErrorMessage(respError);
        }
      })
    }

    if (this.form.valid && !this.isModal && this.edition) {
      const body = {
        ...this.child,
        ...formValues
      } as Child;

      console.log('CASE 3: body that was send: ', body)

      const familyId = this.familyId == undefined ? (this.localStorageService.getItem('login') as Family).id : this.familyId

      this.familyService.updateChild(this.familyId, body).subscribe({
        next: (resp) => {
          this.messageSuccessUpdate();
          this.redirectToChildren();
        },
        error: (respError) => {
          this.familyService.tooltipErrorMessage(respError);
        }
      })
    }
    else if (this.form.valid && !this.isModal) {
      const body = {
        ...formValues,
        isSpecialChild: formValues.isSpecialChild == null ? false : formValues.isSpecialChild,
        familyId: this.familyId == undefined ? (this.localStorageService.getItem('login') as Family).id : this.familyId
      } as ChildBase;

      console.log('CASE 4: body that was send: ', body)

      const familyId = this.familyId == undefined ? (this.localStorageService.getItem('login') as Family).id : this.familyId

      this.familyService.createChild(body.familyId, body).subscribe({
        next: (resp) => {
          this.messageSuccessCreation();
          this.redirectToChildren();
        },
        error: (respError) => {
          this.familyService.tooltipErrorMessage(respError);
        }
      })
    }


  }

  public cancelModal() {
    this.dialogRef.close()
  }

  private messageSuccessUpdate() {
    this.notificationService.success("criança atualizada com sucesso");
  }

  private messageSuccessCreation() {
    this.notificationService.success('criança cadastrada com sucesso');
  }

  private redirectToChildren() {
    this.router.navigateByUrl('/family/children');
  }
}
