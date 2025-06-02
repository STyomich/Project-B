import React, { useEffect, useState } from "react";
import * as signalR from "@microsoft/signalr";
import { useAppSelector } from "../../stores/hooks";

interface ChatMessage {
  nickname: string;
  content: string;
}

const Chat: React.FC = () => {
  const { user } = useAppSelector((state) => state.user);
  const [connection, setConnection] = useState<signalR.HubConnection | null>(
    null
  );
  const [messages, setMessages] = useState<ChatMessage[]>([]);
  const [nickname, setNickname] = useState<string>();
  const [message, setMessage] = useState<string>("");

  useEffect(() => {
    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:5000/hubs/chatHub")
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);
  }, []);
  useEffect(() => {
    if (user) {
      setNickname(user.userNickname);
    }
  }, [user]);

  useEffect(() => {
    if (connection) {
      connection
        .start()
        .then(() => {
          console.log("Connected to SignalR hub");

          connection.on(
            "ReceiveMessage",
            (nickname: string, content: string) => {
              setMessages((prev) => [...prev, { nickname, content }]);
            }
          );
        })
        .catch((error) => console.error("Connection failed:", error));
    }
  }, [connection]);

  const sendMessage = async () => {
    if (connection && message.trim() !== "") {
      await connection.invoke("SendMessage", nickname, message);
      setMessage("");
    }
  };

  return (
    <div style={{ padding: 20, maxWidth: 600, margin: "0 auto" }}>
      <h2 className="text-3xl">Chat Room</h2>
      <div style={{ marginBottom: 10 }}>
        <strong>Nickname:</strong>{" "}
        <input value={nickname} onChange={(e) => setNickname(e.target.value)} />
      </div>
      <div className="flex items-center gap-2">
        <input
          type="text"
          placeholder="Type a message..."
          value={message}
          onChange={(e) => setMessage(e.target.value)}
          onKeyDown={(e) => e.key === "Enter" && sendMessage()}
          style={{ width: "80%" }}
        />
        <button
          onClick={sendMessage}
          className="px-4 py-2 bg-green-400 hover:underline text-white font-semibold p-2 mt-3 rounded"
          style={{ marginLeft: 10 }}
        >
          Send
        </button>
      </div>
      <div
        style={{
          maxHeight: 300,
          overflowY: "auto",
          border: "1px solid #ccc",
          padding: 10,
          marginBottom: 10,
        }}
      >
        {messages.map((msg, index) => (
          <div key={index}>
            <strong>{msg.nickname}:</strong> {msg.content}
          </div>
        ))}
      </div>
    </div>
  );
};

export default Chat;
