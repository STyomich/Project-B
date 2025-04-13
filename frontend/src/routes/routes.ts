import { createBrowserRouter, RouteObject } from "react-router-dom";
import App from "../App";
import Home from "../pages/Home/Home";
import SignUp from "../pages/SignUp/SignUp";
import SignIn from "../pages/SignIn/SignIn";
import UserProfile from "../pages/UserProfile/UserProfile";
import AddNewCar from "../pages/Cars/AddNewCar";
import UserCarInfo from "../pages/Cars/UserCarInfo";
import UploadCarImages from "../pages/Cars/UploadCarImages";
import UploadCarDocuments from "../pages/Cars/UploadCarDocuments";

export const routes: RouteObject[] = [
  {
    path: "/",
    Component: App,
    children: [
      { path: "/", Component: Home },
      { path: "/home", Component: Home },
      { path: "/sign-up", Component: SignUp },
      { path: "/sign-in", Component: SignIn },
      { path: "/user-profile", Component: UserProfile },
      { path: "/add-new-car", Component: AddNewCar },
      { path: "/user-car/:carId", Component: UserCarInfo },
      { path: "/user-car/:carId/upload-images", Component: UploadCarImages },
      { path: "/user-car/:carId/upload-car-documents", Component: UploadCarDocuments },
    ],
  },
];

export const router = createBrowserRouter(routes);
