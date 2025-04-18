import { AuctionInfo } from "./auctionInfo";
import { User } from "./user";

export interface AuctionBid {
    auctionInfoId: string;
    userId: string;
    bidAmount: number;
    bidDate: Date;
    auctionInfo: AuctionInfo | null;
    user: User | null;
}

export interface AuctionBidDto {
    auctionInfoId: string;
    userId: string;
    bidAmount: number;
    bidDate: Date;
}