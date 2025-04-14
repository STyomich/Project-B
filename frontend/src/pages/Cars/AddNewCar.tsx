import { useState } from "react";
import { CarFormData } from "../../types/car";
import AddNewCarFormStepOne from "./Form/AddNewCarFormStepOne";
import AddNewCarFormStepTwo from "./Form/AddNewCarFormStepTwo";
import AddNewCarFormStepThree from "./Form/AddNewCarFormStepThree";
import api from "../../services/api";
import { AxiosResponse } from "axios";
import { useNavigate } from "react-router-dom";

export default function AddNewCar() {
  const navigate = useNavigate();
  const [step, setStep] = useState(1);
  const [formData, setFormData] = useState<CarFormData>({
    carTopic: {
      id: "",
      carName: "",
      carModel: "",
      carYear: 0,
      carDescription: "",
      imageLogoUrl: "",
    },
    registrationCountry: "",
    registrationText: "",
    ownersDescription: "",
  });

  const updateData = (fields: Partial<CarFormData>) => {
    setFormData((prev) => ({ ...prev, ...fields }));
  };

  const next = () => setStep((prev) => prev + 1);
  const back = () => setStep((prev) => prev - 1);

  const submit = async () => {
    const response = (await api.Car.createCarWithFormValues(
      formData
    )) as AxiosResponse;

    if (response.status !== 200) {
      alert("Error adding car");
      return;
    }
    navigate("/user-profile");
  };

  return (
    <div className="max-w-xl mx-auto mt-10 p-4 border rounded shadow">
      {step === 1 && (
        <AddNewCarFormStepOne
          data={formData}
          updateData={updateData}
          next={next}
        />
      )}
      {step === 2 && (
        <AddNewCarFormStepTwo
          data={formData}
          updateData={updateData}
          next={next}
          back={back}
        />
      )}
      {step === 3 && (
        <AddNewCarFormStepThree
          data={formData}
          updateData={updateData}
          back={back}
          submit={() => {
            submit();
          }}
        />
      )}
    </div>
  );
}
