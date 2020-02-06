import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/authentication/services/authentication.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit, OnDestroy {

  showNavbar = false;
  private subscription: Subscription;

  constructor(private _authService: AuthenticationService,
    private _router: Router) { 
  }

  ngOnInit() {
    this.subscription = this._authService.authenticationData$.subscribe(data => {
      if (!data) {
        this.showNavbar = false;
      } else {
        this.showNavbar = true; 
      }
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  logout() {
    this._authService.logout();
    this._router.navigate(['/login']);
  }
}
