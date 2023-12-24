import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { of } from 'rxjs/internal/observable/of';
import { catchError } from 'rxjs/internal/operators/catchError';
import { tap } from 'rxjs/internal/operators/tap';
import { Login } from 'src/app/models/auth/login';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { Subscription } from 'rxjs';
import { AuthUser } from 'src/app/models/auth/auth-user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit, OnDestroy {
  loginForm: FormGroup;
  errorMessage: string = '';
  currentUser: AuthUser | undefined;
  private subscription: Subscription = new Subscription()

  constructor(
    private fb: FormBuilder,
    private authService: AuthenticationService,
    private router: Router,
  ) {

    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }
  ngOnInit(): void {
    this.currentUser = this.authService.getCurrentUser();
    this.authService.authUser$.subscribe((user: AuthUser | undefined) => {
      this.currentUser = user;
    });
  }

  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  get email() {
    return this.loginForm.get('email');
  }

  get password() {
    return this.loginForm.get('password');
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const user: Login = {
        email: this.loginForm.value.email,
        password: this.loginForm.value.password
      };

      this.subscription = this.authService.login(user).pipe(
        tap(_ => this.router.navigate(['/'])),
        catchError(error => {
          this.errorMessage = error;
          return of(null);
        })
      ).subscribe();
    }
  }

  logout() {
    this.authService.logout();
  }
}
