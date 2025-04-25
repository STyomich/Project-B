import { AuctionBidDto } from "./auctionBid";
import { Car, CarListItemDto } from "./car";
import { UserShortInfo } from "./user";

export interface AuctionInfo {
  id: string;
  carId: string;
  startPrice: number;
  buyoutPrice: number;
  startDate: Date;
  endDate: Date;
  isActive: boolean;
  car: Car | null;
}

export interface AuctionInfoDto {
  id: string;
  carId: string;
  startPrice: number;
  buyoutPrice: number;
  startDate: Date;
  endDate: Date;
  maxBid: AuctionBidDto | null;
  isActive: boolean;
  owner: UserShortInfo;
  car: Car | null;
}
export interface AuctionInfoCreateRequest {
  carId: string;
  startPrice: number;
  buyoutPrice: number;
  startDate: Date;
  endDate: Date;
}

export interface AuctionInfoListItemDto {
  id: string;
  carId: string;
  startPrice: number;
  buyoutPrice: number;
  startDate: Date;
  endDate: Date;
  isActive: boolean;
  car: CarListItemDto | null;
  maxBid: AuctionBidDto | null;
}
