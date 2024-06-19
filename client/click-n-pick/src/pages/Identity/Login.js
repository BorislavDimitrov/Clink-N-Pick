import { useState, useRef } from "react";
import { useNavigate } from "react-router-dom";
import Tippy from "@tippyjs/react";
import "react-tippy/dist/tippy.css";

import { isEmailValid, isPasswordValid } from "../../Utility/validations";
import Logo from "../../img/logo.jpg";
import { login } from "../../fetch/requests/identity";
import { SetAuthToken } from "../../Utility/auth";
import Modal from "../../components/Modal";
import { SetUserImageUrl } from "../../Utility/user";

function Login() {
  const navigate = useNavigate();
  const modal = useRef();

  const [responseResult, setResponseResult] = useState(null);

  const [enteredValues, setEnteredValues] = useState({
    email: "",
    password: "",
  });

  const [didEdit, setDidEdit] = useState({
    email: false,
    password: false,
  });

  const [showPassword, setShowPassword] = useState(false);

  const emailIsInvalid = didEdit.email && !isEmailValid(enteredValues.email);

  const passwordIsInvalid =
    didEdit.password && !isPasswordValid(enteredValues.password);

  async function handleSubmit(event) {
    event.preventDefault();

    try {
      const response = await login(enteredValues);

      if (response.status !== 200) {
        throw new Error("Network response was not ok");
      }

      var data = await response.json();
      SetUserImageUrl(data.userImageUrl);
      SetAuthToken(data.token);

      window.location.href = "/";
    } catch (error) {
      setResponseResult("bad");
      modal.current.open();
    }
  }

  function handleInputChange(identifier, value) {
    setEnteredValues((prevValues) => ({
      ...prevValues,
      [identifier]: value,
    }));
    setDidEdit((prevEdit) => ({
      ...prevEdit,
      [identifier]: false,
    }));
  }

  function handleInputBlur(identifier) {
    setDidEdit((prevEdit) => ({
      ...prevEdit,
      [identifier]: true,
    }));
  }

  function togglePasswordVisibility() {
    setShowPassword((prevState) => !prevState);
  }

  function redirectTo() {
    navigate("/login");
  }

  return (
    <>
      <Modal
        ref={modal}
        performAction={responseResult === "ok" ? redirectTo : ""}
        buttonCaption="Okay"
      >
        {responseResult === "bad" && (
          <>
            <h2 className="text-xl font-bold text-red-700 my-4">
              Sign up Failed!
            </h2>
            <p className="text-stone-600 mb-4">
              Please check the information you provide and try again.
            </p>
          </>
        )}
      </Modal>
      <div className="flex min-h-full flex-col justify-center px-6 py-12 lg:px-8">
        <div className="sm:mx-auto sm:w-full sm:max-w-sm mb-10">
          <img className="mx-auto " src={Logo} alt="Your Company" />
          <h2 className="mt-1 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900">
            Sign in to your account
          </h2>
        </div>

        <div className="mt-1 sm:mx-auto sm:w-full sm:max-w-sm shadow-md border rounded-lg p-8">
          <form className="space-y-6">
            <div>
              <label className="block text-sm font-medium leading-6 text-gray-900">
                Email address
              </label>
              <div className="mt-2">
                <input
                  id="email"
                  name="email"
                  type="email"
                  required
                  onBlur={() => handleInputBlur("email")}
                  onChange={(event) =>
                    handleInputChange("email", event.target.value)
                  }
                  value={enteredValues.email}
                  className="px-4 block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                />

                <div className="text-red-500">
                  {emailIsInvalid && <p>Please enter a valid email address.</p>}
                </div>
              </div>
            </div>

            <div>
              <div className="flex items-center justify-between">
                <label className="block text-sm font-medium leading-6 text-gray-900">
                  Password
                </label>

                <div className="text-sm">
                  <a
                    href="/Identity/ForgotPassword"
                    className="font-semibold text-indigo-600 hover:text-indigo-500"
                  >
                    Forgot password?
                  </a>
                </div>
              </div>
              <div className="relative w-full">
                <div className="absolute inset-y-0 right-0 flex items-center px-2">
                  <input className="hidden " />
                  <Tippy content="Password visibility">
                    <span
                      className="cursor-pointer"
                      onClick={togglePasswordVisibility}
                    >
                      &#128274;
                    </span>
                  </Tippy>
                </div>

                <input
                  id="password"
                  name="password"
                  type={showPassword === true ? "text" : "password"}
                  required
                  onBlur={() => handleInputBlur("password")}
                  onChange={(event) =>
                    handleInputChange("password", event.target.value)
                  }
                  value={enteredValues.password}
                  className="px-4 block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                />
              </div>
            </div>
            <div className="text-red-500">
              {passwordIsInvalid && (
                <p>
                  Required! The password must be at least 8 characters long and
                  include at least one lowercase letter, one uppercase letter,
                  one number, and one non-alphanumeric symbol.
                </p>
              )}
            </div>
            <div className="mt-8 flex justify-center">
              <button
                onClick={handleSubmit}
                type="submit"
                className="flex justify-center rounded-md bg-indigo-600 px-7 py-2 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
              >
                Sign in
              </button>
            </div>
          </form>

          <p className="mt-10 text-center text-sm text-gray-500">
            Not a member?
            <a
              href="/register"
              className="font-semibold leading-6  hover:text-indigo-500 px-2"
            >
              Register here
            </a>
          </p>
        </div>
      </div>
    </>
  );
}

export default Login;
