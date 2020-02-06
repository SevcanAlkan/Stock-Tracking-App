import { User } from 'src/app/user';

export interface AuthenticationData {
    token: string;
    expireTime: number;
    user: User;
}