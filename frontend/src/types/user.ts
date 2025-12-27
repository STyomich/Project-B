import { OrganizationShortInfo } from "./organization";

export interface User{
    id: string;
    roleId: string;
    userName: string;
    userSurname: string;
    userNickname: string;
    email: string;
    avatar: string;
    token: string;
    role: string;
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

export interface UserShortInfo{
    userName: string;
    userSurname: string;
    userNickname: string;
    email: string;
    avatarUrl: string;
    organization: OrganizationShortInfo | null;
}

