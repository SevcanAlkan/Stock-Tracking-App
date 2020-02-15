import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { SharedRoutingModule } from './shared-routing.module';

import { ToastrModule } from 'ngx-toastr';
import { NavbarComponent } from './components/navbar/navbar.component';
import { GridListComponent } from './components/grid-list/grid-list.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';

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
