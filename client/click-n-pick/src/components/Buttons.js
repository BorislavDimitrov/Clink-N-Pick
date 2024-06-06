import { Link, redirect } from "react-router-dom";
import { useState, useRef } from "react";
import { useNavigate } from "react-router-dom";
import { deleteProduct } from "../fetch/requests/products";

function Buttons({ id, isPromoted }) {
  async function onDeleteHandler(event) {
    debugger;
    if (event) {
      event.preventDefault();
    }
    try {
      const response = await deleteProduct(id);

      if (response.status !== 200) {
        throw new Error("Registration failed");
      }

      window.location.reload();
    } catch (error) {
      alert("Deletion failed");
    }
  }

  return (
    <>
      <div className="flex flex-row">
        <div class="ml-auto">
          <Link to={`/products/edit/${id}`}>
            <button
              type="button"
              class="focus:outline-none text-white bg-green-700 hover:bg-green-800 focus:ring-4 focus:ring-green-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 mb-2 dark:bg-green-600 dark:hover:bg-green-700 dark:focus:ring-green-800"
            >
              Edit
            </button>
          </Link>
        </div>
        <div class="ml-auto">
          <button
            onClick={onDeleteHandler}
            type="button"
            class="focus:outline-none text-white bg-red-700 hover:bg-red-800 focus:ring-4 focus:ring-red-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 mb-2 dark:bg-red-600 dark:hover:bg-red-700 dark:focus:ring-red-900"
          >
            Delete
          </button>
        </div>
        <div class="ml-auto">
          <Link to={`/products/promotion/${id}`}>
            <button
              disabled={isPromoted === true ? true : false}
              type="button"
              class="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800"
            >
              Promote
            </button>
          </Link>
        </div>
      </div>
    </>
  );
}

export default Buttons;
