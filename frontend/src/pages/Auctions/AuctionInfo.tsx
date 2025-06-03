import { useEffect, useRef, useState } from "react";
import { AuctionBidDto } from "../../types/auctionBid";
import { useParams } from "react-router-dom";
import { useAppSelector } from "../../stores/hooks";
import * as signalR from "@microsoft/signalr";
import api from "../../services/api";
import { AxiosResponse } from "axios";
import { AuctionInfoDto } from "../../types/auctionInfo";
import Chat from "./Chat";
import { useTranslation } from "react-i18next";

export default function AuctionInfo() {
  const { t } = useTranslation();
  const { user } = useAppSelector((state) => state.user);
  const [currentBid, setCurrentBid] = useState<AuctionBidDto | null>(null);
  const [bidAmount, setBidAmount] = useState<number>(0);
  const [isConnected, setIsConnected] = useState<boolean>(false);
  const [auctionInfo, setAuctionInfo] = useState<AuctionInfoDto>();
  const connectionRef = useRef<signalR.HubConnection | null>(null);
  const auctionInfoId = useParams().auctionInfoId as string;
  const [currentImageIndex, setCurrentImageIndex] = useState(0);
  const userId = user?.id as string;

  useEffect(() => {
    const fetchAuctionInfo = async () => {
      try {
        const response = (await api.AuctionInfo.getAuctionInfoById(
          auctionInfoId
        )) as AxiosResponse;
        if (response.status === 200) {
          setAuctionInfo(response.data);
          setCurrentBid(response.data.maxBid);
        } else {
          console.error("Failed to fetch auction info:", response.statusText);
        }
      } catch (error) {
        console.error("Error fetching auction info:", error);
      }
    };

    fetchAuctionInfo();
  }, [auctionInfoId]);

  useEffect(() => {
    let isMounted = true;

    const connection = new signalR.HubConnectionBuilder()
      .withUrl((import.meta.env.VITE_HUB_URL as string) + "/hubs/auctionbid") // use your actual endpoint
      .withAutomaticReconnect()
      .build();

    connectionRef.current = connection; // 🔥 Store in ref

    const startConnection = async () => {
      try {
        await connection.start();
        if (!isMounted) return;

        setIsConnected(true);

        await connection.invoke("JoinAuctionGroup", auctionInfoId);

        connection.on("CurrentMaxBid", (bid: AuctionBidDto) => {
          setCurrentBid(bid);
        });

        connection.on("ReceiveBid", (bid: AuctionBidDto) => {
          setCurrentBid((prev) => {
            if (!prev || bid.bidAmount > prev.bidAmount) return bid;
            return prev;
          });
        });

        connection.on("BidRejected", (error: { message: string }) => {
          alert(error.message);
        });
      } catch (err) {
        if (isMounted) {
          console.error("Failed to start connection: ", err);
        }
      }
    };

    startConnection();

    return () => {
      isMounted = false;

      const cleanUp = async () => {
        if (connection.state === "Connected") {
          try {
            await connection.invoke("LeaveAuctionGroup", auctionInfoId);
          } catch (err) {
            console.error("Error leaving group: ", err);
          }
        }

        try {
          await connection.stop();
        } catch (err) {
          console.error("Error stopping connection: ", err);
        }
      };

      void cleanUp();
    };
  }, [auctionInfoId]);

  const handlePrev = () => {
    if (
      !auctionInfo?.car?.carImages ||
      auctionInfo?.car?.carImages.length === 0
    )
      return;
    setCurrentImageIndex((prev) =>
      prev === 0 ? auctionInfo?.car!.carImages.length - 1 : prev - 1
    );
  };

  const handleNext = () => {
    if (
      !auctionInfo?.car?.carImages ||
      auctionInfo?.car?.carImages.length === 0
    )
      return;
    setCurrentImageIndex((prev) =>
      prev === auctionInfo?.car!.carImages.length - 1 ? 0 : prev + 1
    );
  };
  const imageToDisplay =
    auctionInfo?.car?.carImages && auctionInfo?.car?.carImages.length > 0
      ? auctionInfo?.car?.carImages[currentImageIndex].imageUrl
      : "/assets/images/no-image-icon.png";

  // Submit bid when the connection is established
  const submitBid = async (price: number) => {
    const connection = connectionRef.current;

    if (!isConnected || !connection || connection.state !== "Connected") {
      alert("Connection not established yet.");
      return;
    }

    const bid: AuctionBidDto = {
      auctionInfoId,
      userId,
      bidAmount: price,
      bidDate: new Date(),
    };

    try {
      await connection.invoke("SubmitBid", bid);
      if (price == auctionInfo?.buyoutPrice) {
        alert(t("Congratulations! You bought the car!"));
      }
    } catch (err) {
      console.error("Error submitting bid: ", err);
    }
  };
  const now = new Date();
  const isAuctionActive =
    auctionInfo &&
    new Date(auctionInfo.startDate) <= now &&
    now <= new Date(auctionInfo.endDate);

  return (
    <div className="flex items-start justify-center min-h-screen bg-gray-100 py-10">
      <div className="p-6 w-250 rounded-lg shadow-md bg-white space-y-6">
        <h2 className="text-2xl font-bold text-gray-800 text-center">
          🚗 {t("Auction")}
        </h2>
        <div className="flex gap-8 items-start bg-white shadow-lg rounded-lg p-6">
          <div>
            {auctionInfo && (
              <div className="w-full max-w-screen-md mx-auto">
                <div className="relative h-60 w-96 overflow-hidden rounded-md shadow-lg">
                  <img
                    src={imageToDisplay}
                    alt="Car"
                    className="absolute top-0 left-0 w-full h-full object-cover rounded-md mb-2"
                  />
                  {auctionInfo?.car!.carImages.length > 1 && (
                    <>
                      <button
                        onClick={handlePrev}
                        className="absolute left-2 top-1/2 transform -translate-y-1/2 bg-white bg-opacity-80 hover:bg-opacity-100 px-3 py-1 rounded shadow"
                      >
                        {"<"}
                      </button>
                      <button
                        onClick={handleNext}
                        className="absolute right-2 top-1/2 transform -translate-y-1/2 bg-white bg-opacity-80 hover:bg-opacity-100 px-3 py-1 rounded shadow"
                      >
                        {">"}
                      </button>
                    </>
                  )}
                </div>

                <div className="bg-gray-100 p-4 rounded mt-4 space-y-2 text-lg">
                  <p>
                    <strong>{t("Start Price")}:</strong> $
                    {auctionInfo.startPrice}
                  </p>
                  <p>
                    <strong>{t("Buyout Price")}:</strong> $
                    {auctionInfo.buyoutPrice}
                  </p>
                  <p>
                    <strong>{t("Auction Ends")}:</strong>{" "}
                    {new Date(auctionInfo.endDate).toLocaleString()}
                  </p>
                  {auctionInfo.car && (
                    <p>
                      <strong>{t("Car")}:</strong>{" "}
                      {auctionInfo.car.carTopic.carName}{" "}
                      {auctionInfo.car.carTopic.carModel}
                    </p>
                  )}
                </div>
              </div>
            )}
          </div>
          <div>
            <h2 className="text-2xl font-semibold mb-2">
              {auctionInfo?.car?.carTopic.carName}{" "}
              {auctionInfo?.car?.carTopic.carModel} (
              {auctionInfo?.car?.carTopic.carYear})
            </h2>
            {auctionInfo?.car?.registrationPlate?.text ? (
              <p className="mb-2">
                <span className="font-semibold">{t("Plate Number")}:</span>{" "}
                {auctionInfo?.car?.registrationPlate.text}
                <span className="font-semibold ml-2">{t("Country")}:</span>{" "}
                {auctionInfo?.car?.registrationPlate.country}
              </p>
            ) : (
              <p>
                <a className="text-red-700">
                  {t("Registration plate information not provided.")}
                </a>
              </p>
            )}

            {auctionInfo?.car?.carDocuments ? (
              <p>
                <a
                  className="hover:underline text-blue-700"
                  href={auctionInfo?.car?.carDocuments.url}
                >
                  {t("Car documents")}
                </a>
              </p>
            ) : (
              <p>
                <a className="text-red-700">
                  {t("Car documents not uploaded.")}
                </a>
              </p>
            )}
            <p>
              <span className="font-semibold">{t("Owners description")}:</span>{" "}
              {auctionInfo?.car?.ownersDescription}
            </p>
            <p>
              <span className="font-semibold">{t("Car topic")}:</span>{" "}
              {auctionInfo?.car?.carTopic.description}
            </p>
          </div>
          <div>
            <p className="mb-2">
              <span className="font-semibold">{t("Owner")}:</span>{" "}
            </p>
            <img
              src={
                auctionInfo?.owner.avatarUrl ??
                "/assets/images/stock_avatar.jpg"
              }
              alt="User Avatar"
              className="w-16 h-16 rounded-full border-4 border-gray-500 transition-all duration-300 group-hover:brightness-50 cursor-pointer"
            />
            <h2 className="text-xl font-semibold mt-4">
              {user?.userName} {user?.userSurname}
            </h2>
            <p className="text-gray-600">@{user?.userNickname}</p>
            <p className="mt-2 text-gray-700">{user?.email}</p>
          </div>
        </div>
        <div className="space-y-2">
          <input
            type="number"
            value={bidAmount}
            onChange={(e) => setBidAmount(parseFloat(e.target.value))}
            placeholder="Enter your bid"
            className={`${
              isAuctionActive
                ? "border border-gray-300 p-2 rounded w-full"
                : "bg-gray-400 hidden"
            } text-black px-4 py-2 rounded w-full`}
            disabled={!isAuctionActive}
          />
          <button
            onClick={() => submitBid(bidAmount)}
            disabled={!isAuctionActive}
            className={`${
              isAuctionActive
                ? "bg-blue-600 hover:bg-blue-700"
                : "bg-gray-400 hidden"
            } text-white px-4 py-2 rounded w-full`}
          >
            {t("Submit Bid")}
          </button>
          <button
            onClick={() => {
              submitBid(auctionInfo?.buyoutPrice ?? 0);
            }}
            disabled={!isAuctionActive}
            className={`${
              isAuctionActive
                ? "bg-green-700 hover:bg-green-800"
                : "bg-gray-400 hidden"
            } text-white px-4 py-2 rounded w-full`}
          >
            {t("Buy out")}
          </button>
        </div>

        <div className="mt-4">
          <h3 className="text-xl font-semibold text-center">
            📜 {t("Bid History")}
          </h3>
          <div className="text-green-600 font-medium mt-2 text-center">
            💰 {t("Current highest bid")}: $
            {currentBid?.bidAmount ?? auctionInfo?.maxBid?.bidAmount ?? 0}
            {currentBid?.userId === userId && (
              <span className="text-blue-600">
                {" "}
                ({t("You")} {!isAuctionActive && t("won!")})
              </span>
            )}
          </div>
        </div>
      </div>
      <div>
        <Chat />
      </div>
    </div>
  );
}
