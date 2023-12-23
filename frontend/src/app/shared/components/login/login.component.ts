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
    this.login.email = "lewis1@gmail.com";
    this.login.password = "P@ssword123!";
  }

  Login(login:Login )
  {
    this.authService.login(login).subscribe((jwt) => {
      
      localStorage.setItem('jwtToken', jwt.token);
      const res = this.decodeJwt(jwt.token);
      console.log(res)
    }, timeout)
  }

  Register(register:Register )
  {
    this.authService.register(register).subscribe()
  }

  private decodeJwt(token: string): any {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    const jsonPayload = decodeURIComponent(
      atob(base64)
        .split('')
        .map((c) => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
        .join('')
    );

    return JSON.parse(jsonPayload);
  }
}
