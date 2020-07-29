import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user';

/*
const httpOptions = {   // token pass in header
  headers: new HttpHeaders ({
    'Authorization': 'Bearer '+localStorage.getItem('token')
  })
};*/  // comment cz jwt in app.module.ts e korci sobai pabe

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = environment.apiUrl +'user/';

constructor(private Http: HttpClient) { }
  getUsers(): Observable<User[]> // return observable type user
  {
    //return this.Http.get<User[]>( this.baseUrl + 'all', httpOptions);
    return this.Http.get<User[]>( this.baseUrl + 'all');
  }

  getUser(id): Observable<User> // return observable type user
  {
    return this.Http.get<User>( this.baseUrl + id);
  }
  updateUser(id: string, user: User){
    return this.Http.put(this.baseUrl + 'edit/'+ id, user);
  }
}
