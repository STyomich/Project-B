import { Link } from "react-router-dom";
import Logo from "./Logo";
import ProfileButtonsGroup from "./ProfileButtonsGroup";
import LanguageSelector from "./LanguageSelector";
import { useTranslation } from "react-i18next";

export default function Header() {
  const { t } = useTranslation();
  return (
    <>
      <header className="w-full bg-gradient-to-r from-black via-gray-700 to-black p-4">
        <div className="max-w-screen-xl mx-auto flex justify-between items-center">
          {/* Left side buttons (Functionality) */}
          <div className="flex space-x-4">
            <Link to="/home">
              <button className="text-white font-semibold hover:text-gray-400 p-2 rounded">
                {t("Home")}
              </button>
            </Link>
            <Link to="/auctions">
              <button className="text-white font-semibold hover:text-gray-400 p-2 rounded">
                {t("Auctions")}
              </button>
            </Link>
            <Link to="/posts">
              <button className="text-white font-semibold hover:text-gray-400 p-2 rounded">
                {t("Blogs")}
              </button>
            </Link>
          </div>

          {/* Logo */}
          <Logo />

          {/* Right side buttons (Profile) */}
          <div className="flex space-x-4 items-center">
            <LanguageSelector />
            <ProfileButtonsGroup />
          </div>
        </div>
      </header>

      {/* Divider */}
      <div className="w-full bg-gray-400 h-1"></div>
    </>
  );
}
