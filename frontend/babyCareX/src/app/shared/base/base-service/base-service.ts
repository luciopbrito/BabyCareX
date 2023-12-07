import { NotificationService } from './../../../services/notification/notification.service';
import { HttpClient, HttpErrorResponse } from "@angular/common/http"
import { Observable } from "rxjs";
import { environment } from "src/environment/environment";

export abstract class BaseService<B, F> {
  protected host = environment.api;
  protected endpoint = ''

  getFullEndpoint(): string {
    return this.host + this.endpoint
  }

  public getAll(): Observable<F[]> {
    return this.http.get<F[]>(`${this.host}${this.endpoint}`);
  }

  public getById(id: number): Observable<F> {
    return this.http.get<F>(`${this.host}${this.endpoint}/${id}`);
  }

  public update(id: number, data: F): Observable<F> {
    return this.http.put<F>(`${this.host}${this.endpoint}/${id}`, data);
  }

  public delete(id: number): Observable<{message: string}> {
    return this.http.delete<{message: string}>(`${this.host}${this.endpoint}/${id}`);
  }

  public create(data: B): Observable<F> {
    return this.http.post<F>(`${this.host}${this.endpoint}`, data);
  }

  public tooltipErrorMessage(respError: HttpErrorResponse) {
    debugger
    this.notificationService.responseError(respError);
  }

  constructor(private http: HttpClient, private notificationService: NotificationService) { }
}
