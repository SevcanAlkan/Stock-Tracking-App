import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  public currentUser$: Observable<any>;  
  public authenticationData$: Observable<any>;

  private currentUserSubject: BehaviorSubject<any> = new BehaviorSubject<any>({ username: "", password: "" });  
  private authenticationDataSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  private authenticationData?: any = '';
  private token: string = '';
  
  constructor() { 
    this.currentUser$ = this.currentUserSubject.asObservable();    
    this.authenticationData$ = this.authenticationDataSubject.asObservable();
  }

  public isAuthenticated(): boolean {
    return !!this.authenticationData;
  }

  public getToken(): string {
    return this.token;
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
