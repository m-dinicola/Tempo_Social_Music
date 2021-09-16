import { TestBed } from '@angular/core/testing';

import { TempoUserService } from './tempo-user.service';

describe('TempoUserService', () => {
  let service: TempoUserService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TempoUserService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
