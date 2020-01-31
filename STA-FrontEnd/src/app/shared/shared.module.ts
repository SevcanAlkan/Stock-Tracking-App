import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './components/navbar/navbar.component';
import { GridListComponent } from './components/grid-list/grid-list.component';



@NgModule({
  declarations: [NavbarComponent, GridListComponent],
  imports: [
    CommonModule
  ]
})
export class SharedModule { }
