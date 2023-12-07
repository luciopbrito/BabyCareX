import { Injectable } from '@angular/core';
import { BaseService } from 'src/app/shared/base/base-service/base-service';
import { HttpClient } from '@angular/common/http';
import { Family, FamilyBase } from 'src/app/interfaces/family.interface';
import { NotificationService } from '../notification/notification.service';
import { BaseLoginService } from 'src/app/interfaces/base-login-service';
import { Observable } from 'rxjs';
import { Child, ChildBase } from 'src/app/interfaces/child.interface';

@Injectable({
  providedIn: 'root'
})
export class FamilyService extends BaseService<FamilyBase,Family> implements BaseLoginService<Family>  {
  override endpoint = '/Families'
  public childrenEndpoint = '/Children'

  constructor(private httpClient: HttpClient, private notificationServices: NotificationService) {
    super(httpClient, notificationServices)
  }

  public login(email: string, password: string): Observable<Family> {
    return this.httpClient.post<Family>(`${this.host}${this.endpoint}/login`, { email, password });
  }

  public createChild(familyId: number, child: ChildBase): Observable<Child> {
    return this.httpClient.post<Child>(`${this.host}${this.endpoint}/${familyId}${this.childrenEndpoint}`, child)
  }

  public updateChild(familyId: number, child: Child): Observable<Child> {
    return this.httpClient.put<Child>(`${this.host}${this.endpoint}/${familyId}${this.childrenEndpoint}/${child.id}`, child)
  }

  public deleteChildById(familyId: number, childId: number): Observable<void> {
    return this.httpClient.delete<void>(`${this.host}${this.endpoint}/${familyId}${this.childrenEndpoint}/${childId}`)
  }

  public getChildById(familyId: number, childId: number): Observable<Child> {
    return this.httpClient.get<Child>(`${this.host}${this.endpoint}/${familyId}${this.childrenEndpoint}/${childId}`)
  }
}
