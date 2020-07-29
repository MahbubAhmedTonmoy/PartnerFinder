import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from 'src/app/service/user.service';
import { AlertifyService } from 'src/app/service/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/models/user';
import { NgForm } from '@angular/forms';
import { AuthService } from 'src/app/service/auth.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  
  @ViewChild('editForm') editForm: NgForm;
  user: User
  constructor(private userService: UserService,
    private alert: AlertifyService,
    private route: ActivatedRoute, 
    public authservice: AuthService) { }

  ngOnInit() {
    this.loadUser();
  }

  loadUser(){
    this.userService.getUser(this.route.snapshot.params['id']).subscribe((user: User) =>{
      this.user = user;
      console.log(this.user);
    }, error => {
      this.alert.error(error);
    })
  }

  updateUser(){
    console.log(this.user);
    this.userService.updateUser(this.authservice.decodedToken.nameid , this.user)
      .subscribe(next => {
        this.alert.success('updated');
        this.editForm.reset(this.user);
      }, error => {
        this.alert.error(error);
      });
  }
}
