import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthenticationService } from 'src/app/authentication';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit, OnDestroy {

  showNavbar = false;
  private subscription: Subscription;

  constructor(private _authService: AuthenticationService) { 
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
}
