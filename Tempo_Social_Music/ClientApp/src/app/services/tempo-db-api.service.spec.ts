import { TestBed } from '@angular/core/testing';

import { TempoDBAPIService } from './tempo-db-api.service';

describe('TempoDBAPIService', () => {
  let service: TempoDBAPIService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TempoDBAPIService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
