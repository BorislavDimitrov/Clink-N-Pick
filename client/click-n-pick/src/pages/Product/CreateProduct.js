import { useState, useRef, useEffect } from "react";

import Modal from "../../components/Modal";
import { getAll } from "../../fetch/requests/categories";
import {
  isTitleValid,
  isPriceValid,
  isDescriptionValid,
  areImagesValid,
  isImageValid,
} from "../../Utility/validations";
import { createProduct } from "../../fetch/requests/products";
import Tippy from "@tippyjs/react";
import "tippy.js/dist/tippy.css";

function CreateProduct() {
  const modal = useRef();

  const [enteredValues, setEnteredValues] = useState({
    title: "",
    price: null,
    categoryId: "",
    description: "",
    thumbnailImage: null,
    images: null,
  });

  const [didEdit, setDidEdit] = useState({
    title: false,
    price: false,
    description: false,
  });

  const [imagesIsValid, setImagesIsValid] = useState(true);
  const [thumbnailImageIsValid, setthumbnailImageIsValid] = useState(true);
  const [responseResult, setResponseResult] = useState(null);
  const [categories, setCategories] = useState(null);

  useEffect(() => {
    (async function getCategories() {
      const response = await getAll();
      var data = await response.json();

      setCategories(data.categories);
      setEnteredValues((prevValues) => ({
        ...prevValues,
        ["categoryId"]: data.categories[0].id,
      }));
    })();
  }, []);

  const titleIsInvalid = didEdit.title && !isTitleValid(enteredValues.title);
  const priceIsInvalid =
    didEdit.price && !isPriceValid(enteredValues.price, 1, 50000);
  const descriptionIsInvalid =
    didEdit.description && !isDescriptionValid(enteredValues.description);

  async function handleSubmit(event) {
    event.preventDefault();

    let anyInvalid =
      titleIsInvalid ||
      priceIsInvalid ||
      descriptionIsInvalid ||
      !thumbnailImageIsValid ||
      !imagesIsValid;

    if (enteredValues.thumbnailImage === null) {
      setthumbnailImageIsValid(false);
      anyInvalid = true;
    }

    if (enteredValues.images === null) {
      setImagesIsValid(false);
      anyInvalid = true;
    }

    if (anyInvalid === true) {
      return;
    }

    const formData = new FormData();
    formData.append("Title", enteredValues.title);
    formData.append("Price", enteredValues.price);
    formData.append("CategoryId", enteredValues.categoryId);
    formData.append("Description", enteredValues.description);
    if (enteredValues.thumbnailImage) {
      formData.append("ThumbnailImage", enteredValues.thumbnailImage);
    }

    for (let i = 0; i < enteredValues.images.length; i++) {
      formData.append("Images", enteredValues.images[i]);
    }

    try {
      var response = await createProduct(formData, true);

      if (response.status !== 200) {
        throw new Error("Network response was not ok");
      }

      setResponseResult("ok");
      modal.current.open();
    } catch (error) {
      setResponseResult("bad");
      modal.current.open();
    }
  }

  function handleThumbnailImageChange(event) {
    var thumbnailImage = event.target.files[0];

    if (!thumbnailImage) {
      return;
    }

    setthumbnailImageIsValid(isImageValid(thumbnailImage));
    setEnteredValues((prevValues) => ({
      ...prevValues,
      thumbnailImage,
    }));
  }

  function handleImagesChange(event) {
    var images = event.target.files;

    if (!images) {
      return;
    }

    setImagesIsValid(areImagesValid(images, 10));
    setEnteredValues((prevValues) => ({
      ...prevValues,
      images,
    }));
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
    window.location.href = "/products/myProducts";
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
              Successful create!
            </h2>
          </>
        )}
        {responseResult === "bad" && (
          <>
            <h2 className="text-xl font-bold text-red-700 my-4">
              Creationg Failed!
            </h2>
            <p className="text-stone-600 mb-4">
              Please check the information you provide and try again.
            </p>
          </>
        )}
      </Modal>

      <div className="flex min-h-full flex-col justify-center px-6 py-12 lg:px-8">
        <div className="sm:mx-auto sm:w-full sm:max-w-sm">
          <h2 className="mt-1 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900">
            Create Product
          </h2>
        </div>
        <div className="w-full max-w-xl mx-auto p-8">
          <form onSubmit={handleSubmit}>
            <div className="bg-white dark:bg-gray-800 p-8 rounded-lg shadow-md border dark:border-gray-700 items-center">
              <div className="mb-6">
                <div className="mt-4">
                  <label className="block text-gray-700 dark:text-white mb-1">
                    Title
                    <Tippy
                      content={
                        <p className="text-red-400 text-base font-medium">
                          Required! Between 5 and 30 letters.
                        </p>
                      }
                    >
                      <span className="mx-1 font-medium text-lg">
                        &#128712;
                      </span>
                    </Tippy>
                  </label>
                  <input
                    name="email"
                    required
                    type="text"
                    onBlur={() => handleInputBlur("title")}
                    onChange={(event) =>
                      handleInputChange("title", event.target.value)
                    }
                    value={enteredValues.title}
                    className="w-full rounded-lg border py-2 px-3 dark:bg-gray-700 dark:text-white dark:border-none"
                  />

                  <div className="text-red-500">
                    {titleIsInvalid && (
                      <p>Please enter a title with 5 to 30 symbols</p>
                    )}
                  </div>
                </div>

                <div className="mt-4">
                  <label className="block text-gray-700 dark:text-white mb-1">
                    Price
                    <Tippy
                      content={
                        <p className="text-red-400 text-base font-medium">
                          Required! Between 1 - 50 000.
                          <span className="text-white pl-2">
                            Please provide a price in the range of 1 - 50 000.
                          </span>
                        </p>
                      }
                    >
                      <span className="mx-1 font-medium text-lg">
                        &#128712;
                      </span>
                    </Tippy>
                  </label>
                  <input
                    name="price"
                    min="1"
                    max="50000"
                    step="0.01"
                    required
                    type="number"
                    onBlur={() => handleInputBlur("price")}
                    onChange={(event) =>
                      handleInputChange("price", event.target.value)
                    }
                    value={enteredValues.price}
                    className="w-full rounded-lg border py-2 px-3 dark:bg-gray-700 dark:text-white dark:border-none"
                  />

                  <div className="text-red-500">
                    {priceIsInvalid && (
                      <p>The price must be between 1 and 50 000.</p>
                    )}
                  </div>
                </div>
              </div>

              <label className="block mt-4">
                <span className="text-gray-700">Category</span>
                <select
                  onChange={(event) =>
                    handleInputChange("categoryId", event.target.value)
                  }
                  className="form-select  mt-1 block w-full rounded-lg border py-2 px-3"
                >
                  {categories &&
                    categories.map((category) => (
                      <option key={category.id} value={category.id}>
                        {category.name}
                      </option>
                    ))}
                </select>
              </label>

              <div className="mt-4">
                <label className="block text-gray-700 dark:text-white mb-1">
                  Description
                  <Tippy
                    content={
                      <p className="text-red-400 text-base font-medium">
                        Required! Between 1 and 2000 letters.
                      </p>
                    }
                  >
                    <span className="mx-1 font-medium	 text-lg">&#128712;</span>
                  </Tippy>
                </label>
                <textarea
                  name="description"
                  required
                  type="textarea"
                  onBlur={() => handleInputBlur("description")}
                  onChange={(event) =>
                    handleInputChange("description", event.target.value)
                  }
                  value={enteredValues.description}
                  className="w-full rounded-lg border py-2 px-3 dark:bg-gray-700 dark:text-white dark:border-none h-28"
                />

                <div className="text-red-500">
                  {descriptionIsInvalid && (
                    <p>Please provide description up to 2000 symbols.</p>
                  )}
                </div>
              </div>

              <div className=" mt-10">
                <div className="flex flex-col justify-center items-center space-y-5 sm:flex-row sm:space-y-0 ">
                  <Tippy
                    content={
                      <p className="text-red-400 text-base font-medium">
                        Required! Maximum size of an image is 2 MB.
                      </p>
                    }
                  >
                    <div className="flex flex-col space-y-2  w-full">
                      <label
                        className="block mb-2 text-sm font-medium text-gray-900 dark:text-white"
                        for="user_avatar"
                      >
                        Thumbnail Image
                      </label>
                      <input
                        className="block w-full text-sm text-gray-900 border border-gray-300 rounded-lg cursor-pointer bg-gray-50 dark:text-gray-400 focus:outline-none dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400"
                        aria-describedby="user_avatar_help"
                        id="user_avatar"
                        type="file"
                        accept="image/*"
                        onChange={handleThumbnailImageChange}
                      />
                      {/* <div
                        className="mt-1 text-sm text-gray-500 dark:text-gray-300"
                        id="user_avatar_help"
                      >
                        A profile picture is useful to confirm your are logged
                        into your account
                      </div> */}

                      {/* <label
                        htmlFor="image"
                        className="py-3.5 px-7 text-base font-medium text-indigo-100 focus:outline-none bg-[#4e53e0] rounded-lg border border-indigo-200 hover:bg-indigo-900 focus:z-10 focus:ring-4 focus:ring-indigo-200 cursor-pointer"
                      >
                        Upload thumbnail image
                      </label>
                      <input
                        type="file"
                        name="image"
                        id="image"
                        accept="image/*"
                        className="hidden"
                        onChange={handleThumbnailImageChange}
                      /> */}
                    </div>
                  </Tippy>
                </div>
              </div>
              <div className="text-red-500 mt-4">
                {!thumbnailImageIsValid && (
                  <p>Please provide an image with maximum size of 2 MB.</p>
                )}
              </div>

              <div className=" mt-10">
                <div className="flex flex-col justify-center items-center space-y-5 sm:flex-row sm:space-y-0  ">
                  <Tippy
                    content={
                      <p className="text-red-400 text-base font-medium">
                        Required atleast 1 image! Maximum 10 Images. Each image
                        cannot be bigger than 2 MB.
                      </p>
                    }
                  >
                    <div className="flex flex-col space-y-2  w-full">
                      <label
                        className="block text-sm font-medium text-gray-900 dark:text-white"
                        for="user_avatar"
                      >
                        Images
                      </label>
                      <input
                        className="block w-full text-sm text-gray-900 border border-gray-300 rounded-lg cursor-pointer bg-gray-50 dark:text-gray-400 focus:outline-none dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400"
                        aria-describedby="user_avatar_help"
                        id="user_avatar"
                        type="file"
                        accept="image/*"
                        multiple
                        onChange={handleImagesChange}
                      />
                      {/* <label
                        htmlFor="images"
                        className="py-3.5 px-7 text-base font-medium text-indigo-100 focus:outline-none bg-[#4e53e0] rounded-lg border border-indigo-200 hover:bg-indigo-900 focus:z-10 focus:ring-4 focus:ring-indigo-200 cursor-pointer"
                      >
                        Upload images
                      </label>
                      <input
                        type="file"
                        name="images"
                        id="images"
                        accept="image/*"
                        multiple
                        className="hidden"
                        onChange={handleImagesChange}
                      /> */}
                    </div>
                  </Tippy>
                </div>
              </div>
              <div className="text-red-500 mt-4">
                {!imagesIsValid && (
                  <p>
                    Please provide up to 10 images, each of with maximum size of
                    2MB.
                  </p>
                )}
              </div>
              <div className="mt-8 flex justify-center">
                <button
                  className="flex justify-center rounded-md bg-indigo-600 px-10 py-3 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
                  data-ripple-light="true"
                >
                  Create
                </button>
              </div>
            </div>
          </form>
        </div>
      </div>
    </>
  );
}

export default CreateProduct;
