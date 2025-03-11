import { createBrowserRouter, RouteObject } from "react-router-dom";
import App from "../App";
import Home from "../pages/Home/Home";

export const routes:RouteObject[] =[{
    path: "/",
    Component: App,
    children: [
        {path: "/", Component: Home},
        {path: "/home", Component: Home},
    ]
}]

export const router = createBrowserRouter(routes)