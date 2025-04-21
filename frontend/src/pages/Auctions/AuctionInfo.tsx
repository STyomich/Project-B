import { useEffect, useRef, useState } from "react";
import { AuctionBidDto } from "../../types/auctionBid";
import { useParams } from "react-router-dom";
import { useAppSelector } from "../../stores/hooks";
import * as signalR from "@microsoft/signalr";

export default function AuctionInfo() {
  const { user } = useAppSelector((state) => state.user);
  const [currentBid, setCurrentBid] = useState<AuctionBidDto | null>(null);
  const [bidAmount, setBidAmount] = useState<number>(0);
  const [isConnected, setIsConnected] = useState(false);
  const connectionRef = useRef<signalR.HubConnection | null>(null);
  const auctionInfoId = useParams().auctionInfoId as string;
  const userId = user?.id as string;

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

  // Submit bid when the connection is established
  const submitBid = async () => {
    const connection = connectionRef.current;

    if (!isConnected || !connection || connection.state !== "Connected") {
      alert("Connection not established yet.");
      return;
    }

    const bid: AuctionBidDto = {
      auctionInfoId,
      userId,
      bidAmount,
      bidDate: new Date(),
    };

    try {
      await connection.invoke("SubmitBid", bid);
    } catch (err) {
      console.error("Error submitting bid: ", err);
    }
  };

  return (
    <div>
      <h2>Live Auction</h2>
      <input
        type="number"
        value={bidAmount}
        onChange={(e) => setBidAmount(parseFloat(e.target.value))}
        placeholder="Enter bid amount"
      />
      <button onClick={submitBid}>Submit Bid</button>

      <h3>Bid History</h3>
      <ul>
        {currentBid ? (
          <div>💰 Current highest bid: ${currentBid.bidAmount}</div>
        ) : (
          <div>-</div>
        )}
      </ul>
    </div>
  );
}
