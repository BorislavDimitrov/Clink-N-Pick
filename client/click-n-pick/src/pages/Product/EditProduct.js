import { useState, useRef, useEffect } from "react";
import { useParams } from "react-router-dom";

import Modal from "../../components/Modal";
import { getEditDetails } from "../../fetch/requests/products";
import { getAll } from "../../fetch/requests/categories";
import { editProduct } from "../../fetch/requests/products";
import {
  isTitleValid,
  isPriceValid,
  isDescriptionValid,
  areImagesValid,
  isImageValid,
} from "../../Utility/validations";
import Tippy from "@tippyjs/react";
import "tippy.js/dist/tippy.css";

function EditProduct() {
  const modal = useRef();

  var params = useParams();

  const [enteredValues, setEnteredValues] = useState({
    title: "",
    price: 0,
    discountPrice: 0,
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
    (async function getProductInfo() {
      try {
        const response = await getEditDetails(params.id);

        if (response.status !== 200) {
          throw new Error("Network response was not ok");
        }

        var data = await response.json();

        setEnteredValues({
          title: data.title,
          price: data.price,
          categoryId: data.categoryId,
          description: data.description,
          discountPrice: data.discountPrice,
        });

        setEnteredValues((prevValues) => ({
          ...prevValues,
          ["categoryId"]: data.categoryId,
        }));
      } catch (error) {
        alert("Some problem occurred.");
      }
    })();
  }, []);

  useEffect(() => {
    (async function getCategories() {
      try {
        const response = await getAll();

        if (response.status !== 200) {
          throw new Error("Network response was not ok");
        }

        var data = await response.json();
        setCategories(data.categories);
      } catch (error) {
        alert("Some problem occurred.");
      }
    })();
  }, []);

  const titleIsInvalid = didEdit.title && !isTitleValid(enteredValues.title);
  const priceIsInvalid =
    didEdit.price && !isPriceValid(enteredValues.price, 1, 100000);
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

    if (anyInvalid === true) {
      return;
    }

    const formData = new FormData();
    formData.append("Title", enteredValues.title);
    formData.append("Price", enteredValues.price);
    formData.append("DiscountPrice", enteredValues.discountPrice);
    formData.append("CategoryId", enteredValues.categoryId);
    formData.append("Description", enteredValues.description);
    formData.append("ProductId", params.id);
    if (enteredValues.thumbnailImage) {
      formData.append("ThumbnailImage", enteredValues.thumbnailImage);
    }

    if (enteredValues.images) {
      for (let i = 0; i < enteredValues.images.length; i++) {
        formData.append("Images", enteredValues.images[i]);
      }
    }

    try {
      var response = await editProduct(formData, true);

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
    if (
      identifier === "discountPrice" &&
      enteredValues.discountPrice >= enteredValues.price
    ) {
      var newValue = enteredValues.price - 1;

      if (newValue < 0) {
        newValue = 0;
      }

      setEnteredValues((prevValues) => ({
        ...prevValues,
        [identifier]: newValue,
      }));
    }

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
              Successful edit!
            </h2>
          </>
        )}
        {responseResult === "bad" && (
          <>
            <h2 className="text-xl font-bold text-red-700 my-4">
              Editing Failed!
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
            Edit Product
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
                          Required! Between 1 - 100 000.
                          <span className="text-white pl-2">
                            Please provide a price in the range of 1 - 100 000.
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
                    max="100000"
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
                      <p>The price must be between 1 and 100 000.</p>
                    )}
                  </div>
                </div>

                <div className="mt-4">
                  <label className="block text-gray-700 dark:text-white mb-1">
                    Discount price
                    <Tippy
                      content={
                        <p className="text-red-400 text-base font-medium">
                          Discount price cannot be bigger than the actual price.
                          <span className="text-white pl-2">
                            Leave it 0 if you dont want to apply any discount.
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
                    min="0"
                    max="100000"
                    step="0.01"
                    required
                    type="number"
                    onBlur={() => handleInputBlur("discountPrice")}
                    onChange={(event) =>
                      handleInputChange("discountPrice", event.target.value)
                    }
                    value={enteredValues.discountPrice}
                    className="w-full rounded-lg border py-2 px-3 dark:bg-gray-700 dark:text-white dark:border-none"
                  />
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
                      <option
                        selected={
                          enteredValues.categoryId === category.id
                            ? true
                            : false
                        }
                        key={category.id}
                        value={category.id}
                      >
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
                        Required! Between 5 and 30 letters.
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
                        Maximum size of an image is 2 MB.
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
                        Maximum 10 Images. Each image cannot be bigger than 2
                        MB.
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
                    </div>
                  </Tippy>
                </div>
              </div>
              <div className="text-red-500 mt-4">
                {!imagesIsValid && (
                  <p>Each image can be with maximum size of 2MB.</p>
                )}
              </div>
              <div className="mt-8 flex justify-center">
                <button
                  className="flex justify-center rounded-md bg-indigo-600 px-10 py-3 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
                  data-ripple-light="true"
                >
                  Edit
                </button>
              </div>
            </div>
          </form>
        </div>
      </div>
    </>
  );
}

export default EditProduct;
