import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../models/product/product';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private baseUrl = environment.baseUrl;

  constructor(private http: HttpClient) {}

  getProducts(page: number): Observable<Product[]> {
    const url = `${this.baseUrl}/api/products`;
    const params = new HttpParams().set('page', page.toString());
    return this.http.get<Product[]>(url, { params });
  }

  getPagesCount():Observable<number>
  {
    const url = `${this.baseUrl}/api/products/pagescount`;
    return this.http.get<number>(url, undefined);
  }

  getProductById(id: number): Observable<Product> {
    const url = `${this.baseUrl}/api/products/${id}`;
    return this.http.get<Product>(url);
  }
}