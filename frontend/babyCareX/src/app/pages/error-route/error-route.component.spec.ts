import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ErrorRouteComponent } from './error-route.component';

describe('ErrorRouteComponent', () => {
  let component: ErrorRouteComponent;
  let fixture: ComponentFixture<ErrorRouteComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ErrorRouteComponent]
    });
    fixture = TestBed.createComponent(ErrorRouteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
