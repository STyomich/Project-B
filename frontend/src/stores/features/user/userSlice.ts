import { createAsyncThunk, createSlice, PayloadAction } from "@reduxjs/toolkit";
import { setToken } from "../auth/authSlice";
import { User, UserLoginValues } from "../../../types/user";
import api from "../../../services/api";

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
      .addCase(login.fulfilled, (state, action: PayloadAction<User>) => {
        state.user = action.payload;
      })
      .addCase(register.fulfilled, (state, action: PayloadAction<User>) => {
        state.user = action.payload;
      })
      .addCase(getUser.fulfilled, (state, action: PayloadAction<User>) => {
        state.user = action.payload;
      });
  },
});

export const login = createAsyncThunk(
  "user/login",
  async (values: UserLoginValues, { dispatch }) => {
    const user: User = (await api.User.login(values)) as User;
    dispatch(setToken(user.token));
    return user;
  }
);

export const register = createAsyncThunk(
  "user/register",
  async (values: UserLoginValues, { dispatch }) => {
    const user: User = (await api.User.register(values)) as User;
    dispatch(setToken(user.token));
    return user;
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

export const getUser = createAsyncThunk<User, void>(
  "user/getUser",
  async (): Promise<User> => {
    return (await api.User.current()) as User;
  }
);

export default userSlice.reducer;
