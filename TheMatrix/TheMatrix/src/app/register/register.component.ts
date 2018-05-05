import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  model = {};

  register() {
    // console.log(this.model); // testing
    console.log(this.model);
    this.authService.register(this.model).subscribe(
      x => console.log(x)
    );
  }

}
