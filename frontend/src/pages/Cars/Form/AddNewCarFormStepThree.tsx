import { useState } from "react";
import { CarFormData } from "../../../types/car";

interface Props {
    data: CarFormData;
    updateData: (fields: Partial<CarFormData>) => void;
    back: () => void;
    submit: () => void;
  }

export default function AddNewCarFormStepThree({
  data,
  updateData,
  back,
  submit,
}: Props) {
  const [desc, setDesc] = useState(data.ownersDescription || "");

  const handleSubmit = () => {
    updateData({ ownersDescription: desc });
    submit();
  };

  return (
    <div className="fade-in">
      <h2 className="text-xl font-bold mb-4">Step 3: Description</h2>
      <textarea
        className="w-full border p-2 rounded"
        rows={4}
        placeholder="Write a short description"
        value={desc}
        onChange={(e) => setDesc(e.target.value)}
      ></textarea>

      <div className="flex gap-4 mt-4">
        <button className="btn hover:underline" onClick={back}>
          Back
        </button>
        <button className={`px-4 py-2 font-semibold rounded p-2 text-white ${
          !desc
            ? "bg-gray-400 cursor-not-allowed"
            : "bg-gray-700 hover:bg-gray-800"
        }
        `} onClick={handleSubmit} disabled={!desc}>
          Add a New Car
        </button>
      </div>
    </div>
  );
}
