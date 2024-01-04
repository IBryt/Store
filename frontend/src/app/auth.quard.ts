import { Injectable } from '@angular/core';
import { AuthenticationService } from './services/authentication.service';
import { Observable } from 'rxjs/internal/Observable';
import { Router, UrlTree } from '@angular/router';


@Injectable({
    providedIn: 'root',
})
export class AuthGuard {
    constructor(
        private authService: AuthenticationService,
        private router: Router) { }

    canActivate():
        | Observable<boolean | UrlTree>
        | Promise<boolean | UrlTree>
        | boolean
        | UrlTree {
        if (!this.authService.isAuthenticated()) {
            this.router.navigate(['/login']);
            return false;
        }
        this.authService.isAuthenticated();
        return true;
    }
}