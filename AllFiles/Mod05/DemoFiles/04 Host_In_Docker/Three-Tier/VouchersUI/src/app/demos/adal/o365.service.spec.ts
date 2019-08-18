import { TestBed } from '@angular/core/testing';

import { O365Service } from './o365.service';

describe('O365Service', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: O365Service = TestBed.get(O365Service);
    expect(service).toBeTruthy();
  });
});
