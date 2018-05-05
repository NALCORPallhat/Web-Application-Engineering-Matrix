import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthUser } from '../models/auth-user';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from '../../environments/environment';

@Injectable()
export class AuthService {

  baseUrl = environment.apiUrl + '/auth';

  constructor(private http: HttpClient, private jwtHelperService: JwtHelperService) { }

  login(user) {
    return this.http.post<AuthUser>(this.baseUrl + '/login', user)
      .map((result: AuthUser) => {
        // console.log("USER RESULTS in login(): ", result); // testing
        if (result) {
          localStorage.setItem('token', result.tokenString);
          localStorage.setItem('user', JSON.stringify(result.user));
          console.log(JSON.stringify(result.user)); // testing
        }
        return result;
      });
  }

  isLoggedIn() {
    var token = localStorage.getItem('token');
    console.log("token: " + token); // testing

    var tokenExpired = this.jwtHelperService.isTokenExpired(localStorage.getItem('token'));
    console.log("tokenExpired: " + tokenExpired); // testing
    if (tokenExpired) {
      localStorage.clear();
    }
    return !tokenExpired;
  }

  logout() {
    localStorage.clear();
    console.log("token after clear(): " + localStorage.getItem('token')); // testing
  }


  register(userModel) {
    console.log('In register()!');
    const contentHeader = new HttpHeaders({ 'Content-type': 'application/json' });
    return this.http.post((this.baseUrl + '/register'), userModel, { headers: contentHeader });
  }
}
