import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthenticationService } from '../..';
import { MessageDialogService } from 'src/app/shared';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {

  loginForm: FormGroup;
  isSending = false;

  showNavbar = false;
  private subscription: Subscription;

  constructor(private _authService: AuthenticationService,
    private _fb: FormBuilder,
    private _router: Router,
    private _msg: MessageDialogService) { }

  ngOnInit() {
    this.subscription = this._authService.authenticationData$.subscribe(data => {
      if (data) {
        this.showNavbar = false;
      } else {
        this.showNavbar = true; 
      }
    });

    this.loginForm = this._fb.group({
      username: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(25)]],
      password: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(50)]]
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  onSubmit(): void {
    this.loginForm.markAllAsTouched();
    this.isSending = true;

    if (this.loginForm.valid) {
      const data = this.loginForm.value;

      console.log(data);
      if (this._authService.login(data)) {
        this._router.navigate(['/dashboard']);
      } else {
        this._msg.showError('Error!', 'Username or password is not correct!');
      }
    } else {  
      this._msg.showWarning('Warning!', "The login form isn't valid!");
    }
    
    this.isSending = false;
  }

}
