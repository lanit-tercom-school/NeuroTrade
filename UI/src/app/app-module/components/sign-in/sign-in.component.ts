import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';

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

  form: FormGroup;
  constructor(private userService: UserService, private authService: AuthService) { }
  ngOnInit() {
    //this.message = new Message('danger', '');
    this.form = new FormGroup({
      'email': new FormControl(null, [Validators.required, Validators.email]),
      'password': new FormControl(null, [Validators.required])
    });
  }


  Authorize(email: string, password: string) {
    console.log(this.form);
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
