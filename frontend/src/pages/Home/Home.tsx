import { useTranslation } from "react-i18next";
import { Link } from "react-router-dom";

export default function Home() {
  const { t } = useTranslation();
  return (
    <div className="flex flex-col items-center bg-gray-100 p-4 min-h-screen fade-in">
      <div className="w-4/5 bg-white p-6 shadow-lg rounded-lg mt-6">
        <div className="grid grid-cols-1 md:grid-cols-2 gap-6 items-center mb-6">
          <div>
            <img
              src="/assets/images/bmw-homepage-second.png"
              alt="Vehicle"
              className="w-full rounded-lg fade-in"
            />
          </div>
          <div className="flex flex-col justify-center">
            <p className="text-black text-7xl font-semibold">Project B</p>
            <p className="text-black text-6xl">{t("Auction Portal")}</p>
            <p className="text-black text-5xl">{t("Buy cars fast and easy")}</p>
            <div className="flex flex-row">
              <Link to="/auctions">
                <button className="mt-4 bg-gray-700 text-white px-4 py-2 rounded hover:bg-blue-600 transition duration-300 mr-4">
                  {t("Auctions")}
                </button>
              </Link>
              <Link to="/posts">
                <button className="mt-4 bg-gray-700 text-white px-4 py-2 rounded hover:bg-blue-600 transition duration-300">
                  {t("Blogs")}
                </button>
              </Link>
            </div>
          </div>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 gap-6 items-center mb-6">
          <div>
            <p className="text-black text-6xl font-semibold">
              {t("We offer platform - you offer wheels.")}
            </p>
            <p className="text-black text-3xl">
              {t("Our functionality provides fast search, filtering, and sorting options to help you find the perfect vehicle quickly.")}
            </p>
          </div>
          <div>
            <img
              src="/assets/images/mclaren-homepage.png"
              alt="ManyCars"
              className="w-full rounded-lg fade-in"
            />
          </div>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 gap-6 items-center">
          <img
            src="/assets/images/cars-homepage.png"
            alt="JapaneseCars"
            className="w-full rounded-lg fade-in"
          />
          <p className="text-black text-3xl">
            {t("Let's become part of the community and share your passion for cars.")}
          </p>
        </div>
      </div>
    </div>
  );
}
