import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { LoginVM } from '../models/login.model';
import { AuthenticationData } from '../models/authentication-data.model';
import { map, catchError } from 'rxjs/operators';
import { User } from 'src/app/user/models/user';
import { UserService } from 'src/app/user/services/user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  public currentUser$: Observable<User>;
  public authenticationData$: Observable<AuthenticationData>;

  private currentUserSubject: BehaviorSubject<User> = new BehaviorSubject<User>(null);
  private authenticationDataSubject: BehaviorSubject<any> = new BehaviorSubject<AuthenticationData>(null);

  private authData: AuthenticationData = null;

  constructor(
    private http: HttpClient,
    private userService: UserService
    ) {
    this.currentUser$ = this.currentUserSubject.asObservable();
    this.authenticationData$ = this.authenticationDataSubject.asObservable();
  }

  public login(loginModel: LoginVM): Observable<boolean> {

    const reqHeaders = new HttpHeaders({ 'dont-authenticate': '', 'dont-cache': '' });

    return this.http.post(environment.api.auth.token, loginModel, { headers: reqHeaders }).pipe(map(result => {
      if (result.hasOwnProperty('Token')) {
        this.authData = result as AuthenticationData;
        this.authenticationDataSubject.next(result);
        this.loadUser();
        return true;
      } else {
        return false;
      }
    }), catchError(err => this.handleHttpError(err)));
  }

  public logout(): void {
    this.authData = null;
    this.authenticationDataSubject.next(null);
  }

  private loadUser(): void {
    if (this.authData) {
      this.userService.getById(this.authData.UserId).subscribe(user => {
        if (user) {
          this.currentUserSubject.next(user as User);
        }
      });
    }
  }

  private handleHttpError(error: HttpErrorResponse): Observable<boolean> {
    console.log(error);
    return of(false);
  }
}
