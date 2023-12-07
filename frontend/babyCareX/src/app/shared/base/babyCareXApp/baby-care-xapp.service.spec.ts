import { TestBed } from '@angular/core/testing';

import { BabyCareXAppService } from './baby-care-xapp.service';

describe('BabyCareXAppService', () => {
  let service: BabyCareXAppService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BabyCareXAppService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
