import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

// Modules
import { SharedModule } from './shared/shared.module';
import { AuthenticationModule } from './authentication/authentication.module';

import { AuthInterceptor } from './authentication/interceptors/auth.interceptor';
import { CacheInterceptor } from './shared/interceptors/cache.interceptor';
import { LoggingInterceptor } from './shared/interceptors/log.interceptor';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    AuthenticationModule,
    SharedModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: CacheInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoggingInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
