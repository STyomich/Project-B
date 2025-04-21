import { AxiosResponse } from "axios";
import { useState, useEffect } from "react";
import { AuctionInfoListItemDto } from "../../types/auctionInfo";
import api from "../../services/api";
import { Link } from "react-router-dom";

export default function Auctions() {
  const [auctions, setAuctions] = useState<AuctionInfoListItemDto[]>([]);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    const fetchAuctions = async () => {
      try {
        const response =
          (await api.AuctionInfo.getAuctionInfoList()) as AxiosResponse;
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
    <div className="flex flex-col items-center justify-start min-h-screen bg-gray-50 p-6 ">
      <h1 className="text-3xl font-bold mb-6">Active Auctions</h1>
      <div className="flex flex-col items-center gap-6 transform transition-transform duration-500 hover:scale-105">
        {auctions.map((auction) => (
          <Link to={`/auctions/${auction.id}`} key={auction.id}>
            <div
              key={auction.id}
              className="bg-white shadow-md rounded-xl overflow-hidden border border-gray-200 w-[300px]"
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
                  {auction.car?.carTopic.carYear}{" "}
                  {auction.car?.carTopic.carName}{" "}
                  {auction.car?.carTopic.carModel}
                </h2>
                <p className="text-sm text-gray-600 mb-1">
                  <strong>Start Price:</strong> $
                  {auction.startPrice.toLocaleString()}
                </p>
                <p className="text-sm text-gray-600 mb-1">
                  <strong>Buyout Price:</strong> $
                  {auction.buyoutPrice.toLocaleString()}
                </p>
                <p className="text-sm text-gray-600 mb-1">
                  <strong>Max Bid:</strong>{" "}
                  {auction.maxBid
                    ? `$${auction.maxBid.bidAmount.toLocaleString()}`
                    : "No bids yet"}
                </p>
                <p className="text-sm text-gray-600 mb-1">
                  <strong>Start:</strong>{" "}
                  {new Date(auction.startDate).toLocaleString()}
                </p>
                <p className="text-sm text-gray-600">
                  <strong>End:</strong>{" "}
                  {new Date(auction.endDate).toLocaleString()}
                </p>
                {(() => {
                  const now = new Date();
                  const start = new Date(auction.startDate);
                  const end = new Date(auction.endDate);
                  const isLive = now >= start && now <= end;

                  return (
                    <span
                      className={`inline-block text-sm font-semibold px-3 py-1 rounded-full mt-3 ${
                        isLive
                          ? "bg-green-100 text-green-800"
                          : "bg-yellow-100 text-yellow-800"
                      }`}
                    >
                      Status: {isLive ? "Live" : "Not in live"}
                    </span>
                  );
                })()}
              </div>
            </div>
          </Link>
        ))}
      </div>
    </div>
  );
}
