import { TestBed } from '@angular/core/testing';

import { BopsService } from './bops.service';

describe('BopsService', () => {
  let service: BopsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(BopsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
