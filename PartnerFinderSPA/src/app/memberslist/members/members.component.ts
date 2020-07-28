import { Component, OnInit } from '@angular/core';
import { User } from '../../models/user';
import { UserService } from '../../service/user.service';
import { AlertifyService } from '../../service/alertify.service';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.css']
})
export class MembersComponent implements OnInit {
 users: User[];
 
  constructor(private userService: UserService, private alert: AlertifyService) { }

  ngOnInit() {
    this.loadUsers();
  }


  loadUsers(){
    this.userService.getUsers().subscribe((users: User[]) =>{
      this.users = users;
      console.log(this.users);
    }, error => {
      console.log(error);
      this.alert.error(error);
    })
  }
}
