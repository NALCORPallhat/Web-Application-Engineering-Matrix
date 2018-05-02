import { Component, OnInit } from '@angular/core';
import { IUser } from '../../models/i-user';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

  constructor(private userService: UserService) { }

  members: any;

  ngOnInit() {
    this.userService.getUsers().subscribe(m => { this.members = m });
    console.log(this.members);
  }

}
