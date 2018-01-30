import { Component, OnInit } from '@angular/core';

import { SignUpUser } from '../../../models/user/sign-up-user';
import {AuthService} from '../../../services/auth.service';
import {AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators} from '@angular/forms';
import { UserService } from '../../../services/user.service';
import {CurrentUser} from '../../../models/user/current-user';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css', '../shared/styles/sign-form.css']
})
export class SignUpComponent implements OnInit {

  form: FormGroup;
  constructor(
    private authService: AuthService,
    private userService: UserService
  ) { }

  ngOnInit() {
    //this.message = new Message('danger', '');
    this.form = new FormGroup({
      'person': new FormGroup({
        'name': new FormControl(null,[Validators.required,this.personValidator]),
        'surname': new FormControl(null,[Validators.required,this.personValidator])
      }),
      'email': new FormControl(null, [Validators.required, Validators.email],
        this.forbiddenEmail.bind(this)),
      'passGr': new FormGroup({
        'password': new FormControl(null, [Validators.required, Validators.minLength(7),this.passwordValidator]),
        'passConfirm': new FormControl(null)
      }, this.areEqual)

    });
  }

  private personValidator (control: FormControl) {
    const value = control.value;
    const hasLatinLetters = /[A-Z]|[a-z]/.test(value);
    const hasRussianLetters = /[а-я]|[А-Я]|[ёЁ]/.test(value);
    const passwordValid =
      (hasRussianLetters && !hasLatinLetters) || (!hasRussianLetters && hasLatinLetters);
    if (!passwordValid) {
      return {
        'invalidPersonalData' : true
      };
    }
    return null;
  }

  private passwordValidator(control: FormControl) {
    const value = control.value;
    const hasNumber = /[0-9]/.test(value);

    if (!hasNumber) {
      return {
        'hasNumber' : true
      };
    }
    return null;
  }

  private areEqual(c: AbstractControl): ValidationErrors | null {
    const keys: string[] = Object.keys(c.value);
    for (const i in keys) {
      if (i !== '0' && c.value[ keys[ +i - 1 ] ] !== c.value[ keys[ i ] ]) {
        return { 'areNotEqual': true };
      }
    }
  }

  forbiddenEmail(control: FormControl): Promise<any> {
    return new Promise((resolve, reject) => {
      this.userService.getUserByEmail(control.value)
        .subscribe((user: CurrentUser) => {
          if (user) {
            resolve({'forbiddenEmail': true})
          } else {
            resolve(null);
          }
        })
    });
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
