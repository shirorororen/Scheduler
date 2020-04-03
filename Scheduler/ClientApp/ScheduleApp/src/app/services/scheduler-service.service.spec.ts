import { TestBed } from '@angular/core/testing';

import { SchedulerServiceService } from './scheduler-service.service';

describe('SchedulerServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: SchedulerServiceService = TestBed.get(SchedulerServiceService);
    expect(service).toBeTruthy();
  });
});
