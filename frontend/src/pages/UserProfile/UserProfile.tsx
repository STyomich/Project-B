import { useAppSelector } from "../../stores/hooks";

export default function UserProfile() {
  const { user } = useAppSelector((state) => state.user);

  return (
    <div className="flex flex-col items-center p-6 bg-gray-100 min-h-screen fade-in">
      <div className="flex flex-row bg-white p-6 rounded-2xl shadow-lg w-full max-w-8/12">
        <div>
          <div className="relative inline-block group">
            <img
              src={
                user?.avatarUrl
                  ? user.avatarUrl
                  : "/assets/images/stock_avatar.jpg"
              }
              alt="User Avatar"
              className="w-32 h-32 rounded-full border-4 border-gray-500 transition-all duration-300 group-hover:brightness-50"
            />
            <span className="absolute inset-0 font-semibold flex items-center justify-center text-white text-lg opacity-0 group-hover:opacity-100 transition-opacity">
              Change
            </span>
          </div>

          <h2 className="text-xl font-semibold mt-4">
            {user?.userName} {user?.userSurname}
          </h2>
          <p className="text-gray-600">@{user?.userNickname}</p>
          <p className="mt-2 text-gray-700">{user?.email}</p>
          <p className="text-blue-400">Your documents</p>
          <div className="mt-4 flex justify-center space-x-4">
            <button className="px-4 py-2 bg-blue-500 text-white rounded-lg hover:bg-blue-600">
              Follow
            </button>
            <button className="px-4 py-2 border border-gray-300 rounded-lg hover:bg-gray-200">
              Message
            </button>
          </div>
        </div>
        <div className="ml-8 w-4/5">
          <div>
            <h1 className="text-3xl font-bold mb-6">Your cars:</h1>
          </div>
          <div>
            <h1 className="text-3xl font-bold mb-6">Your auction history:</h1>
          </div>
        </div>
      </div>
    </div>
  );
}
