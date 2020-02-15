export interface AuthenticationData {
    UserId: string;
    LastLoginDateTime: Date;
    IsAdmin: boolean;
    IsBanned: boolean;
    DisplayName: string;
    About: string;
    StatusVal: number;
    Token: string;
}
