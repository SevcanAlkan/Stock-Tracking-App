import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  public currentUser$: Observable<any>;
  public token$: Observable<string>;
  public authenticationData$: Observable<any>;

  private currentUserSubject: BehaviorSubject<any> = new BehaviorSubject<any>({ username: "", password: "" });
  private tokenSubject: BehaviorSubject<string> = new BehaviorSubject<string>("");
  private authenticationDataSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  private authenticationData?: any = "";
  
  constructor() { 
    this.currentUser$ = this.currentUserSubject.asObservable();
    this.token$ = this.tokenSubject.asObservable();
    this.authenticationData$ = this.authenticationDataSubject.asObservable();
  }

  public isAuthenticated(): boolean {
    return !!this.authenticationData;
  }

  public login(loginModel: any): boolean {
    if (loginModel.username == 'admin' && loginModel.password == 'admin') {
      this.authenticationDataSubject.next(loginModel);
      this.authenticationData = loginModel;
      return true;
    } else {
      return false;
    }
  }

  public logout(): void {
    this.authenticationDataSubject.next(null);
    this.authenticationData = null;
  }

}
