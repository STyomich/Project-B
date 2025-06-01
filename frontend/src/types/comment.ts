import { UserShortInfo } from "./user";

export interface Comment {
    id: string;
    postId: string;
    userId: string;
    content: string;
    createdAt: Date;
    user: UserShortInfo;
}