import axios, { AxiosResponse } from "axios";
import store from "../stores";

axios.defaults.baseURL = import.meta.env.VITE_API_URL as string;

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

axios.interceptors.request.use(
  (config) => {
    const state = store.getState();
    const token = state.auth.token;
    if (token && config.headers) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

const requests = {
  get: <T>(url: string) => axios.get<T>(url).then(responseBody),
  post: <T>(url: string, body: NonNullable<unknown>) =>
    axios.post<T>(url, body).then(responseBody),
  put: <T>(url: string, body: NonNullable<unknown>) =>
    axios.put<T>(url, body).then(responseBody),
  delete: <T>(url: string) => axios.delete<T>(url).then(responseBody),
};

const User = {
  current: () => requests.get("/users/current"),
  login: (body: { email: string; password: string }) =>
    requests.post("/users/login", body),
  register: (body: { email: string; password: string }) =>
    requests.post("/users/register", body),
};

const api = {
    User,
};

export default api;
