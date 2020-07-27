import { Component, OnInit } from '@angular/core';
import { AuthService } from '../service/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  
  model: any = {};
  constructor(private authservice: AuthService) { }

  ngOnInit() {
  }

  Login(){
    console.log(this.model);
    this.authservice.login(this.model).subscribe(next => {
      console.log("loged in");
    }, error => {
      console.log(error);
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
