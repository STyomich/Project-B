import { Role } from "./role";

export interface User{
    id: string;
    roleId: string;
    userName: string;
    userSurname: string;
    userNickname: string;
    email: string;
    avatarUrl: string;
    token: string;
    role: Role;
}

export interface UserLoginValues{
    email: string;
    password: string;
}

export interface UserRegisterValues{
    email: string;
    userNickname: string;
    userName: string;
    userSurname: string;
    password: string;
}

