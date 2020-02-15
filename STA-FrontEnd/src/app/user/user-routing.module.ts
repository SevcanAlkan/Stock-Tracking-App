import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProfileComponent } from './components/profile/profile.component';
import { UserListComponent } from './components/user-list/user-list.component';

const routes: Routes = [
  { path: 'profile', component: ProfileComponent },
  { path: '', component: UserListComponent, pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
