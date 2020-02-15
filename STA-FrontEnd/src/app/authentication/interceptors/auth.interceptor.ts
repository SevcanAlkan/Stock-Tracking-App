import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';
import { AuthenticationData } from '../models/authentication-data.model';
import { Router } from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    private authData: AuthenticationData = null;

    constructor(private authService: AuthenticationService,
                private router: Router) {
        this.authService.authenticationData$.subscribe(s => this.authData = s);
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        let headers = new HttpHeaders({
            'Content-Type': 'application/json',
            'Access-Control-Allow-Methods': 'GET, POST, OPTIONS, PUT, PATCH, DELETE',
            'Access-Control-Allow-Headers': 'X-Requested-With,content-type',
            'Access-Control-Allow-Credentials': 'true'
        });

        if (!req.headers.has('dont-authenticate') && this.authData) {
            headers = headers.append('Authorization', 'Bearer ' + this.authData.Token);
        }

        const modifiedRequest = req.clone({
            headers
        });

        return next.handle(modifiedRequest)
            .pipe(
                tap(event => {
                    if (event instanceof HttpResponse) {
                        if (event.status === 401) {
                            this.authService.logout();
                            this.router.navigate(['/login']);
                        }
                    }
                })
            );
    }
}
