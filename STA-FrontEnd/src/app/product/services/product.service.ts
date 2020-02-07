import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators'
import { MessageDialogService } from 'src/app/shared';
import { environment } from 'src/environments/environment';
import { Product } from '..';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient,
    private msg: MessageDialogService) {

  }

  getAll(): Observable<Product[] | HttpErrorResponse> {
    return this.http.get<Product[]>('https://reqres.in/api/users?page=2').pipe(
      catchError(error => this.handleHttpError(error))
    );
  }

  getById(id: string): Observable<Product | HttpErrorResponse> {
    return this.http.get<Product>(environment.api.products.baseUrl + `/${id}`).pipe(
      catchError(error => this.handleHttpError(error))
    );
  }

  post(product: Product): Observable<Product | HttpErrorResponse> {
    return this.http.post<Product>(environment.api.products.baseUrl, product).pipe(
      catchError(error => this.handleHttpError(error))
    );
  }

  put(product: Product): Observable<Product | HttpErrorResponse> {
    return this.http.put<Product>(environment.api.products.baseUrl + `/${product.id}`, product).pipe(
      catchError(error => this.handleHttpError(error))
    );
  }

  delete(id: string): Observable<Product | HttpErrorResponse> {
    return this.http.delete<Product>(environment.api.products.baseUrl + `/${id}`).pipe(
      catchError(error => this.handleHttpError(error))
    );
  }
  
  private handleHttpError(error: HttpErrorResponse): Observable<HttpErrorResponse>{
    this.msg.showError(`Error!(${error.statusText})`, error.message);

    return throwError(error);
  }
}
