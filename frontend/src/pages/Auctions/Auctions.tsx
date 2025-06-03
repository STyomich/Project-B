import { useState } from "react";
import LiveAuctions from "./LiveAuctions";
import UpcomingAuctions from "./UpcomingAuctions";
import DeprecatedAuctions from "./DeprecatedAuctions";
import { useTranslation } from "react-i18next";

export default function Auctions() {
  const { t } = useTranslation();
  const [filterParams, setFilterParams] = useState<string>("Live");
  const [searchByNameParams, setSearchByNameParams] = useState<string>("");

  return (
    <div className="flex flex-col items-center justify-start min-h-screen bg-gray-50 p-6 ">
      <div className="flex flex-row items-center gap-6 ">
        <button
          onClick={() => setFilterParams("Live")}
          className={`px-4 py-2 rounded transition-colors duration-200 ${
            filterParams === "Live"
              ? "bg-gray-600 text-white"
              : "bg-gray-200 text-gray-700 hover:bg-gray-300"
          }`}
        >
          {t("In live")}
        </button>
        <button
          onClick={() => setFilterParams("Upcoming")}
          className={`px-4 py-2 rounded transition-colors duration-200 ${
            filterParams === "Upcoming"
              ? "bg-gray-600 text-white"
              : "bg-gray-200 text-gray-700 hover:bg-gray-300"
          }`}
        >
          {t("Upcoming")}
        </button>
        <button
          onClick={() => setFilterParams("Deprecated")}
          className={`px-4 py-2 rounded transition-colors duration-200 ${
            filterParams === "Deprecated"
              ? "bg-gray-600 text-white"
              : "bg-gray-200 text-gray-700 hover:bg-gray-300"
          }`}
        >
          {t("Deprecated")}
        </button>
      </div>
      <div>
        <input
          type="text"
          placeholder={t("Search by name")}
          value={searchByNameParams}
          onChange={(e) => setSearchByNameParams(e.target.value)}
          className="mt-4 p-2 border border-gray-300 rounded w-full max-w-md"
        />
      </div>
      <div className="flex flex-wrap gap-6 mt-6">
        {filterParams === "Live" && (
          <LiveAuctions searchByNameParams={searchByNameParams} />
        )}
        {filterParams === "Upcoming" && (
          <UpcomingAuctions searchByNameParams={searchByNameParams} />
        )}
        {filterParams === "Deprecated" && (
          <DeprecatedAuctions searchByNameParams={searchByNameParams} />
        )}
      </div>
    </div>
  );
}
