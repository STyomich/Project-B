import { useState } from "react";
import { CarFormData } from "../../../types/car";
import { useTranslation } from "react-i18next";

interface Props {
    data: CarFormData;
    updateData: (fields: Partial<CarFormData>) => void;
    next: () => void;
    back: () => void;
  }

export default function AddNewCarFormStepTwo({ data, updateData, next, back }: Props) {
  const {t} = useTranslation();
  const [country, setCountry] = useState(data.registrationCountry || "");
  const [text, setText] = useState(data.registrationText || "");

  const handleNext = () => {
    updateData({ registrationCountry: country, registrationText: text });
    next();
  };

  return (
    <div className="fade-in">
      <h2 className="text-xl font-bold mb-4">{t("Step 2: Registration Plate")}</h2>
      <div className="mb-2">
        <input
          type="text"
          placeholder={t("Country")}
          className="p-2 border border-gray-300 rounded"
          value={country}
          onChange={(e) => setCountry(e.target.value)}
        />
        <input
          type="text"
          placeholder={t("Plate Text")}
          className="p-2 border border-gray-300 rounded"
          value={text}
          onChange={(e) => setText(e.target.value)}
        />
      </div>

      <div className="flex gap-4 mt-4">
        <button className="btn hover:underline" onClick={back}>
          {t("Back")}
        </button>
        <button
          className={`px-4 py-2 font-semibold rounded p-2 text-white ${
            !country || !text
              ? "bg-gray-400 cursor-not-allowed"
              : "bg-gray-700 hover:bg-gray-800"
          }
          `}
          onClick={handleNext}
          disabled={!country || !text}
        >
          {t("Next")}
        </button>
      </div>
    </div>
  );
}
