import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthenticationService } from '../../services/authentication.service';
import { LoginVM } from '../../models/login.model';
import { MessageDialogService } from 'src/app/shared/services/message-dialog.service';

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

  constructor(
    private authService: AuthenticationService,
    private fb: FormBuilder,
    private router: Router,
    private msg: MessageDialogService) { }

  ngOnInit() {
    this.subscription = this.authService.authenticationData$.subscribe(data => {
      if (data) {
        this.showNavbar = false;
      } else {
        this.showNavbar = true;
      }
    });

    this.loginForm = this.fb.group({
      UserName: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(25)]],
      PasswordHash: ['', [Validators.required, Validators.minLength(5), Validators.maxLength(50)]]
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  onSubmit(): void {
    this.loginForm.markAllAsTouched();
    this.isSending = true;

    // Trim fields

    if (this.loginForm.valid) {
      const data = (this.loginForm.value as LoginVM);

      data.UserName = data.UserName.replace(/\s/g, '');
      data.PasswordHash = data.PasswordHash.replace(/\s/g, '');

      this.authService.login(data).subscribe(result => {
        if (result) {
          this.router.navigate(['/dashboard']);
        } else {
          this.msg.showError('Error!', 'Username or password is not correct!');
        }
      });
    } else {
      this.msg.showWarning('Warning!', 'The login form isn\'t valid!');
    }

    this.isSending = false;
  }

}
