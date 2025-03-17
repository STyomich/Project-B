import { createAsyncThunk, createSlice, PayloadAction } from "@reduxjs/toolkit";
import { setToken } from "../auth/authSlice";
import { User, UserLoginValues } from "../../../types/user";
import api from "../../../services/api";
import { AxiosResponse } from "axios";

interface UserState {
  user: User | null;
  loading: boolean;
}

const initialState: UserState = {
  user: null,
  loading: false,
};

const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    logout(state) {
      state.user = null;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(login.fulfilled, (state, action: PayloadAction<AxiosResponse>) => {
        state.user = action.payload.data;
      })
      .addCase(register.fulfilled, (state, action: PayloadAction<AxiosResponse>) => {
        state.user = action.payload.data;
      })
      .addCase(getUser.fulfilled, (state, action: PayloadAction<AxiosResponse>) => {
        state.user = action.payload.data;
      });
  },
});

export const login = createAsyncThunk(
  "user/login",
  async (values: UserLoginValues, { dispatch }) => {
    const response: AxiosResponse = (await api.User.login(values)) as AxiosResponse;
    if(response.status === 200)
    dispatch(setToken(response.data.token));
    return response;
  }
);

export const register = createAsyncThunk(
  "user/register",
  async (values: UserLoginValues, { dispatch }) => {
    const response: AxiosResponse = (await api.User.register(values)) as AxiosResponse;
    if(response.status === 200)
    dispatch(setToken(response.data.token));
    return response;
  }
);

export const logout = createAsyncThunk(
  "user/logout",
  async (_, { dispatch }) => {
    dispatch(setToken(null));
    dispatch(userSlice.actions.logout());
    return null;
  }
);

export const getUser = createAsyncThunk(
  "user/getUser",
  async () => {
    const response: AxiosResponse = await api.User.current();
    return response.data;
  }
);


export default userSlice.reducer;
