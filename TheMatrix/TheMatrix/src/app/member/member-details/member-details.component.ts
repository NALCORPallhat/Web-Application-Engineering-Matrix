import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../../services/user.service';
import { IUser } from '../../models/i-user';

@Component({
  selector: 'app-member-details',
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.css']
})
export class MemberDetailsComponent implements OnInit {

  user: IUser;

  constructor(private route: ActivatedRoute, private userService: UserService) { }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    // console.log("id: ", id); // testing
    this.userService.getUser(id).subscribe(u => this.user = u);
    // console.log("user: ", this.user); // testing
  }

}
