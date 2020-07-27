import { Component, OnInit } from '@angular/core';
import { AuthService } from '../service/auth.service';
import { AlertifyService } from '../service/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  
  model: any = {};
  constructor(private authservice: AuthService, private alerfity : AlertifyService) { }

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
    });
  }

  IsLoggedIn(){
    const token = localStorage.getItem('token');
    if(token){
      return true;
    }
    else{ return false;}
  }

  Logout(){
    localStorage.removeItem('token')
  }

}
