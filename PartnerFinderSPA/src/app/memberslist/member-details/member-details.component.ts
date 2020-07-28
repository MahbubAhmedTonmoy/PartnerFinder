import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/service/user.service';
import { AlertifyService } from 'src/app/service/alertify.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-member-details',
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.css']
})
export class MemberDetailsComponent implements OnInit {
  
  user: User
  constructor(private userService: UserService,
    private alert: AlertifyService,
    private route: ActivatedRoute) { } // route for pass is in url

  ngOnInit() {
    this.loadUser()
  }

  loadUser(){
    this.userService.getUser(this.route.snapshot.params['id']).subscribe((user: User) =>{
      this.user = user;
      console.log(this.user);
    }, error => {
      this.alert.error(error);
    })
  }

}
