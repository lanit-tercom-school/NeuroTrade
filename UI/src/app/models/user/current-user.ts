
import {UserInfo} from './user-info';

export class CurrentUser implements UserInfo {

    token: string;
    info: UserInfo;

    // UserInfo
    id: number;
    name: string;

    constructor(_token: string, _id?: number, _name?: string) {
        this.token = _token;
        this.id = _id;
        this.name = _name;
    }

}
