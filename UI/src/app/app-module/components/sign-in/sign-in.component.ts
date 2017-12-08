import { Component, OnInit } from '@angular/core';
import {UserService} from '../../../services/user.service';
import {CurrentUser} from '../../../models/user/current-user';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css', '../shared/styles/sign-form.css']
})
export class SignInComponent implements OnInit {

  constructor(private service: UserService) { }
  ngOnInit() {
  }

}
