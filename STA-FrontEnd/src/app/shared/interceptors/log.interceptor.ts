import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpEventType } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Injectable } from '@angular/core';

@Injectable()
export class LoggingInterceptor implements HttpInterceptor {
    constructor() { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        console.log(`Logging Interceptor - ${req.url}`);

        return next.handle(req).pipe(
            tap(event => {
                if (event.type === HttpEventType.Response) {
                    // console.log(event.body);
                }
            })
        );
    }
}