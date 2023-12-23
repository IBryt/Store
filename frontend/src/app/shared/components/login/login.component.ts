import { Component } from '@angular/core';
import { timeout } from 'rxjs';
import { JwtAuth } from 'src/app/models/auth/jwt-auth';
import { Login } from 'src/app/models/auth/login';
import { Register } from 'src/app/models/auth/register';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  login:Login = {} as Login;
  register:Register = {} as Register;
  private jwt:JwtAuth = {} as JwtAuth;

  constructor(
    private authService: AuthenticationService
  ) { 
  }

  Login(login:Login )
  {
    this.authService.login(login).subscribe((jwt) => {
      localStorage.setItem('jwtToken', this.jwt.token);
    }, timeout)
  }

  Register(register:Register )
  {
    this.authService.register(register).subscribe()
  }
}
