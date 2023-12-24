import { Component, OnInit } from '@angular/core';
import { AuthUser } from 'src/app/models/auth/auth-user';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  authUser: AuthUser | undefined;

  constructor(
    private authService: AuthenticationService
  ) { }

  ngOnInit(): void {
    this.authUser = this.authService.getCurrentUser();
    this.authService.authUser$.subscribe((user: AuthUser | undefined) => {
      this.authUser = user;
    });
  }
}
