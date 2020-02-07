import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { AuthenticationService } from '../services/authentication.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(private _authService: AuthenticationService) {     
    }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        console.log(`Authentication Interceptor - ${this._authService.getToken()}`);

        const headers = new HttpHeaders({
            'Authorization': this._authService.getToken(),
            'Content-Type': 'application/json'
        });

        const modifiedRequest = req.clone({
            headers           
        });

        return next.handle(modifiedRequest)
            .pipe(
                tap(event => {
                    if (event instanceof HttpResponse) {
                        //modify response here
                    }
            })
        );
    }
}