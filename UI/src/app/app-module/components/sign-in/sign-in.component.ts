import { Component, OnInit } from '@angular/core';
import {UserService} from '../../../services/user.service';
import {CurrentUser} from '../../../models/user/current-user';
import {SignInUser} from '../../../models/user/sign-in-user';
import {AuthService} from '../../../services/auth.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css', '../shared/styles/sign-form.css']
})
export class SignInComponent implements OnInit {


  constructor(private userService: UserService, private authService: AuthService) { }
  ngOnInit() {
  }

  Authorize(email: string, password: string) {
    const user: SignInUser = {
      email: email,
      password: password
    };
    this.authService.authorize(user)
      .subscribe((response) => {
        console.log(response);
      });
  }
}
