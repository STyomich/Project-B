import { UserShortInfo } from "./user";
import { UserReaction } from "./userReaction";
import { Comment } from "./comment";

export interface Post {
    id: string;
    userId: string;
    title: string;
    content: string;
    createdAt: Date;
    reactionsCount: number;
    user: UserShortInfo;
}
export interface PostInfo {
    id: string;
    userId: string;
    title: string;
    content: string;
    createdAt: Date;
    reactionsCount: number;
    user: UserShortInfo;
    userReactions: UserReaction[];
    comments: Comment[];
}