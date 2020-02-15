import { Injectable } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationData } from '../models/authentication-data.model';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate  {

  private authData: AuthenticationData = null;

  constructor(private authService: AuthenticationService,
              private router: Router) {
    this.authService.authenticationData$.subscribe(s => this.authData = s);
              }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    if (this.authData) {
        return true;
    }

    this.router.navigate(['/login']);
    return false;
  }
}
