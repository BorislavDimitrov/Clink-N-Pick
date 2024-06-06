import { useState, useRef } from "react";
import { isEmailValid } from "../Utility/validations";
import { Link } from "react-router-dom";
import { forgotPasswordInput } from "../fetch/requests/identity";
import { useNavigate } from "react-router-dom";
import Modal from "../components/Modal";

import Logo from "../img/logo.jpg";

function ForgotPasswordInput() {
  const navigate = useNavigate();
  const modal = useRef();
  console.log(modal.current);

  const [enteredValues, setEnteredValues] = useState({
    email: "",
  });

  const [didEdit, setDidEdit] = useState({
    email: false,
  });

  const [responseResult, setResponseResult] = useState(null);

  const emailIsInvalid = didEdit.email && !isEmailValid(enteredValues.email);

  async function handleSubmit(event) {
    event.preventDefault();

    if (emailIsInvalid === true) {
      console.log("invalid input");
      return;
    }

    try {
      const response = await forgotPasswordInput(enteredValues);

      if (response.status !== 200) {
        throw new Error("The email confirmation failed");
      }

      setResponseResult("ok");

      modal.current.open();
    } catch (error) {
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

  function redirectTo() {
    navigate("/");
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
              Successfully sent reset password link!
            </h2>
            <p className="text-stone-600 mb-4">Successful Registration!</p>
            <p className="text-stone-600 mb-4">
              Check your email address to move forward.
            </p>
          </>
        )}
        {responseResult === "bad" && (
          <>
            <h2 className="text-xl font-bold text-red-700 my-4">
              Sending password reseting link failed!
            </h2>
            <p className="text-stone-600 mb-4">
              Please make sure you sent the email you registered with and try
              again.
            </p>
          </>
        )}
      </Modal>
      <div className="flex min-h-full flex-col justify-center px-6 py-12 lg:px-8">
        <div className="sm:mx-auto sm:w-full sm:max-w-sm">
          <img className="mx-auto" src={Logo} alt="Your Company" />
          <h2 className="mt-10 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900">
            Forgot your password ?
          </h2>
          <p className="mt-8 text-center text-base text-gray-500">
            We get it, stuff happens. Just enter your email address below and
            we'll send you a link to reset your password!
          </p>
        </div>

        <div className="mt-10 sm:mx-auto sm:w-full sm:max-w-sm">
          <form onSubmit={handleSubmit} className="space-y-6">
            <div>
              <label
                for="email"
                className="block text-sm font-medium leading-6 text-gray-900"
              >
                Email address
              </label>
              <div className="mt-2">
                <input
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

            <div className="flex flex-col items-center">
              <button
                type="submit"
                className="flex w-40 justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
              >
                Request reset link
              </button>

              <p className="mt-10 text-center text-sm text-gray-500">
                Remember your password?
                <Link
                  to="/login"
                  className="font-semibold leading-6 text-indigo-600 hover:text-indigo-500 px-2"
                >
                  Login here
                </Link>
              </p>
            </div>
          </form>
        </div>
      </div>
    </>
  );
}

export default ForgotPasswordInput;
