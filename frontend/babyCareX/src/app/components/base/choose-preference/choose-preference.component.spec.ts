import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChoosePreferenceComponent } from './choose-preference.component';

describe('ChoosePreferenceComponent', () => {
  let component: ChoosePreferenceComponent;
  let fixture: ComponentFixture<ChoosePreferenceComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ChoosePreferenceComponent]
    });
    fixture = TestBed.createComponent(ChoosePreferenceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
