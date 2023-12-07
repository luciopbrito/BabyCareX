import { HttpErrorResponse, HttpStatusCode } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private snackBar: MatSnackBar) { }

  public success(message: string = "ação realizada com sucesso") {
    this.snackBar.open(message, 'x', {
      duration: 5000,
      horizontalPosition: "right",
      verticalPosition: "top",
      panelClass: 'success'
    })
  }

  public waring(message: string) {
    this.snackBar.open(message, 'x', {
      duration: 5000,
      horizontalPosition: "right",
      verticalPosition: "top",
      panelClass: 'waring'
    })
  }

  public error(message: string) {
    this.snackBar.open(message, 'x', {
      duration: 5000,
      horizontalPosition: "right",
      verticalPosition: "top",
      panelClass: 'error'
    })
  }

  public responseError(respError: HttpErrorResponse) {
    let message = '';
    if (respError.status === HttpStatusCode.BadRequest) {
      respError.error.errors ? message = Object.values(respError.error.errors as Array<Array<any>>).map(e => e.toString()).join('\n') : message = respError.error.userMessage;
    }

    if (respError.status === HttpStatusCode.NotFound || respError.status === HttpStatusCode.InternalServerError) {
      message = respError.error.userMessage
    }

    if (respError.status === HttpStatusCode.NotFound && Number(respError.error.errorCode) === HttpStatusCode.NotFound) {
      message = respError.error.developerMessage
    }


    this.snackBar.open(`ERROR: \n ${message}`, 'x', {
      duration: 10000,
      horizontalPosition: "right",
      verticalPosition: "top",
      panelClass: 'response-error'
    })
  }
}
