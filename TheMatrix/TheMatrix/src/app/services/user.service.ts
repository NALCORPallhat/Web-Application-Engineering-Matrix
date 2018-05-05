import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { IUser } from '../models/i-user';
import { IPhoto } from '../models/i-photo';
import { HttpClient } from '@angular/common/http';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable()
export class UserService {

  baseUrl = environment.apiUrl + '/users';

  constructor(private http: HttpClient, private jwtHelperService: JwtHelperService) { }

  getUsers() {
    return this.http.get<IUser[]>(this.baseUrl)
      .map((result: IUser[]) => {
        console.log(result); // testing
        return result;
      });
  }

  getUser(id) {
    return this.http.get<IUser>(this.baseUrl + '/getuser/' + id)
      .map((result: IUser) => {
        console.log(result); // testing
        return result;
      });
  }

  updateUser(id, edits) {
    return this.http.put(this.baseUrl + '/updateuser/' + id, edits)
      .map(result => {
        return result;
      });
  }
}
