import { getEditProfileInfo } from "../fetch/requests/users";
import { useState, useEffect, useRef } from "react";
import Modal from "./Modal";
import {
  hasMaxLength,
  isImageValid,
  isPhoneNumberValid,
  isUsernameValid,
} from "../Utility/validations";
import { editProfile } from "../fetch/requests/users";
import { SetUserImageUrl } from "../Utility/user";
import Tippy from "@tippyjs/react";
import "tippy.js/dist/tippy.css";

function EditProfile() {
  const modal = useRef();
  const [responseResult, setResponseResult] = useState(null);
  const [userImageUrl, setUserImageUrl] = useState();
  const [imageIsValid, setImageIsValid] = useState(true);

  const [enteredValues, setEnteredValues] = useState({
    username: "",
    phoneNumber: "",
    bio: "",
    address: "",
    image: null,
  });

  const [didEdit, setDidEdit] = useState({
    username: false,
    phoneNumber: false,
    address: false,
    bio: false,
  });

  console.log(enteredValues);

  useEffect(() => {
    (async function GetProfileInfo() {
      try {
        const response = await getEditProfileInfo();
        console.log(response);

        if (response.status !== 200) {
          throw new Error("The email confirmation failed");
        }

        console.log("Fetch info");
        var data = await response.json();
        console.log(data);

        setUserImageUrl(data.profileImageUrl);
        const { profileImageUrl, ...neededData } = data;
        console.log(neededData);
        setEnteredValues(neededData);
      } catch (error) {
        setResponseResult("bad");
        modal.current.open();
      }
    })();
  }, []);

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

  function handleImageChange(event) {
    var image = event.target.files[0];

    if (!image) {
      return;
    }

    setImageIsValid(isImageValid(image));

    setEnteredValues((prevValues) => ({
      ...prevValues,
      image,
    }));
  }

  const usernameIsInvalid =
    didEdit.username && !isUsernameValid(enteredValues.username);
  const phoneNumberIsInvalid =
    didEdit.phoneNumber && !isPhoneNumberValid(enteredValues.phoneNumber);
  const bioIsInvalid = didEdit.bio && !hasMaxLength(enteredValues.bio, 2000);
  const addressIsInvalid =
    didEdit.address && !hasMaxLength(enteredValues.address, 100);

  async function handleSubmit(event) {
    event.preventDefault();
    console.log(imageIsValid);
    console.log(enteredValues);

    try {
      const formData = new FormData();
      formData.append("Username", enteredValues.username);
      formData.append("PhoneNumber", enteredValues.phoneNumber);
      formData.append("Bio", enteredValues.bio);
      formData.append("Address", enteredValues.address);
      if (enteredValues.image) {
        formData.append("Image", enteredValues.image);
      }

      const response = await editProfile(formData, true);

      if (response.status !== 200) {
        throw new Error("Network response was not ok");
      }

      const data = await response.json();
      const profileImageUrl = data.profileImageUrl;

      SetUserImageUrl(profileImageUrl);
      setUserImageUrl(profileImageUrl);

      setResponseResult("ok");
      modal.current.open();
    } catch (error) {
      setResponseResult("bad");
      modal.current.open();
    }
  }

  function redirectTo() {
    window.location.reload();
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
              Your profile information has been changed successfully!
            </h2>
          </>
        )}
        {responseResult === "bad" && (
          <>
            <h2 className="text-xl font-bold text-red-700 my-4">
              The changing of profile information has failed!
            </h2>
            <p className="text-stone-600 mb-4">
              Please check the information you provide and try again.
            </p>
          </>
        )}
      </Modal>
      <div
        id="publicProfile"
        className=" md:p-4 border-t-4 border-blue-400 rounded-xl mx-auto "
      >
        <div className="w-full px-6 pb-8 mt-8 sm:max-w-xl sm:rounded-lg">
          <h2 className="pl-6 text-2xl font-bold sm:text-xl">Public Profile</h2>
          <form onSubmit={handleSubmit}>
            <div className="grid max-w-2xl mx-auto mt-8">
              <div className="flex flex-col items-center space-y-5 sm:flex-row sm:space-y-0">
                {userImageUrl == null ? (
                  <img></img>
                ) : (
                  <img
                    className="object-cover w-40 h-40 p-1 rounded-full ring-2 ring-blue-500 "
                    src={userImageUrl}
                    alt="Bordered avatar"
                  />
                )}

                <Tippy
                  content={
                    <p className="text-red-400 text-base font-medium">
                      Maximum Image Size: 2MB
                      <span className="text-white pl-2">
                        Recommended Image Dimensions: 160x160 pixels or 2:1
                        aspect ratio for the best display quality."
                      </span>
                    </p>
                  }
                >
                  <div className="flex flex-col space-y-5 sm:ml-8">
                    <label
                      class="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
                      for="user_avatar"
                    >
                      Upload file
                    </label>
                    <input
                      class="block w-full text-sm text-gray-900 border border-gray-300 rounded-lg cursor-pointer bg-gray-50 dark:text-gray-400 focus:outline-none dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400"
                      aria-describedby="user_avatar_help"
                      id="user_avatar"
                      type="file"
                      onChange={handleImageChange}
                    />
                    <div
                      class="mt-1 text-sm text-gray-500 dark:text-gray-300"
                      id="user_avatar_help"
                    >
                      A profile picture is useful to confirm your are logged
                      into your account
                    </div>
                  </div>
                </Tippy>
              </div>
              <div className="text-red-500 mt-4">
                {!imageIsValid && <p>Maximum size of the image is 2 MB.</p>}
              </div>

              <div className="items-center mt-8 sm:mt-14 text-[#202142]">
                <div className="mb-2 sm:mb-6">
                  <label
                    for="email"
                    className="block mb-2 text-sm font-medium text-indigo-900 dark:text-white"
                  >
                    Username
                    <Tippy
                      content={
                        <p className="text-red-400 text-base font-medium">
                          Required! Between 5 and 30 symbols.
                          <span className="text-white pl-2">
                            Please provide a display name. This could be your
                            personal name or the name of your company, visible
                            to other users.
                          </span>
                        </p>
                      }
                    >
                      <span className=" ml-2 color: font-medium text-lg">
                        &#128712;
                      </span>
                    </Tippy>
                  </label>
                  <input
                    className="bg-indigo-50 border border-indigo-300 text-indigo-900 text-sm rounded-lg focus:ring-indigo-500 focus:border-indigo-500 block w-full p-2.5 "
                    name="username"
                    required
                    maxlength="30"
                    onBlur={() => handleInputBlur("username")}
                    onChange={(event) =>
                      handleInputChange("username", event.target.value)
                    }
                    value={enteredValues.username}
                  />
                </div>
                <div className="text-red-500 mt-4">
                  {usernameIsInvalid && (
                    <p>Please provide a username between 5 and 30 symbols.</p>
                  )}
                </div>

                <div className="mb-2 sm:mb-6">
                  <label
                    for="profession"
                    className="block mb-2 text-sm font-medium text-indigo-900 dark:text-white"
                  >
                    Phone number
                  </label>
                  <input
                    pattern="^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$"
                    type="text"
                    className="bg-indigo-50 border border-indigo-300 text-indigo-900 text-sm rounded-lg focus:ring-indigo-500 focus:border-indigo-500 block w-full p-2.5 "
                    name="phoneNumber"
                    onBlur={() => handleInputBlur("phoneNumber")}
                    onChange={(event) =>
                      handleInputChange("phoneNumber", event.target.value)
                    }
                    value={enteredValues.phoneNumber}
                  />
                </div>
                <div className="text-red-500 mt-4">
                  {phoneNumberIsInvalid && (
                    <p>Please provide a real phone number.</p>
                  )}
                </div>

                <div className="mb-2 sm:mb-6">
                  <label
                    for="email"
                    className="block mb-2 text-sm font-medium text-indigo-900 dark:text-white"
                  >
                    Address
                  </label>
                  <input
                    className="bg-indigo-50 border border-indigo-300 text-indigo-900 text-sm rounded-lg focus:ring-indigo-500 focus:border-indigo-500 block w-full p-2.5 "
                    name="username"
                    maxlength="100"
                    onBlur={() => handleInputBlur("address")}
                    onChange={(event) =>
                      handleInputChange("address", event.target.value)
                    }
                    value={enteredValues.address}
                  />
                </div>

                <div className="text-red-500 mt-4">
                  {addressIsInvalid && <p>Max 100 symbols.</p>}
                </div>

                <div className="mb-6">
                  <label
                    for="message"
                    className="block mb-2 text-sm font-medium text-indigo-900 dark:text-white"
                  >
                    Bio
                  </label>
                  <textarea
                    id="message"
                    placeholder="Up to 2000 symbols"
                    maxlength="2000"
                    rows="5"
                    className="block p-2.5 w-full text-sm text-indigo-900 bg-indigo-50 rounded-lg border border-indigo-300 focus:ring-indigo-500 focus:border-indigo-500 "
                    name="bio"
                    onBlur={() => handleInputBlur("bio")}
                    onChange={(event) =>
                      handleInputChange("bio", event.target.value)
                    }
                    value={enteredValues.bio}
                  ></textarea>
                </div>

                <div className="text-red-500 mt-4">
                  {bioIsInvalid && <p>Max 2000 symbols.</p>}
                </div>

                <div className="flex justify-end">
                  <button
                    type="submit"
                    className="text-white bg-blue-600  hover:bg-blue-700 focus:ring-4 focus:outline-none focus:ring-indigo-300 font-medium rounded-lg text-sm w-full sm:w-auto px-5 py-2.5 text-center dark:bg-indigo-600 dark:hover:bg-indigo-700 dark:focus:ring-indigo-800"
                  >
                    Save
                  </button>
                </div>
              </div>
            </div>
          </form>
        </div>
      </div>
    </>
  );
}

export default EditProfile;
