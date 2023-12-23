import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Register } from '../models/auth/register';
import { Observable } from 'rxjs';

import { environment } from '../environments/environment';
import { Login } from '../models/auth/login';
import { JwtAuth } from '../models/auth/jwt-auth';


@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private registerUrl = "/api/AuthManager/Register";
  private loginUrl = "/api/AuthManager/Login";

  constructor(
    private http: HttpClient
  ) { }

  register(user: Register): Observable<JwtAuth> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    return this.http.post<JwtAuth>(`${environment.baseUrl}${this.registerUrl}`, user, { headers });
  }

  login(user: Login): Observable<JwtAuth> {
    return this.http.post<JwtAuth>(`${environment.baseUrl}${this.loginUrl}`, user);
  }
}
