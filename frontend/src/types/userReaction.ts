import { Post } from "./post";
import { UserShortInfo } from "./user";

export interface UserReaction {
    id: string;
    userId: string;
    postId: string;
    user: UserShortInfo;
    post: Post;
}