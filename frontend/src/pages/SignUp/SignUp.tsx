import { ErrorMessage, Field, Form, Formik } from "formik";
import * as Yup from "yup";
import { useAppDispatch } from "../../stores/hooks";
import { register } from "../../stores/features/user/userSlice";
import { useNavigate } from "react-router-dom";

const validationSchema = Yup.object({
  email: Yup.string()
    .email("Invalid email adress.")
    .required("Email is required."),
  userNickname: Yup.string().required("Nickname is required"),
  userName: Yup.string().required("Nickname is required"),
  userSurname: Yup.string().required("Nickname is required"),
  password: Yup.string()
    .min(8, "Password must be at least 8 characters")
    .matches(/[a-zA-Z]/, "Password can only contain letters")
    .matches(/\d/, "Password must contain a number")
    .required("Password is required"),
  confirmPassword: Yup.string()
    .oneOf([Yup.ref("password")], "Passwords must match")
    .required("Password is required"),
});

export default function SignUp() {
  const dispatch = useAppDispatch();
  const navigate = useNavigate();
  return (
    <div className="flex flex-col items-center bg-gray-100 p-4 min-h-screen fade-in">
      <h1 className="text-4xl font-bold">Sign Up</h1>
      <div className="bg-white p-6 shadow-lg rounded-lg mt-6">
        <h2 className="font-semibold text-2xl space-y-4">
          Enter your credentials to register you into system.
        </h2>
        <Formik
          initialValues={{
            email: "",
            userNickname: "",
            userName: "",
            userSurname: "",
            password: "",
            confirmPassword: "",
          }}
          validationSchema={validationSchema}
          onSubmit={async (values, { setSubmitting }) => {
            setSubmitting(true);
            await dispatch(register(values))
              .then(() => {
                navigate("/");
              })
              .finally(() => {
                setSubmitting(false);
              });
          }}
        >
          {({ isSubmitting }) => (
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
                <label>Nickname:</label>
                <Field
                  type="text"
                  placeholder="Nickname"
                  id="userNickname"
                  name="userNickname"
                  className="p-2 border border-gray-300 rounded"
                />
                <ErrorMessage
                  name="userNickname"
                  component="div"
                  className="text-red-500"
                />
              </div>
              <div className="flex flex-col space-y-2">
                <label>Username:</label>
                <Field
                  type="text"
                  placeholder="Username"
                  id="userName"
                  name="userName"
                  className="p-2 border border-gray-300 rounded"
                />
                <ErrorMessage
                  name="userName"
                  component="div"
                  className="text-red-500"
                />
              </div>
              <div className="flex flex-col space-y-2">
                <label>User surname:</label>
                <Field
                  type="text"
                  placeholder="User surname"
                  id="userSurname"
                  name="userSurname"
                  className="p-2 border border-gray-300 rounded"
                />
                <ErrorMessage
                  name="userName"
                  component="div"
                  className="text-red-500"
                />
              </div>
              <div className="flex flex-col space-y-2">
                <label>Password:</label>
                <Field
                  type="password"
                  placeholder="Passsword"
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
              <div className="flex flex-col space-y-2">
                <label>Confirm password:</label>
                <Field
                  type="password"
                  placeholder="Confirm password"
                  id="confirmPassword"
                  name="confirmPassword"
                  className="p-2 border border-gray-300 rounded"
                />
                <ErrorMessage
                  name="confirmPassword"
                  component="div"
                  className="text-red-500"
                />
              </div>

              <button
                type="submit"
                disabled={isSubmitting}
                className="bg-gray-700 hover:bg-gray-800 text-white font-semibold p-2 rounded"
              >
                Sign Up
              </button>
            </Form>
          )}
        </Formik>
      </div>
    </div>
  );
}
