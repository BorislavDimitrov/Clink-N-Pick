import { useState, useRef } from "react";
import { useNavigate } from "react-router-dom";
import Tippy from "@tippyjs/react";
import "tippy.js/dist/tippy.css";

import Modal from "../components/Modal";
import { passwordChange } from "../fetch/requests/identity";
import { isPasswordValid } from "../Utility/validations";
import { RemoveAuthToken } from "../Utility/auth";

function ChangePassword() {
  const navigate = useNavigate();

  const modal = useRef();

  const [enteredValues, setEnteredValues] = useState({
    oldPassword: "",
    newPassword: "",
  });

  const [didEdit, setDidEdit] = useState({
    oldPassword: false,
    newPassword: false,
  });

  const [showPassword, setShowPassword] = useState({
    oldPassword: false,
    newPassword: false,
  });

  const [responseResult, setResponseResult] = useState(null);

  const oldPasswordIsInvalid =
    didEdit.oldPassword && !isPasswordValid(enteredValues.oldPassword);
  const newPasswordIsInvalid =
    didEdit.newPassword && !isPasswordValid(enteredValues.newPassword);

  async function handleSubmit(event) {
    event.preventDefault();

    const anyInvalid = oldPasswordIsInvalid || newPasswordIsInvalid;

    if (anyInvalid === true) {
      return;
    }

    try {
      const response = await passwordChange({
        ...enteredValues,
      });

      if (response.status !== 200) {
        throw new Error("Network response was not ok");
      }

      RemoveAuthToken();

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
              Changing your password failed!
            </h2>
            <p className="text-stone-600 mb-4">Please try again.</p>
          </>
        )}
      </Modal>

      <div
        id="changePassword"
        className="md:p-4 mt-20 border-t-4 border-blue-400 rounded-xl mx-auto"
      >
        <div className="w-full px-6 pb-8  sm:max-w-xl sm:rounded-lg">
          <h2 className="pl-6 text-2xl font-bold sm:text-xl">
            Change Password
          </h2>

          <div className="grid max-w-2xl mx-auto mt-2">
            <form onSubmit={handleSubmit}>
              <div className="items-center mt-8 sm:mt-14 text-[#202142]">
                <div className="flex flex-col items-center w-full mb-2 space-x-0 space-y-2 sm:flex-row sm:space-x-4 sm:space-y-0 sm:mb-6">
                  <div className="mb-6 w-full">
                    <div className="grid grid-cols-2 gap-4 mt-4">
                      <div>
                        <label className="block mb-3 text-sm font-medium text-indigo-900">
                          Old Password
                        </label>
                        <div className="relative w-full">
                          <div className="absolute inset-y-0 right-0 flex items-center px-2">
                            <input className="hidden" />
                            <Tippy content="Password visibility">
                              <span
                                className="cursor-pointer"
                                onClick={() =>
                                  togglePasswordVisibility("oldPassword")
                                }
                              >
                                &#128274;
                              </span>
                            </Tippy>
                          </div>
                          <input
                            name="password"
                            required
                            type={
                              showPassword.oldPassword === true
                                ? "text"
                                : "password"
                            }
                            onBlur={() => handleInputBlur("oldPassword")}
                            onChange={(event) =>
                              handleInputChange(
                                "oldPassword",
                                event.target.value
                              )
                            }
                            value={enteredValues.oldPassword}
                            className="w-full rounded-lg border py-2 px-3 dark:bg-gray-700 dark:text-white dark:border-none bg-indigo-50 border-indigo-300 text-indigo-900 text-sm focus:ring-indigo-500 focus:border-indigo-500 block p-2.5 "
                          />
                        </div>

                        <div className="text-red-500">
                          {oldPasswordIsInvalid && (
                            <p>
                              The password must be atleast 8 characters long, to
                              contains lower case letter, upper case letter, a
                              number and a non-numberic symbol.
                            </p>
                          )}
                        </div>
                      </div>
                      <div>
                        <label className="block mb-2 text-sm font-medium text-indigo-900 -mt-1">
                          New Password
                          <Tippy
                            content={
                              <p className="text-red-400 text-base font-medium">
                                The password must be at least 8 characters long
                                and include at least one lowercase letter, one
                                uppercase letter, one number, and one
                                non-alphanumeric symbol.
                              </p>
                            }
                          >
                            <span className=" ml-2 color: font-medium text-lg">
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
                                  togglePasswordVisibility("newPassword")
                                }
                              >
                                &#128274;
                              </span>
                            </Tippy>
                          </div>
                          <input
                            name="newPassword"
                            required
                            type={
                              showPassword.newPassword === true
                                ? "text"
                                : "password"
                            }
                            onBlur={() => handleInputBlur("newPassword")}
                            onChange={(event) =>
                              handleInputChange(
                                "newPassword",
                                event.target.value
                              )
                            }
                            value={enteredValues.newPassword}
                            className="mt--1 w-full rounded-lg border py-2 px-3 dark:bg-gray-700 dark:text-white dark:border-none bg-indigo-50  border-indigo-300 text-indigo-900 text-sm  focus:ring-indigo-500 focus:border-indigo-500 block p-2.5 "
                          />
                        </div>

                        <div className="text-red-500">
                          {newPasswordIsInvalid && (
                            <p className="text-red-400 text-base font-medium">
                              The password must be at least 8 characters long
                              and include at least one lowercase letter, one
                              uppercase letter, one number, and one
                              non-alphanumeric symbol.
                            </p>
                          )}
                        </div>
                      </div>
                    </div>

                    <div className="grid grid-cols-2 gap-4"></div>
                  </div>
                </div>

                <div className="flex justify-end">
                  <button
                    type="submit"
                    className="text-white bg-indigo-700  hover:bg-indigo-800 focus:ring-4 focus:outline-none focus:ring-indigo-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center dark:bg-indigo-600 dark:hover:bg-indigo-700 dark:focus:ring-indigo-800"
                  >
                    Save
                  </button>
                </div>
              </div>
            </form>
          </div>
        </div>
      </div>
    </>
  );
}

export default ChangePassword;
