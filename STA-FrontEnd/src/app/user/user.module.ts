import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';

import { ProfileComponent } from './components/profile/profile.component';
import { UserListComponent } from './components/user-list/user-list.component';


@NgModule({
  declarations: [ProfileComponent, UserListComponent],
  imports: [
    CommonModule,
    UserRoutingModule
  ]
})
export class UserModule { }
