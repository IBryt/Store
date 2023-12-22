import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../models/product/product';
import { environment } from '../environments/environment';
import { Params } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private baseUrl = environment.baseUrl;

  constructor(
    private http: HttpClient,
  ) { }

  getProducts(params: Params): Observable<Product[]> {
    const url = `${this.baseUrl}/api/products`;
    this.removeUndefinedParams(params);
    return this.http.get<Product[]>(url, { params });
  }

  getPagesCount(params: Params): Observable<number> {
    const url = `${this.baseUrl}/api/products/pagescount`;
    this.removeUndefinedParams(params);
    return this.http.get<number>(url, { params });
  }

  getProductById(id: number): Observable<Product> {
    const url = `${this.baseUrl}/api/products/${id}`;
    return this.http.get<Product>(url);
  }

  private removeUndefinedParams(obj: Params) {
    for (let key in obj) {
      if (obj.hasOwnProperty(key) && obj[key] === undefined) {
        delete obj[key];
      }
    }
  }
}