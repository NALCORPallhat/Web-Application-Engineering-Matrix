import { Component, OnInit } from '@angular/core';
import { IUser } from '../../models/i-user';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {

  user: IUser;
  model = {};

  constructor(private route: ActivatedRoute, private userService: UserService) { }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    // console.log("id: ", id); // testing
    this.userService.getUser(id).subscribe(u => this.user = u);
    this.model = this.user;
    // console.log("user: ", this.user); // testing
  }

  update() {
    console.log("MODEL:" + this.model);
    this.userService.updateUser(this.user.id, this.model).subscribe(
      x => console.log(x) // could be removed later
    );
  }

}
