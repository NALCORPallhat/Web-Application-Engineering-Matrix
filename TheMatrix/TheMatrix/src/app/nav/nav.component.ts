import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { UserService } from '../services/user.service';
import { IUser } from '../models/i-user';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  isLoggedIn = false;
  currentUser: any;
  alerts: any = [];
  model = {};

  constructor(private authService: AuthService, private userService: UserService) { }

  ngOnInit() {
    var isCollapsed = false;
    this.isLoggedIn = this.authService.isLoggedIn();

    if (this.isLoggedIn) {
      this.currentUser = JSON.parse(localStorage.getItem('user'));
    }
  }

  onClosedAlert(dismissedAlert: any) {
    this.alerts = this.alerts.filter(alert => alert !== dismissedAlert);
  }

  login() {
    if (this.authService.isLoggedIn()) {
      // already logged in!
      this.alerts = [{
        type: 'danger',
        msg: 'Already logged in!'
      }];
    }
    else {
      this.authService.login(this.model).subscribe(data => {
        this.model = data;
        this.alerts = [{
          type: 'success',
          msg: 'Successfully signed in!'
        }]
      },
        error => this.alerts = [{
          type: 'danger',
          msg: 'Invalid credentials!'
        }]);
    }
  }

  logout() {
    this.authService.logout();
    if (localStorage.getItem('token') == null) {
      this.alerts = [{
        type: 'success',
        msg: 'Successfully signed out!'
      }];
    }
    else {
      this.alerts = [{
        type: 'danger',
        msg: 'Error with signing out!'
      }];
    }
  }

}
