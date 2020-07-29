import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, from } from 'rxjs';
import { User } from '../models/user';
import {PaginatedResult } from '../models/paggination';
import { map } from 'rxjs/operators';

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
  getUsers(page?, itemPerPage?): Observable<User[]> // return observable type user
  {
    //return this.Http.get<User[]>( this.baseUrl + 'all', httpOptions);
    const paginatedResult: PaginatedResult<User[]> = new PaginatedResult<User[]>();
    let params = new HttpParams();
    if(page != null && itemPerPage != null){
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemPerPage);
    }
    return this.Http.get<User[]>( this.baseUrl + 'all', {params: params});
    
}

  getUser(id): Observable<User> // return observable type user
  {
    return this.Http.get<User>( this.baseUrl + id);
  }
  updateUser(id: string, user: User){
    return this.Http.put(this.baseUrl + 'edit/'+ id, user);
  }
}
