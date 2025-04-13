import { useEffect } from "react";
import { getUsersCars } from "../../stores/features/user/userSlice";
import { useAppDispatch, useAppSelector } from "../../stores/hooks";
import { CarListItemDto } from "../../types/car";
import { Link } from "react-router-dom";

interface UserCarsProps {
  nickname: string;
}

export default function UserCars({ nickname }: UserCarsProps) {
  const { carList } = useAppSelector((state) => state.user);
  const dispatch = useAppDispatch();

  useEffect(() => {
    dispatch(getUsersCars(nickname));
  }, [nickname, dispatch]);

  return (
    <div className="flex flex-col items-center rounded-2xl bg-gray-150 p-4 fade-in">
      <div className="bg-white p-6 shadow-lg rounded-lg mt-6 w-full">
        {carList && carList.length > 0 ? (
          <div className="flex flex-wrap gap-4">
            {carList.map((car: CarListItemDto) => (
              <Link key={car.id} to={`/user-car/${car.id}`}>
                <div
                  key={car.id}
                  className="flex flex-col items-center bg-gray-100 p-4 rounded-lg w-60 transform transition-transform duration-500 hover:scale-105"
                >
                  {car.carMainImage ? (
                    <img
                      src={car.carMainImage.imageUrl}
                      alt={`${car.carMainImage.imageUrl} ${car.carMainImage.imageUrl}`}
                      className="w-full h-32 object-cover rounded-md mb-2"
                    />
                  ) : (
                    <img
                      src="/assets/images/no-image-icon.png"
                      alt={`${car.carMainImage} ${car.carMainImage}`}
                      className="w-full object-cover rounded-md mb-2"
                    />
                  )}
                  <h3 className="text-lg font-semibold">
                    ({car.carTopic.carYear}) {car.carTopic.carName}
                  </h3>
                  <p className="text-sm text-gray-600">
                    {car.carTopic.carModel}
                  </p>
                </div>
              </Link>
            ))}
          </div>
        ) : (
          <h2 className="text-center text-gray-500">
            User doesn't have any cars
          </h2>
        )}
      </div>
    </div>
  );
}
