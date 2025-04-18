import { Link } from "react-router-dom";
import Logo from "./Logo";
import ProfileButtonsGroup from "./ProfileButtonsGroup";

export default function Header() {
  return (
    <>
      <header className="w-full bg-gradient-to-r from-black via-gray-700 to-black p-4">
        <div className="max-w-screen-xl mx-auto flex justify-between items-center">
          {/* Left side buttons (Functionality) */}
          <div className="flex space-x-4">
            <Link to="/home">
              <button className="text-white font-semibold hover:text-gray-400 p-2 rounded">
                Home
              </button>
            </Link>
            <Link to="/auctions">
              <button className="text-white font-semibold hover:text-gray-400 p-2 rounded">
                Auctions
              </button>
            </Link>
            <button className="text-white font-semibold hover:text-gray-400 p-2 rounded">
              Shop
            </button>
          </div>

          {/* Logo */}
          <Logo />

          {/* Right side buttons (Profile) */}
          <ProfileButtonsGroup />
        </div>
      </header>

      {/* Divider */}
      <div className="w-full bg-gray-400 h-1"></div>
    </>
  );
}
