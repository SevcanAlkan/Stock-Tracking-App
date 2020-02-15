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
  public userName = '';

  constructor(private authService: AuthenticationService,
              private router: Router) {
  }

  ngOnInit() {
    this.subscription = this.authService.authenticationData$.subscribe(data => {
      if (!data) {
        this.showNavbar = false;
        this.userName = '';
      } else {
        this.showNavbar = true;
        this.userName = data.DisplayName;
      }
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
