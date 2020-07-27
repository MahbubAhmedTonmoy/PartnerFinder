import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {BehaviorSubject} from 'rxjs';
import { map } from 'rxjs/operators';
import { Token } from '../models/token';
import { UserForRegistration } from '../models/UserForRegistration';
import { JwtHelperService } from "@auth0/angular-jwt";


@Injectable({   // Injectable decorator
  providedIn: 'root'  //app.module.ts [providers] e maira dia asa lagbe
})
export class AuthService {
  baseUrl = 'https://localhost:44322/api/auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;

constructor(private http: HttpClient) { }

login(model: any){
    return this.http.post(this.baseUrl + 'login', model)
    .pipe(map((response: Token) => {
      const user = response;
      if(user) {
        localStorage.setItem('token', user.accessToken);
        this.decodedToken = this.jwtHelper.decodeToken(user.accessToken);
        console.log(this.decodedToken);
      }
    }));
  }

  register(user: UserForRegistration) {
    return this.http.post(this.baseUrl+'registration', user);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }
}
