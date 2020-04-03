import { Base } from './base';
import { UserType } from './userType';

export interface UserDetail extends Base {
    FirstName: string;
    LastName: string;
    Username: string;
    Address: string;
    PhoneNo: string;
    Email: string;
    Age: number;
    Gender: string;
    Admin: boolean;
    UserType: UserType;
    Valid: boolean;
}