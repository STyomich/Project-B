import { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "../stores/hooks";
import { getUser } from "../stores/features/user/userSlice";
import { Link } from "react-router-dom";

export default function ProfileButtonsGroup() {
  const dispatch = useAppDispatch();
  const { user } = useAppSelector((state) => state.user);

  useEffect(() => {
    if (user) {
      dispatch(getUser());
    }
  }, [dispatch, user]);

  return (
    <div className="flex space-x-4">
      {user ? (
        <>
          <img src={user.avatarUrl} className="w-10 h-10 rounded-full" />
          <button className="text-white font-semibold hover:text-gray-400 p-2 rounded">
            {user.userNickname}
          </button>
          <button className="text-white font-semibold hover:text-gray-400 p-2 rounded">
            Logout
          </button>
        </>
      ) : (
        <>
          <Link to="/sign-in">
            <button className="text-white font-semibold hover:text-gray-400 p-2 rounded">
              Sign In
            </button>
          </Link>
          <p> | </p>
          <Link to="/sign-up">
            <button className="text-white font-semibold hover:text-gray-400 p-2 rounded">
              Sign Up
            </button>
          </Link>
        </>
      )}
    </div>
  );
}
