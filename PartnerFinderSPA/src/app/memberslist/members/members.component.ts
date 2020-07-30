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
 genderList = [{value: 'male', display: 'Males'}, {value: 'female', display: 'Females'}];
 pageSizeList = [{value: 5,  display: '5'}, {value: 10, display: '10'}, {value: 15, display: '15'}, {value: 20, display: '20'}]
 user: User = JSON.parse(localStorage.getItem('user'));
 userParams: any = {};
 pagingParams: any = {};
 
  constructor(private userService: UserService, private alert: AlertifyService) { }

  ngOnInit() {
    this.loadUsers();
    this.userParams.gender = this.user.gender === 'female' ? 'male' : 'female';
    this.userParams.minAge = 18;
    this.userParams.maxAge = 99;
    this.userParams.orderBy = 'lastActive';
    this.pagingParams.pageSize = this.pageSizeList[0];
    this.pagingParams.pageNumber = 1;
  }

  resetFilters() {
    this.userParams.gender = this.user.gender === 'female' ? 'male' : 'female';
    this.userParams.minAge = 18;
    this.userParams.maxAge = 99;
    this.loadUsers();
  }

  left(){
    if(this.pagingParams.pageNumber == 1) return;
    else{
      this.pagingParams.pageNumber  = this.pagingParams.pageNumber - 1
    }
  }
  right(){
    if(this.pagingParams.pageNumber == 100) return;
    else{
      this.pagingParams.pageNumber  = this.pagingParams.pageNumber + 1
    }
  }

  loadUsers(){
    this.userService.getUsers(this.pagingParams.pageNumber, this.pagingParams.pageSize).subscribe((users: User[]) =>{
      this.users = users;
      console.log(this.users);
    }, error => {
      console.log(error);
      this.alert.error(error);
    })
  }
}
