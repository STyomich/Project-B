import { AxiosResponse } from "axios";
import { useState, useEffect } from "react";
import { AuctionInfoListItemDto } from "../../types/auctionInfo";
import api from "../../services/api";

export default function Auctions() {
    const [auctions, setAuctions] = useState<AuctionInfoListItemDto[]>([]);
    const [loading, setLoading] = useState<boolean>(true);
  
    useEffect(() => {
      const fetchAuctions = async () => {
        try {
          const response = await api.AuctionInfo.getAuctionInfoList() as AxiosResponse;
          setAuctions(response.status === 200 ? response.data : []);
        } catch (error) {
          console.error("Failed to fetch auctions", error);
        } finally {
          setLoading(false);
        }
      };
  
      fetchAuctions();
    }, []);
  
    if (loading) {
      return (
        <div className="flex justify-center items-center h-64">
          <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-gray-900"></div>
        </div>
      );
    }
  
    return (
      <div className="p-6">
        <h1 className="text-3xl font-bold mb-6">Active Auctions</h1>
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-6">
          {auctions.map((auction) => (
            <div
              key={auction.id}
              className="bg-white shadow-md rounded-xl overflow-hidden border border-gray-200"
            >
              {auction.car?.carMainImage?.imageUrl && (
                <img
                  src={auction.car.carMainImage.imageUrl}
                  alt="Car"
                  className="w-full h-48 object-cover"
                />
              )}
              <div className="p-4">
                <h2 className="text-xl font-semibold mb-2">
                  {auction.car?.carTopic.carYear} {auction.car?.carTopic.carName} {auction.car?.carTopic.carModel}
                </h2>
                <p className="text-sm text-gray-600 mb-1">
                  <strong>Start Price:</strong> ${auction.startPrice.toLocaleString()}
                </p>
                <p className="text-sm text-gray-600 mb-1">
                  <strong>Buyout Price:</strong> ${auction.buyoutPrice.toLocaleString()}
                </p>
                <p className="text-sm text-gray-600 mb-1">
                  <strong>Max Bid:</strong>{" "}
                  {auction.maxBid ? `$${auction.maxBid.bidAmount.toLocaleString()}` : "No bids yet"}
                </p>
                <p className="text-sm text-gray-600 mb-1">
                  <strong>Start:</strong>{" "}
                  {new Date(auction.startDate).toLocaleString()}
                </p>
                <p className="text-sm text-gray-600">
                  <strong>End:</strong>{" "}
                  {new Date(auction.endDate).toLocaleString()}
                </p>
              </div>
            </div>
          ))}
        </div>
      </div>
    );
  }