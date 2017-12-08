import { Http, Response } from '@angular/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';


import {CurrentUser} from '../models/user/current-user';

@Injectable()
export class UserService {
  constructor(private http: Http) {}

  getUserByToken(token: string): Observable<CurrentUser> {
    return this.http.get(`http://localhost:3000/users?token=${token}`)
      .map((response: Response) => response.json())
      .map((user: CurrentUser[]) => user[0] ? user[0] : undefined);
  }
}
