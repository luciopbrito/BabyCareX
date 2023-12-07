import { Observable } from "rxjs";

export interface BaseLoginService<T> {
  login(email: string, password: string): Observable<T>;
}
