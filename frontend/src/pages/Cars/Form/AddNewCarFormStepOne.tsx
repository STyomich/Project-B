import { useState } from "react";
import { CarFormData } from "../../../types/car";
import { CarTopic } from "../../../types/carTopic";
import api from "../../../services/api";
import { AxiosResponse } from "axios";
import { useTranslation } from "react-i18next";

interface Props {
  data: CarFormData;
  updateData: (fields: Partial<CarFormData>) => void;
  next: () => void;
}

export default function AddNewCarFormStepOne({
  data,
  updateData,
  next,
}: Props) {
  const { t } = useTranslation();
  const [name, setName] = useState(data.carTopic.carName || "");
  const [model, setModel] = useState(data.carTopic.carModel || "");
  const [selectedCarTopic, setSelectedCarTopic] = useState<CarTopic | null>(
    data.carTopic || null
  );
  const [results, setResults] = useState<CarTopic[]>([]);
  const [loading, setLoading] = useState(false);

  const search = async () => {
    setLoading(true);
    const res = (await api.CarTopic.getCarTopics(name, model)) as AxiosResponse<
      CarTopic[]
    >;
    setSelectedCarTopic(null);
    setResults(res.data);
    setLoading(false);
  };

  return (
    <div className="fade-in">
      <h2 className="text-xl font-bold mb-4">{t("Step 1: Search Car Topic")}</h2>
      <div className="mb-2">
        <input
          type="text"
          placeholder={t("Car Name")}
          className="p-2 border border-gray-300 rounded"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
        <input
          type="text"
          placeholder={t("Car Model")}
          className="p-2 border border-gray-300 rounded"
          value={model}
          onChange={(e) => setModel(e.target.value)}
        />
        <button
          className="px-4 py-2 bg-gray-700 hover:bg-gray-800 text-white font-semibold p-2 rounded"
          onClick={search}
        >
          {t("Search")}
        </button>
      </div>

      {loading && <p>{t("Loading...")}</p>}

      <ul className="mt-4">
        {results.map((car) => (
          <li
            key={car.id}
            className={`flex p-2 border my-2 cursor-pointer ${
              data.carTopic.id === car.id ? "bg-blue-100" : ""
            }`}
            onClick={() => updateData({ carTopic: car })}
          >
            ({car.carYear}) {car.carName} - {car.carModel}
          </li>
        ))}
        {selectedCarTopic && selectedCarTopic.id !== "" && (
          <li
            key={selectedCarTopic.id}
            className={`flex p-2 border my-2 cursor-pointer ${
              data.carTopic.id === selectedCarTopic.id ? "bg-blue-100" : ""
            }`}
            onClick={() => updateData({ carTopic: selectedCarTopic })}
          >
            ({selectedCarTopic.carYear}) {selectedCarTopic.carName} -{" "}
            {selectedCarTopic.carModel}
          </li>
        )}
      </ul>

      <button
        className={`px-4 py-2 font-semibold rounded p-2 text-white ${
          !data.carTopic.id
            ? "bg-gray-400 cursor-not-allowed"
            : "bg-gray-700 hover:bg-gray-800"
        }
        `}
        disabled={!data.carTopic.id}
        onClick={next}
      >
        {t("Next")}
      </button>
    </div>
  );
}
