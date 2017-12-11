import { Http, Response } from '@angular/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { environment} from '../../environments/environment';

import {CurrentUser} from '../models/user/current-user';
import {SignInUser} from '../models/user/sign-in-user';
import {SignUpUser} from '../models/user/sign-up-user';

@Injectable()
export class AuthService {
  constructor(private http: Http) {}

  authorize(_user: SignInUser): Observable<CurrentUser> {
    const response = this.http.get(environment.apiUrl + `/users?email=${_user.email}&password=${_user.password}`)
      .map((response: Response) => response.json());
    console.log(response);

    return response;
    /*return this.http.get(environment.apiUrl + `/users?email=${_user.email}&password=${_user.password}`)
      .map((response: Response) => response.json())
      .map((user: CurrentUser[]) => user[0] ? user[0] : undefined);*/
  }
  register(_user: SignUpUser): Observable<CurrentUser> {
    return this.http.post(environment.apiUrl + `/users`, _user)
      .map((response: Response) => response.json());
  }
}
