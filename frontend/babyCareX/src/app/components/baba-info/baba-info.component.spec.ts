import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BabaInfoComponent } from './baba-info.component';

describe('BabaInfoComponent', () => {
  let component: BabaInfoComponent;
  let fixture: ComponentFixture<BabaInfoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BabaInfoComponent]
    });
    fixture = TestBed.createComponent(BabaInfoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
