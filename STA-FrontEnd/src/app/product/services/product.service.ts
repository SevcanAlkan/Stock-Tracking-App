import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { MessageDialogService } from 'src/app/shared/services/message-dialog.service';
import { Product } from '../models/product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient,
              private msg: MessageDialogService) {

  }

  getAll(): Observable<Product[] | HttpErrorResponse> {
    return this.http.get<Product[]>(environment.api.products.baseUrl).pipe(
      retry(3),
      catchError(error => this.handleHttpError(error))
    );
  }

  getById(id: string): Observable<Product | HttpErrorResponse> {
    return this.http.get<Product>(environment.api.products.baseUrl + `/${id}`).pipe(
      retry(3),
      catchError(error => this.handleHttpError(error))
    );
  }

  post(product: Product): Observable<Product | HttpErrorResponse> {
    return this.http.post<Product>(environment.api.products.baseUrl, product).pipe(
      retry(3),
      catchError(error => this.handleHttpError(error))
    );
  }

  put(product: Product): Observable<Product | HttpErrorResponse> {
    return this.http.put<Product>(environment.api.products.baseUrl + `/${product.id}`, product).pipe(
      retry(3),
      catchError(error => this.handleHttpError(error))
    );
  }

  delete(id: string): Observable<Product | HttpErrorResponse> {
    return this.http.delete<Product>(environment.api.products.baseUrl + `/${id}`).pipe(
      retry(3),
      catchError(error => this.handleHttpError(error))
    );
  }

  private handleHttpError(error: HttpErrorResponse): Observable<HttpErrorResponse> {
    this.msg.showError(`Error!(${error.statusText})`, error.message);

    return throwError(error);
  }
}
