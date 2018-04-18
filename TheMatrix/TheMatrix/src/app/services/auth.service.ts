import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable()
export class AuthService {

  baseUrl = 'http://localhost:61051/api/auth/';

  constructor(private http: HttpClient) { }

  login(userModel) {
    console.log('In login()!');
    const contentHeader = new HttpHeaders({ 'Content-type': 'application/json' });
    return this.http.post((this.baseUrl + 'login'), userModel, { headers: contentHeader });
  }

  register(userModel) {
    console.log('In register()!');
    var registerUserModel = { 'UserName': userModel.UserName, 'Password': userModel.Password }
    const contentHeader = new HttpHeaders({ 'Content-type': 'application/json' });
    return this.http.post((this.baseUrl + 'register'), registerUserModel, { headers: contentHeader });
  }
}
