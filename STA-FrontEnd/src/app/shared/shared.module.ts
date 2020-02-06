import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { SharedRoutingModule } from './shared-routing.module';

import { ToastrModule } from 'ngx-toastr';

import { NavbarComponent, GridListComponent, PageNotFoundComponent } from './index';

@NgModule({
  declarations: [NavbarComponent, GridListComponent, PageNotFoundComponent],
  imports: [
    CommonModule,
    SharedRoutingModule,
    BrowserAnimationsModule, 
    ToastrModule.forRoot()
  ],
  exports: [
    NavbarComponent,
    GridListComponent
  ]
})
export class SharedModule { }
