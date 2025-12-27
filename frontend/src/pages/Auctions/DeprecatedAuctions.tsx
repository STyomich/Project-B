import { AxiosResponse } from "axios";
import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import api from "../../services/api";
import { AuctionInfoListItemDto } from "../../types/auctionInfo";
import { useTranslation } from "react-i18next";

interface LiveAuctionsProps {
  searchByNameParams?: string;
}

export default function DeprecatedAuctions({
  searchByNameParams,
}: LiveAuctionsProps) {
  const { t } = useTranslation();
  const [auctions, setAuctions] = useState<AuctionInfoListItemDto[]>([]);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    const fetchAuctions = async () => {
      try {
        const response =
          (await api.AuctionInfo.getDeprecatedAuctionInfoList()) as AxiosResponse;
        setAuctions(response.status === 200 ? response.data : []);
      } catch (error) {
        console.error("Failed to fetch auctions", error);
      } finally {
        setLoading(false);
      }
    };

    fetchAuctions();
  }, []);

  // Filter auctions by carName or carModel
  const filteredAuctions = searchByNameParams
    ? auctions.filter((auction) => {
        const carTopic = auction.car?.carTopic;
        const searchTerm = searchByNameParams.toLowerCase();
        return (
          carTopic?.carName?.toLowerCase().includes(searchTerm) ||
          carTopic?.carModel?.toLowerCase().includes(searchTerm)
        );
      })
    : auctions;

  if (loading) {
    return (
      <div className="flex justify-center items-center h-64">
        <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-gray-900"></div>
      </div>
    );
  }

  return (
    <div className="flex flex-col items-center justify-start min-h-screen bg-gray-50 p-6">
      <h1 className="text-3xl font-bold mb-6">{t("Deprecated Auctions")}</h1>
      <div className="flex flex-row flex-wrap gap-6 bg-gray-100 rounded-xl">
        {filteredAuctions.map((auction) => (
          <Link to={`/auctions/${auction.id}`} key={auction.id}>
            <div className="mt-5 ml-5 bg-white shadow-md rounded-xl overflow-hidden border border-gray-200 w-[300px] transform transition-transform duration-500 hover:scale-105">
              <img
                src={
                  auction.car?.carMainImage?.imageUrl ||
                  "/assets/images/no-image-icon.png"
                }
                alt="Car"
                className="w-full h-48 object-cover"
              />
              <div className="p-4">
                <h2 className="text-xl font-semibold mb-2">
                  {auction.car?.carTopic.carYear}{" "}
                  {auction.car?.carTopic.carName}{" "}
                  {auction.car?.carTopic.carModel}
                </h2>
                <p className="text-sm text-gray-600 mb-1">
                  <strong>{t("Start Price")}:</strong> $
                  {auction.startPrice.toLocaleString()}
                </p>
                <p className="text-sm text-gray-600 mb-1">
                  <strong>{t("Buyout Price")}:</strong> $
                  {auction.buyoutPrice.toLocaleString()}
                </p>
                <p className="text-sm text-gray-600 mb-1">
                  <strong>{t("Max Bid")}:</strong>{" "}
                  {auction.maxBid
                    ? `$${auction.maxBid.bidAmount.toLocaleString()}`
                    : t("No bids yet")}
                </p>
                <p className="text-sm text-gray-600 mb-1">
                  <strong>{t("Start Date")}:</strong>{" "}
                  {new Date(
                    new Date(auction.startDate).getTime() + 3 * 60 * 60 * 1000
                  ).toLocaleString()}
                </p>
                <p className="text-sm text-gray-600">
                  <strong>{t("End Date")}:</strong>{" "}
                  {new Date(
                    new Date(auction.endDate).getTime() + 3 * 60 * 60 * 1000
                  ).toLocaleString()}
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
                          : "bg-red-100 text-red-800"
                      }`}
                    >
                      {t("Status")}:{" "}
                      {isLive ? t("Live") : t("Deprecated-status")}
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
