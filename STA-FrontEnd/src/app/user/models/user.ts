import { BaseModel } from 'src/app/shared/models/base-model';

export interface User extends BaseModel {
    username: string;
    displayName: string;
}