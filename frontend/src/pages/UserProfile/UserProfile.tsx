import { useRef, useState } from "react";
import { useAppDispatch, useAppSelector } from "../../stores/hooks";
import { updateAvatar } from "../../stores/features/user/userSlice";
import { User } from "../../types/user";
import UserCars from "./UserCars";
import UsersAuctionHistory from "./UsersAuctionHistory";
import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";

export default function UserProfile() {
  const {t} = useTranslation();
  const { user } = useAppSelector((state) => state.user);
  const dispatch = useAppDispatch();

  const [selectedFile, setSelectedFile] = useState<File | null>(null);
  const [preview, setPreview] = useState<string | null>(null);

  const fileInputRef = useRef<HTMLInputElement | null>(null);

  const handleAvatarClick = () => {
    fileInputRef.current?.click();
  };

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0];
    if (file) {
      setSelectedFile(file);
      setPreview(URL.createObjectURL(file));
    }
  };

  const handleUpload = async () => {
    if (!selectedFile) return;
    const formData = new FormData();
    formData.append("file", selectedFile);

    try {
      const response = (await dispatch(updateAvatar(formData)).unwrap()) as {
        data: User;
        status: number;
      };
      if (response.status === 200) {
        alert(t("Avatar uploaded successfully."));
      }
    } catch (error) {
      console.error(error);
      alert(t("Error uploading avatar."));
    }
  };

  return (
    <div className="flex flex-col items-center p-6 bg-gray-100 min-h-screen fade-in">
      <div className="flex flex-row bg-white p-6 rounded-2xl shadow-lg w-full max-w-10/12">
        {/* Left side: Avatar and Info */}
        <div>
          <button className="relative inline-block group" onClick={handleAvatarClick}>
            <img
              src={preview ?? user?.avatar ?? "/assets/images/stock_avatar.jpg"}
              alt="User Avatar"
              className="w-32 h-32 rounded-full border-4 border-gray-500 transition-all duration-300 group-hover:brightness-50 cursor-pointer"
            />
            <span className="absolute inset-0 font-semibold flex items-center justify-center text-white text-lg opacity-0 group-hover:opacity-100 transition-opacity">
              {t("Change")}
            </span>
          </button>

          <input
            type="file"
            accept="image/*"
            ref={fileInputRef}
            className="hidden"
            onChange={handleFileChange}
          />

          {selectedFile && (
            <button
              onClick={handleUpload}
              className="mt-2 bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
            >
              {t("Upload Avatar")}
            </button>
          )}

          <h2 className="text-xl font-semibold mt-4">
            {user?.userName} {user?.userSurname}
          </h2>
          <p className="text-gray-600">@{user?.userNickname}</p>
          <p className="mt-2 text-gray-700">{user?.email}</p>
        </div>

        {/* Right side: Cars, Auctions, Posts */}
        <div className="ml-8 w-4/5 space-y-10">
          {/* Cars */}
          <div>
            <div className="flex items-center justify-between mb-6">
              <h1 className="text-3xl font-bold">{t("Your cars:")}</h1>
              <Link to="/add-new-car">
                <button className="px-4 py-2 bg-gray-700 hover:bg-gray-800 text-white font-semibold p-2 rounded">
                  {t("Add a new car")}
                </button>
              </Link>
            </div>
            {user && <UserCars nickname={user.userNickname} />}
          </div>

          {/* Auction History */}
          <div>
            <h1 className="text-3xl font-bold mb-6">{t("Your auction history:")}</h1>
            <UsersAuctionHistory />
          </div>
          <div>
          </div>
        </div>
      </div>
    </div>
  );
}
