import { useState, useEffect, useRef } from "react";

import { getAll } from "../../fetch/requests/categories";
import { deleteCategory } from "../../fetch/requests/categories";
import Modal from "../../components/Modal";

function AdminCategories() {
  const [categories, setCategories] = useState([]);
  const [responseResult, setResponseResult] = useState();

  const modal = useRef();

  useEffect(() => {
    (async function () {
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

  async function handleOnClick(id) {
    try {
      const response = await deleteCategory(id);

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
              Successful deleting!
            </h2>
          </>
        )}
        {responseResult === "bad" && (
          <>
            <h2 className="text-xl font-bold text-red-700 my-4">
              Deleting Failed!
            </h2>
          </>
        )}
      </Modal>
      <div className="flex justify-center items-center h-screen bg-gray-100">
        <div className="w-full max-w-6xl p-4">
          <div className="overflow-x-auto">
            <div className="inline-block min-w-full py-2">
              <div className="overflow-hidden bg-white shadow-md rounded-lg">
                <table className="min-w-full text-center text-sm font-light">
                  <thead className="bg-gray-800 text-white">
                    <tr>
                      <th scope="col" className="px-6 py-4">
                        Id
                      </th>
                      <th scope="col" className="px-6 py-4">
                        Name
                      </th>
                      <th scope="col" className="px-6 py-4">
                        <a href="/Administration/Categories/Create">
                          <button
                            type="button"
                            className="text-white bg-blue-600 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 mr-2 focus:outline-none"
                          >
                            Create
                          </button>
                        </a>
                      </th>
                      <th scope="col" className="px-6 py-4"></th>
                    </tr>
                  </thead>
                  <tbody>
                    {categories.map((category) => (
                      <tr className="border-b border-gray-200 hover:bg-gray-50">
                        <td className="whitespace-nowrap px-6 py-4 font-medium text-gray-700">
                          {category.id}
                        </td>
                        <td className="whitespace-nowrap px-6 py-4 text-gray-700">
                          {category.name}
                        </td>
                        <td className="whitespace-nowrap px-6 py-4">
                          <button
                            type="button"
                            className="text-white bg-red-600 hover:bg-red-800 focus:ring-4 focus:ring-red-300 font-medium rounded-lg text-sm px-5 py-2.5 mr-2 focus:outline-none"
                            onClick={() => handleOnClick(category.id)}
                          >
                            Delete
                          </button>
                        </td>
                        <td className="whitespace-nowrap px-6 py-4">
                          <a
                            href={`/Administration/Categories/Edit/${category.id}`}
                          >
                            <button
                              type="button"
                              className="text-white bg-green-600 hover:bg-green-800 focus:ring-4 focus:ring-green-300 font-medium rounded-lg text-sm px-5 py-2.5 mr-2 focus:outline-none"
                            >
                              Edit
                            </button>
                          </a>
                        </td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}

export default AdminCategories;
