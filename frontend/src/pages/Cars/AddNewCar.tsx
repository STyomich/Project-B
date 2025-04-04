import { useState } from "react";
import { CarTopic } from "../../types/carTopic";
import { RegistrationPlate } from "../../types/registrationPlate";


// Dummy mock data for example
const mockCarTopics: CarTopic[] = [
    { id: 1, brand: "Toyota", model: "Camry" },
    { id: 2, brand: "Honda", model: "Civic" },
    { id: 3, brand: "Ford", model: "Focus" },
  ];
  
  export default function AddNewCar() {
    const [step, setStep] = useState(1);
    const [carTopic, setCarTopic] = useState<CarTopic>();
    const [registrationPlate, setRegistrationPlate] = useState<RegistrationPlate>();
  
    function SelectCarTopic() {
      const [brand, setBrand] = useState("");
      const [model, setModel] = useState("");
      const [searchResults, setSearchResults] = useState<CarTopic[]>([]);
      const [selectedId, setSelectedId] = useState<number | null>(null);
  
      const handleSearch = () => {
        const filtered = mockCarTopics.filter(
          topic =>
            topic.brand.toLowerCase().includes(brand.toLowerCase()) &&
            topic.model.toLowerCase().includes(model.toLowerCase())
        );
        setSearchResults(filtered);
      };
  
      const handleNext = () => {
        const selected = searchResults.find(ct => ct.id === selectedId);
        if (selected) {
          setCarTopic(selected);
          setStep(2);
        }
      };
  
      return (
        <div className="space-y-4 mt-4">
          <div className="flex gap-2">
            <input
              type="text"
              placeholder="Brand"
              value={brand}
              onChange={e => setBrand(e.target.value)}
              className="border p-2 rounded"
            />
            <input
              type="text"
              placeholder="Model"
              value={model}
              onChange={e => setModel(e.target.value)}
              className="border p-2 rounded"
            />
            <button
              onClick={handleSearch}
              className="bg-blue-500 text-white px-4 py-2 rounded"
            >
              Search
            </button>
          </div>
  
          {searchResults.length > 0 && (
            <div className="space-y-2">
              {searchResults.map(topic => (
                <div
                  key={topic.id}
                  className={`p-3 border rounded cursor-pointer ${
                    selectedId === topic.id ? "bg-blue-100" : ""
                  }`}
                  onClick={() => setSelectedId(topic.id)}
                >
                  {topic.brand} {topic.model}
                </div>
              ))}
              <button
                onClick={handleNext}
                disabled={selectedId === null}
                className="bg-green-500 text-white px-4 py-2 rounded mt-2"
              >
                Next
              </button>
            </div>
          )}
        </div>
      );
    }
  
    return (
      <div className="flex flex-col items-center bg-gray-100 p-4 min-h-screen fade-in">
        <h1 className="text-4xl font-bold">Add new car to system</h1>
        <div className="bg-white p-6 shadow-lg rounded-lg mt-6 w-full max-w-xl">
          <h2 className="font-semibold text-2xl mb-4">
            Enter car's information to add it to the system.
          </h2>
  
          {step === 1 && <SelectCarTopic />}
          {step === 2 && (
            <div>
              <p>Selected Car Topic:</p>
              <p className="font-semibold">
                {carTopic?.brand} {carTopic?.model}
              </p>
              {/* Here goes the next step form */}
            </div>
          )}
        </div>
      </div>
    );
  }