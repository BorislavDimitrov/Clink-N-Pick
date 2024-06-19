import { useState, useRef } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";

import Modal from "../../components/Modal";
import { forgotPasswordChange } from "../../fetch/requests/identity";
import Logo from "../../img/logo.jpg";
import {
  isPasswordValid,
  isEqualsToOtherValue,
} from "../../Utility/validations";

import Tippy from "@tippyjs/react";
import "tippy.js/dist/tippy.css";

function ForgotPasswordChange() {
  const [searchParams, setSearchParams] = useSearchParams();

  const email = searchParams.get("email");
  const resetPasswordToken = searchParams.get("token");

  const navigate = useNavigate();

  const modal = useRef();

  const [enteredValues, setEnteredValues] = useState({
    password: "",
    confirmPassword: "",
  });

  const [didEdit, setDidEdit] = useState({
    password: false,
    confirmPassword: false,
  });

  const [showPassword, setShowPassword] = useState({
    password: false,
    confirmPassword: false,
  });

  const [responseResult, setResponseResult] = useState(null);

  const passwordIsInvalid =
    didEdit.password && !isPasswordValid(enteredValues.password);
  const confirmPasswordIsInvalid =
    didEdit.confirmPassword &&
    !isEqualsToOtherValue(
      enteredValues.password,
      enteredValues.confirmPassword
    );

  async function handleSubmit(event) {
    event.preventDefault();

    const anyInvalid = passwordIsInvalid || confirmPasswordIsInvalid;

    if (anyInvalid === true) {
      return;
    }

    try {
      const response = await forgotPasswordChange({
        email,
        resetPasswordToken,
        ...enteredValues,
      });

      if (response.status !== 200) {
        throw new Error("Network response was not ok");
      }

      setResponseResult("ok");
      modal.current.open();
    } catch (error) {
      modal.current.open();
      setResponseResult("bad");
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

  function togglePasswordVisibility(identifier) {
    setShowPassword((prevVisibility) => ({
      ...prevVisibility,
      [identifier]: !prevVisibility[identifier],
    }));
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
        {responseResult === "ok" && (
          <>
            <h2 className="text-xl font-bold text-green-700 my-4">
              Successful Change of your Password!
            </h2>
            <p className="text-stone-600 mb-4">
              You can now Sign Up with your new password.
            </p>
          </>
        )}
        {responseResult === "bad" && (
          <>
            <h2 className="text-xl font-bold text-red-700 my-4">
              Reseting your password failed!
            </h2>
            <p className="text-stone-600 mb-4">Please try again.</p>
          </>
        )}
      </Modal>
      <div className="flex min-h-full flex-col justify-center px-6 py-12 lg:px-8">
        <form onSubmit={handleSubmit} className=" dark:bg-gray-900">
          <div className="sm:mx-auto sm:w-full sm:max-w-sm">
            <img className="mx-auto " src={Logo} alt="Your Company" />
            <h2 className="mt-1 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900">
              Reset password
            </h2>
          </div>
          <div className="w-full max-w-xl mx-auto p-8">
            <div className="bg-white dark:bg-gray-800 p-8 rounded-lg shadow-md border dark:border-gray-700 items-center">
              <div className="mb-6">
                <div className="grid grid-cols-2 gap-4 mt-4">
                  <div>
                    <label className="block text-gray-700 dark:text-white mb-1">
                      Password
                      <Tippy
                        content={
                          <p className="text-red-400 text-base font-medium">
                            Required! The password must be at least 8 characters
                            long and include at least one lowercase letter, one
                            uppercase letter, one number, and one
                            non-alphanumeric symbol.
                          </p>
                        }
                      >
                        <span className="mx-1 font-medium text-lg">
                          &#128712;
                        </span>
                      </Tippy>
                    </label>
                    <div className="relative w-full">
                      <div className="absolute inset-y-0 right-0 flex items-center px-2">
                        <input className="hidden" />
                        <Tippy content="Password visibility">
                          <span
                            className="cursor-pointer"
                            onClick={() => togglePasswordVisibility("password")}
                          >
                            &#128274;
                          </span>
                        </Tippy>
                      </div>
                      <input
                        name="password"
                        required
                        type={
                          showPassword.password === true ? "text" : "password"
                        }
                        onBlur={() => handleInputBlur("password")}
                        onChange={(event) =>
                          handleInputChange("password", event.target.value)
                        }
                        value={enteredValues.password}
                        className="w-full rounded-lg border py-2 px-3 dark:bg-gray-700 dark:text-white dark:border-none"
                      />
                    </div>

                    <div className="text-red-500">
                      {passwordIsInvalid && (
                        <p>
                          The password must be atleast 8 characters long, to
                          contains lower case letter, upper case letter, a
                          number and a non-numberic symbol.
                        </p>
                      )}
                    </div>
                  </div>
                  <div>
                    <label className="block text-gray-700 dark:text-white mb-1">
                      Confirm Password
                      <Tippy
                        content={
                          <p className="text-red-400 text-base font-medium">
                            Required!
                            <span className="text-white pl-2">
                              Please confirm your password by entering it again.
                            </span>
                          </p>
                        }
                      >
                        <span className="mx-1 color: font-medium text-lg">
                          &#128712;
                        </span>
                      </Tippy>
                    </label>
                    <div className="relative w-full">
                      <div className="absolute inset-y-0 right-0 flex items-center px-2">
                        <input className="hidden" />
                        <Tippy content="Password visibility">
                          <span
                            className="cursor-pointer"
                            onClick={() =>
                              togglePasswordVisibility("confirmPassword")
                            }
                          >
                            &#128274;
                          </span>
                        </Tippy>
                      </div>
                      <input
                        name="confirmPassword"
                        required
                        type={
                          showPassword.confirmPassword === true
                            ? "text"
                            : "password"
                        }
                        onBlur={() => handleInputBlur("confirmPassword")}
                        onChange={(event) =>
                          handleInputChange(
                            "confirmPassword",
                            event.target.value
                          )
                        }
                        value={enteredValues.confirmPassword}
                        className="w-full rounded-lg border py-2 px-3 dark:bg-gray-700 dark:text-white dark:border-none"
                      />
                    </div>

                    <div className="text-red-500">
                      {confirmPasswordIsInvalid && (
                        <p>The password does not match.</p>
                      )}
                    </div>
                  </div>
                </div>

                <div className="grid grid-cols-2 gap-4"></div>
              </div>

              <div className="mt-8 flex justify-center">
                <button
                  className="flex justify-center rounded-md bg-indigo-600 px-7 py-2 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
                  data-ripple-light="true"
                >
                  Change password
                </button>
              </div>
              <p className="mt-10 text-center text-sm text-gray-500 ">
                Already registered?
                <a
                  href="/login"
                  className="font-semibold leading-6 text-indigo-600 hover:text-indigo-500 px-2"
                >
                  Login here
                </a>
              </p>
            </div>
          </div>
        </form>
      </div>
    </>
  );
}

export default ForgotPasswordChange;
