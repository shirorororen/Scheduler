import { UserType } from './userType';

export interface UserAccount {
    UserId: number;
    FirstName: string;
    LastName: string;
    Username: string;
    Password: string;
    Token: string; 
    UserType: UserType;
    Admin: boolean;
}