import { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "../stores/hooks";
import { getUser, logout } from "../stores/features/user/userSlice";
import { Link, useNavigate } from "react-router-dom";

export default function ProfileButtonsGroup() {
  const dispatch = useAppDispatch();
  const { user } = useAppSelector((state) => state.user);
  const navigate = useNavigate();

  useEffect(() => {
    if (!user) {
      dispatch(getUser());
    }
  }, [dispatch, user]);

  function handleLogoutButton(){
    dispatch(logout());
    navigate("/", { replace: true });
  }

  return (
    <div className="flex space-x-4">
      {user ? (
        <>
          <Link to="/user-profile">
            <button className="flex">
              {user.avatar ? (
                <img src={user.avatar} className="w-10 h-10 rounded-full" />
              ) : (
                <img
                  src="/assets/images/stock_avatar.jpg"
                  className="w-10 h-10 rounded-full"
                />
              )}

              <label className="text-white font-semibold hover:text-gray-400 p-2 rounded">
                {user.userNickname}
              </label>
            </button>
          </Link>
          <button
            onClick={() => handleLogoutButton()}
            className="text-white font-semibold hover:text-gray-400 p-2 rounded"
          >
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
