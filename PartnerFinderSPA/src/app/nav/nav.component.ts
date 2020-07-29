import { Component, OnInit } from '@angular/core';
import { AuthService } from '../service/auth.service';
import { AlertifyService } from '../service/alertify.service';
import { Router } from '@angular/router';
import { UserService } from '../service/user.service';
import { User } from '../models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  
  model: any = {};
  user: User;
  constructor(public authservice: AuthService, private alerfity : AlertifyService,
    private userService: UserService, private route: Router) { }

  ngOnInit() {
  }

  Login(){
    console.log(this.model);
    this.authservice.login(this.model).subscribe(next => {
      console.log("loged in");
      this.alerfity.success('login successful');
    }, error => {
      console.log(error);
      this.alerfity.error(error);
    }, () =>{
      this.route.navigate(['/members']);
    });
  }

  IsLoggedIn(){
    return this.authservice.loggedIn();
  }

  Logout(){
    localStorage.removeItem('token');
    this.route.navigate(['/home']); // *****
  }

}
