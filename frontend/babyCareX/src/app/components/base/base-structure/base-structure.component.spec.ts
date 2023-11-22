import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BaseStructureComponent } from './base-structure.component';

describe('BaseStructureComponent', () => {
  let component: BaseStructureComponent;
  let fixture: ComponentFixture<BaseStructureComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BaseStructureComponent]
    });
    fixture = TestBed.createComponent(BaseStructureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
