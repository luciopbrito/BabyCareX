import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormChildComponent } from './form-child.component';

describe('FormChildComponent', () => {
  let component: FormChildComponent;
  let fixture: ComponentFixture<FormChildComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [FormChildComponent]
    });
    fixture = TestBed.createComponent(FormChildComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
