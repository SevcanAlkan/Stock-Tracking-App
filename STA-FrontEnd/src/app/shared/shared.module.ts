import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { SharedRoutingModule } from './shared-routing.module';

import { ToastrModule } from 'ngx-toastr';

import { NavbarComponent, GridListComponent, PageNotFoundComponent } from './index';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { LoggingInterceptor } from './interceptors/log.interceptor';

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
  ],
  providers: [
     { provide: HTTP_INTERCEPTORS, useClass: LoggingInterceptor, multi: true }
   ]
})
export class SharedModule { }
