import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './components/login/login.component';
import { ProfileComponent } from './components/profile/profile.component';



@NgModule({
  declarations: [LoginComponent, ProfileComponent],
  imports: [
    CommonModule
  ]
})
export class AutenticationModule { }
