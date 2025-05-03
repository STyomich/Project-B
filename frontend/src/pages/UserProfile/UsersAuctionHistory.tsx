import { useEffect, useState } from "react";
import { AuctionInfoListItemDto } from "../../types/auctionInfo";
import api from "../../services/api";
import { AxiosResponse } from "axios";
import { Link } from "react-router-dom";

export default function UsersAuctionHistory() {
  const [auctionHistory, setAuctionHistory] = useState<
    AuctionInfoListItemDto[]
  >([]);

  useEffect(() => {
    const fetchAuctionHistory = async () => {
      try {
        const response =
          (await api.AuctionInfo.getUsersAuctionInfo()) as AxiosResponse;
        if (response.status !== 200) {
          throw new Error("Failed to fetch auction history");
        }
        setAuctionHistory(response.data);
      } catch (error) {
        console.error(error);
      }
    };

    fetchAuctionHistory();
  }, []);
  return (
    <div className="flex flex-col rounded-2xl bg-gray-150 p-4 fade-in">
      <div className="bg-white p-6 shadow-lg rounded-lg mt-6 max-w-11/12">
        {auctionHistory && auctionHistory.length > 0 ? (
          <div className="flex flex-wrap gap-4">
            {auctionHistory.map((auction: AuctionInfoListItemDto) => (
              <Link to={`/auctions/${auction.id}`} key={auction.id}>
                <div
                  key={auction.car!.id}
                  className="flex flex-col items-center bg-gray-100 p-4 rounded-lg w-60 transform transition-transform duration-500 hover:scale-105"
                >
                  {auction.car!.carMainImage ? (
                    <img
                      src={auction.car!.carMainImage.imageUrl}
                      alt={`${auction.car!.carMainImage.imageUrl} ${
                        auction.car!.carMainImage.imageUrl
                      }`}
                      className="w-full h-32 object-cover rounded-md mb-2"
                    />
                  ) : (
                    <img
                      src="/assets/images/no-image-icon.png"
                      alt={`${auction.car!.carMainImage} ${
                        auction.car!.carMainImage
                      }`}
                      className="w-full object-cover rounded-md mb-2"
                    />
                  )}
                  <h3 className="text-lg font-semibold">
                    ({auction.car!.carTopic.carYear}){" "}
                    {auction.car!.carTopic.carName}
                  </h3>
                  <p className="text-sm text-gray-600">
                    {auction.car!.carTopic.carModel}
                  </p>
                </div>
              </Link>
            ))}
          </div>
        ) : (
          <h2 className="text-center text-gray-500">
            User doesn't have any cars
          </h2>
        )}
      </div>
    </div>
  );
}
