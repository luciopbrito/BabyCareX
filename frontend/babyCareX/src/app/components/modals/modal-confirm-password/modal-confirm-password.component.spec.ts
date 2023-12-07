import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalConfirmPasswordComponent } from './modal-confirm-password.component';

describe('ModalConfirmPasswordComponent', () => {
  let component: ModalConfirmPasswordComponent;
  let fixture: ComponentFixture<ModalConfirmPasswordComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ModalConfirmPasswordComponent]
    });
    fixture = TestBed.createComponent(ModalConfirmPasswordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
