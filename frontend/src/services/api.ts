import axios, { AxiosResponse } from "axios";
import store from "../stores";
import { CarFormData } from "../types/car";
import { AuctionInfoCreateRequest } from "../types/auctionInfo";

axios.defaults.baseURL = import.meta.env.VITE_API_URL as string;

const response = <T>(response: AxiosResponse<T>) => response;

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
  get: <T>(url: string) =>
    axios
      .get<T>(url)
      .then(response)
      .catch((error) => Promise.reject(error)),
  post: <T>(url: string, body: NonNullable<unknown>, config?: object) =>
    axios
      .post<T>(url, body, config)
      .then(response)
      .catch((error) => Promise.reject(error)),
  put: <T>(url: string, body: NonNullable<unknown>) =>
    axios
      .put<T>(url, body)
      .then(response)
      .catch((error) => Promise.reject(error)),
  delete: <T>(url: string) =>
    axios
      .delete<T>(url)
      .then(response)
      .catch((error) => Promise.reject(error)),
};

const User = {
  current: () => requests.get("/users/current"),
  login: (body: { email: string; password: string }) =>
    requests.post("/users/login", body),
  register: (body: { email: string; password: string }) =>
    requests.post("/users/register", body),
  updateAvatar: (formData: FormData) =>
    requests.post("/users/update-avatar", formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    }),
};

const Car = {
  getUsersCars: (nickname: string) =>
    requests.get(`/cars/users-cars/${nickname}`),
  createCarWithFormValues: (formData: CarFormData) =>
    requests.post("/cars/form-values", formData),
  getCarById: (id: string) => requests.get(`/cars/${id}`),
};
const CarImage = {
  uploadCarImage: (file: FormData, carId: string, isMain: boolean) =>
    requests.post(`/carimages?carId=${carId}&isMain=${isMain}`, file, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    }),
};
const CarTopic = {
  getCarTopics: (carName: string, carModel: string) =>
    requests.get(`/cartopics/list?carName=${carName}&carModel=${carModel}`),
  getCarTopicById: (id: string) => requests.get(`/cartopics/${id}`),
};
const CarDocuments = {
  uploadCarDocuments: (formData: FormData, carId: string) =>
    requests.post(`/cardocuments?carId=${carId}`, formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    }),
  getCarDocumentsByCarId: (carId: string) =>
    requests.get(`/cardocuments/${carId}`),
};
const AuctionInfo = {
  createAuctionInfo: (auctionInfoDto: AuctionInfoCreateRequest) =>
    requests.post("/auctioninfos", auctionInfoDto),
  getAuctionInfoList: () => requests.get("/auctioninfos"),
  getLiveAuctionInfoList: () =>
    requests.get("/auctioninfos/is-live"),
  getUpcomingAuctionInfoList: () =>
    requests.get("/auctioninfos/upcoming"),
  getDeprecatedAuctionInfoList: () =>
    requests.get("/auctioninfos/deprecated"),
  getAuctionInfoById: (id: string) =>
    requests.get(`/auctioninfos/whole-info/${id}`),
  getUsersAuctionInfo: () => requests.get(`/auctioninfos/users-auctions`),
};
const Posts = {
  getPosts: () => requests.get("/posts"),
  getPostById: (id: string) => requests.get(`/posts/${id}`),
  getPostsByUserId: (userId: string) => requests.get(`/posts/user/${userId}`),
  createPost: (body: { title: string; content: string }) =>
    requests.post("/posts", body),
  updatePost: (id: string, body: { title: string; content: string }) =>
    requests.put(`/posts/${id}`, body),
  deletePost: (id: string) => requests.delete(`/posts/${id}`),
  reactPost: (id: string, body: { postId: string }) => requests.post(`/posts/${id}/react`, body),
};
const Comments = {
  getCommentsByPostId: (postId: string) =>
    requests.get(`/comments/post/${postId}`),
  createComment: (postId: string, body: { postId: string, content: string }) =>
    requests.post(`/comments/${postId}`, body),
  deleteComment: (id: string) => requests.delete(`/comments/${id}`),
  reactComment: (id: string) => requests.delete(`/comments/${id}/react`),
}

const api = {
  User,
  Car,
  CarImage,
  CarTopic,
  CarDocuments,
  AuctionInfo,
  Posts,
  Comments,
};

export default api;
