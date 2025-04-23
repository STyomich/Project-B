import { AuctionBidDto } from "./auctionBid";
import { Car, CarListItemDto } from "./car";

export interface AuctionInfo {
    id: string;
    carId: string;
    startPrice: number;
    buyoutPrice: number;
    startDate: Date;
    endDate: Date;
    car: Car | null;
}

export interface AuctionInfoDto{
    id: string;
    carId: string;
    startPrice: number;
    buyoutPrice: number;
    startDate: Date;
    endDate: Date;
    maxBid: AuctionBidDto | null;
    car: Car | null;
}
export interface AuctionInfoCreateRequest{
    carId: string;
    startPrice: number;
    buyoutPrice: number;
    startDate: Date;
    endDate: Date;
}

export interface AuctionInfoListItemDto{
    id: string;
    carId: string;
    startPrice: number;
    buyoutPrice: number;
    startDate: Date;
    endDate: Date;
    car: CarListItemDto | null;
    maxBid: AuctionBidDto | null;  
}