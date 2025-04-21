import * as signalR from "@microsoft/signalr";

const connectionString =
  (import.meta.env.VITE_HUB_URL as string) + "/hubs/auctionbid";

const connection = new signalR.HubConnectionBuilder()
  .withUrl(connectionString, {
    withCredentials: true,
  })
  .withAutomaticReconnect([0, 2, 10, 30])
  .build();

export default connection;
