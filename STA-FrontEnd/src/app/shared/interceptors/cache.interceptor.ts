import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse, HttpEventType } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { HttpCacheService } from '../services/http-cache.service';

@Injectable()
export class CacheInterceptor implements HttpInterceptor {
    constructor(private cacheService: HttpCacheService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        if (req.method !== 'GET' || req.headers.has('dont-cache')) {
            console.log(`Cache interceptor passed - ${req.url}`);
            return next.handle(req);
            // Use signalR to detect changes!
        }

        const cachedResponse: HttpResponse<any> = this.cacheService.get(req.url);

        if (cachedResponse) {
            console.log(`Returning data from cached response - ${cachedResponse.url}`);
            return of(cachedResponse);
        } else {
            return next.handle(req).pipe(
                tap(event => {
                    if (event.type === HttpEventType.Response) {
                        console.log(`Adding data to the cache - ${req.url}`);
                        this.cacheService.put(req.url, event);
                    }
                })
            );
        }
    }
}