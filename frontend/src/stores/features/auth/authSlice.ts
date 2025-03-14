import { createSlice, PayloadAction } from "@reduxjs/toolkit";

interface CommonState {
  token: string | null;
  loading: boolean;
}

const initialState: CommonState = {
  token: localStorage.getItem("jwt"),
  loading: false,
};

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    setToken(state, action: PayloadAction<string | null>) {
      state.token = action.payload;
      if (action.payload) {
        localStorage.setItem("jwt", action.payload);
      } else {
        localStorage.removeItem("jwt");
      }
    },
    setAppLoaded(state) {
      state.loading = true;
    },
  },
});

export const { setToken, setAppLoaded } = authSlice.actions;

export default authSlice.reducer;
