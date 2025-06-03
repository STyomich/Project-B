import { ErrorMessage, Field, Form, Formik } from "formik";
import * as Yup from "yup";
import { useAppDispatch } from "../../stores/hooks";
import { login } from "../../stores/features/user/userSlice";
import { useNavigate } from "react-router-dom";
import { useState } from "react";
import { useTranslation } from "react-i18next";

const validationSchema = Yup.object({
  email: Yup.string()
    .email("Invalid email address.")
    .required("Email is required."),
  password: Yup.string()
    .min(8, "Password must be at least 8 characters")
    .matches(/[a-zA-Z]/, "Password can only contain letters")
    .matches(/\d/, "Password must contain a number")
    .required("Password is required"),
});

export default function SignIn() {
  const {t} = useTranslation();
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  const [loginError, setLoginError] = useState("");

  return (
    <div className="flex flex-col items-center bg-gray-100 p-4 min-h-screen fade-in">
      <h1 className="text-4xl font-bold">{t("Sign In")}</h1>
      <div className="bg-white p-6 shadow-lg rounded-lg mt-6">
        <h2 className="font-semibold text-2xl space-y-4">
          {t("Enter your credentials to access your account.")}
        </h2>
        <Formik
          initialValues={{ email: "", password: "" }}
          validationSchema={validationSchema}
          onSubmit={async (values, { setSubmitting }) => {
            try {
              const response = await dispatch(login(values)).unwrap();
              if (response.status === 200) {
                navigate("/");
              } else {
                setLoginError(t("Invalid email or password."));
              }
            } catch {
              setLoginError(t("An error occurred. Invalid email or password."));
            } finally {
              setSubmitting(false);
            }
          }}
        >
          {({ isSubmitting }) => (
            <>
              <Form className="flex flex-col space-y-4 w-full">
                <div className="flex flex-col space-y-2">
                  <label>Email:</label>
                  <Field
                    type="text"
                    placeholder="Email"
                    id="email"
                    name="email"
                    className="p-2 border border-gray-300 rounded"
                  />
                  <ErrorMessage
                    name="email"
                    component="div"
                    className="text-red-500"
                  />
                </div>
                <div className="flex flex-col space-y-2">
                  <label>Password:</label>
                  <Field
                    type="password"
                    placeholder="Password"
                    id="password"
                    name="password"
                    className="p-2 border border-gray-300 rounded"
                  />
                  <ErrorMessage
                    name="password"
                    component="div"
                    className="text-red-500"
                  />
                </div>
                <div>
                  {loginError !== "" ? (
                    <label className="text-red-500">{loginError}</label>
                  ) : (
                    <></>
                  )}
                </div>
                <button
                  type="submit"
                  disabled={isSubmitting}
                  className="bg-gray-700 hover:bg-gray-800 text-white font-semibold p-2 rounded"
                >
                  {t("Sign In")}
                </button>
              </Form>
            </>
          )}
        </Formik>
      </div>
    </div>
  );
}
