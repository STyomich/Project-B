import { useEffect, useState } from "react";
import { Car } from "../../types/car";
import api from "../../services/api";
import { AxiosResponse } from "axios";
import { useParams } from "react-router-dom";
import { Link } from "react-router-dom";

export default function UserCarInfo() {
  const [car, setCar] = useState<Car | null>(null);
  const [currentImageIndex, setCurrentImageIndex] = useState(0);
  const carId = useParams().carId as string;
  useEffect(() => {
    const fetchCar = async () => {
      try {
        const response = (await api.Car.getCarById(carId)) as AxiosResponse;
        setCar(response.data);
      } catch (error) {
        console.error("Error fetching car:", error);
      }
    };
    fetchCar();
  }, [carId]);

  const handlePrev = () => {
    if (!car?.carImages || car.carImages.length === 0) return;
    setCurrentImageIndex((prev) =>
      prev === 0 ? car.carImages.length - 1 : prev - 1
    );
  };

  const handleNext = () => {
    if (!car?.carImages || car.carImages.length === 0) return;
    setCurrentImageIndex((prev) =>
      prev === car.carImages.length - 1 ? 0 : prev + 1
    );
  };

  if (!car) return <div>Loading...</div>;

  const imageToDisplay =
    car.carImages && car.carImages.length > 0
      ? car.carImages[currentImageIndex].imageUrl
      : "/assets/images/no-image-icon.png";

  return (
    <div className="flex flex-col items-center p-6 bg-gray-100 min-h-screen fade-in">
      <div className="flex gap-8 items-start bg-white shadow-lg rounded-lg p-6">
        {/* Left side: Image and navigation */}
        <div className="flex flex-col items-start">
          <div className="relative w-80 h-80 overflow-hidden rounded-md shadow-lg">
            <img
              src={imageToDisplay}
              alt="Car"
              className="w-full object-cover rounded-md mb-2"
            />
            {car.carImages && car.carImages.length > 1 && (
              <>
                <button
                  onClick={handlePrev}
                  className="absolute left-2 top-1/2 transform -translate-y-1/2 bg-white bg-opacity-80 hover:bg-opacity-100 px-3 py-1 rounded shadow"
                >
                  {"<"}
                </button>
                <button
                  onClick={handleNext}
                  className="absolute right-2 top-1/2 transform -translate-y-1/2 bg-white bg-opacity-80 hover:bg-opacity-100 px-3 py-1 rounded shadow"
                >
                  {">"}
                </button>
              </>
            )}
          </div>
          <Link to={`/user-car/${carId}/upload-images`}>
            <button className="px-4 py-2 bg-white hover:underline text-black font-semibold p-2 mt-3 rounded">
              Upload Images
            </button>
          </Link>
        </div>

        {/* Right side: Car Info */}
        <div className="max-w-md">
          <h2 className="text-2xl font-semibold mb-2">
            {car.carTopic.carName} {car.carTopic.carModel} (
            {car.carTopic.carYear})
          </h2>
          {car.registrationPlate?.text ? (
            <p className="mb-2">
              <span className="font-semibold">Plate Number:</span>{" "}
              {car.registrationPlate.text}
            </p>
          ) : (
            <p>
              <a className="text-red-700">
                Registration plate information not provided.
              </a>
            </p>
          )}

          {car.carDocument ? (
            <p>
              <a href={car.carDocument.url}>Car documents</a>
            </p>
          ) : (
            <p>
              <a className="text-red-700">Car documents not uploaded.</a>
            </p>
          )}
          <p>
            <span className="font-semibold">Description:</span>{" "}
            {car.ownersDescription}
          </p>
        </div>
      </div>
    </div>
  );
}
