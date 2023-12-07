import { Family, FamilyBase } from 'src/app/interfaces/family.interface';
import { BaseService } from './base-service';
import { inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { NotificationService } from 'src/app/services/notification/notification.service';

describe('BaseService', () => {
  it('should create an instance', () => {
    const http = inject(HttpClient);
    const nf = inject(NotificationService);
    expect(new BaseService<FamilyBase,Family>(http, nf)).toBeTruthy();
  });
});
