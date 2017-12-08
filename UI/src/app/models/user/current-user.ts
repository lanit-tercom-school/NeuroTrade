import {UserInfo} from './user-info';

export class CurrentUser implements UserInfo {

    // UserInfo

    /*constructor(_token: string, _id?: number, _name?: string) {
        this.token = _token;
        this.id = _id;
        this.name = _name;
    }*/
  constructor(
    public id: number,
    public name: string,
    public email: string,
    public  token: string
  ) {}

}
