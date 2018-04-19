import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  isLoggedIn = false;
  currentUser = "";

  constructor(private authService: AuthService) {
    var isCollapsed = false;
    this.isLoggedIn = authService.isLoggedIn();
    /*
    if (this.isLoggedIn)
      this.currentUser = JSON.parse(localStorage.getItem('user')).UserName;
    */
  }

  ngOnInit() {
  }

  alerts: any = [];
  model = {};

  onClosedAlert(dismissedAlert: any) {
    this.alerts = this.alerts.filter(alert => alert !== dismissedAlert);
  }

  login() {
    if (localStorage.getItem('token') != null) {
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
