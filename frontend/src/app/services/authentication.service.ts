import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Register } from '../models/auth/register';
import { Observable, Subject, catchError, map, throwError } from 'rxjs';

import { environment } from '../environments/environment';
import { Login } from '../models/auth/login';
import { JwtAuth } from '../models/auth/jwt-auth';
import { AuthUser } from '../models/auth/auth-user';
import { jwtDecode } from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService implements OnInit {
  private registerUrl = "/api/AuthManager/Register";
  private loginUrl = "/api/AuthManager/Login";
  authUser: AuthUser | undefined;

  private authUserSubject = new Subject<AuthUser | undefined>();
  authUser$ = this.authUserSubject.asObservable();

  constructor(
    private http: HttpClient,
  ) { }

  ngOnInit(): void {

  }

  register(user: Register): Observable<JwtAuth> {
    return this.http.post<JwtAuth>(`${environment.baseUrl}${this.registerUrl}`, user)
      .pipe(
        catchError(error => {
          throw new Error(error.error);
        })
      );
  }

  login(user: Login): Observable<AuthUser> {
    return this.http.post<JwtAuth>(`${environment.baseUrl}${this.loginUrl}`, user)
      .pipe(
        map(jwt => {
          if (jwt && jwt.token && jwt.result) {
            localStorage.setItem('jwtToken', jwt.token);
            const authUser = jwtDecode<AuthUser>(jwt.token);
            this.authUserSubject.next(authUser);
            return authUser;
          } else {
            throw new Error(jwt.error || 'Unknown error from the server');
          }
        }),
        catchError((error) => {
          throw new Error(error.error);
        })
      );
  }

  logout() {
    this.authUser = undefined;
    localStorage.removeItem('jwtToken');
    this.authUserSubject.next(undefined);
  }

  getCurrentUser(): AuthUser | undefined {
    const token = localStorage.getItem('jwtToken');
    if (token) {
      const authUser = jwtDecode<AuthUser>(token);
      return authUser;
    }
    return undefined
  }
}
