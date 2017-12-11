import { Component, OnInit } from '@angular/core';

import { SignUpUser } from '../../../models/user/sign-up-user';
import {AuthService} from '../../../services/auth.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css', '../shared/styles/sign-form.css']
})
export class SignUpComponent implements OnInit {

  constructor(private authService: AuthService) { }

  ngOnInit() {

  }

  isRegDataValid(user: SignUpUser, passwordConfirmation: string): boolean {

    if (user.password !== passwordConfirmation) {
      console.log('Passwords don\'t match');
      return false;
    }

    return true;

  }

  Register(name: string, surname: string, email: string, password: string, passwordConfirmation: string) {

    const user: SignUpUser = {
      name: name + ' ' + surname,
      email: email,
      password: password
    };

    if (this.isRegDataValid(user, passwordConfirmation)) {

      this.authService.register(user)
        .subscribe(response => {
          console.log(response);
        });

    }

  }

}
