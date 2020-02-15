import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map, catchError, retry } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { MessageDialogService } from 'src/app/shared/services/message-dialog.service';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(
    private http: HttpClient,
    private msg: MessageDialogService
  ) { }

  getAll(): Observable<User[] | HttpErrorResponse> {
    return this.http.get<User[]>(environment.api.users.baseUrl + '/Get').pipe(
      retry(3),
      catchError(error => this.handleHttpError(error))
    );
  }

  getById(id: string): Observable<User | HttpErrorResponse> {
    return this.http.get<User>(environment.api.users.baseUrl + `/GetById/${id}`).pipe(
      retry(3),
      catchError(error => this.handleHttpError(error))
    );
  }

  post(user: User): Observable<User | HttpErrorResponse> {
    return this.http.post<User>(environment.api.users.baseUrl, user).pipe(
      retry(3),
      catchError(error => this.handleHttpError(error))
    );
  }

  put(user: User): Observable<User | HttpErrorResponse> {
    return this.http.put<User>(environment.api.users.baseUrl + `/${user.id}`, user).pipe(
      retry(3),
      catchError(error => this.handleHttpError(error))
    );
  }

  delete(id: string): Observable<User | HttpErrorResponse> {
    return this.http.delete<User>(environment.api.users.baseUrl + `/${id}`).pipe(
      retry(3),
      catchError(error => this.handleHttpError(error))
    );
  }

  private handleHttpError(error: HttpErrorResponse): Observable<HttpErrorResponse> {
    this.msg.showError(`Error!(${error.statusText})`, error.message);

    return throwError(error);
  }
}
