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
      <div className="flex justify-center items-center h-screen">
        <div className="flex flex-col">
          <div className="overflow-x-auto sm:-mx-6 lg:-mx-8">
            <div className="inline-block max-w-4xl py-2 sm:px-6 lg:px-8">
              <div className="overflow-hidden bg-red-50">
                <table className="min-w-full text-center text-sm font-light text-surface dark:text-white">
                  <thead className="border-b border-neutral-200 bg-[#332D2D] font-medium text-white dark:border-white/10">
                    <tr>
                      <th scope="col" className=" px-6 py-4">
                        Id
                      </th>
                      <th scope="col" className=" px-6 py-4">
                        Name
                      </th>
                      <th scope="col" className=" px-6 py-4">
                        <a href="/Administration/Categories/Create">
                          <button
                            type="button"
                            className="text-white bg-blue-700 hover:bg-blue-900 focus:ring-4 focus:blue-red-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2  focus:outline-none "
                          >
                            Create
                          </button>
                        </a>
                      </th>
                      <th scope="col" className=" px-6 py-4"></th>
                    </tr>
                  </thead>
                  <tbody>
                    {categories.map((category) => (
                      <tr className="border-b border-neutral-200 dark:border-white/10">
                        <td className="whitespace-nowrap  px-6 py-4 font-medium">
                          {category.id}
                        </td>
                        <td className="whitespace-nowrap  px-6 py-4">
                          {category.name}
                        </td>
                        <td className="whitespace-nowrap  px-6 py-4">
                          <button
                            type="button"
                            className="text-white bg-red-700 hover:bg-red-900 focus:ring-4 focus:ring-red-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2  focus:outline-none "
                            onClick={() => handleOnClick(category.id)}
                          >
                            Delete
                          </button>
                        </td>

                        <td className="whitespace-nowrap  px-6 py-4">
                          <a
                            href={`/Administration/Categories/Edit/${category.id}`}
                          >
                            <button
                              type="button"
                              className="text-white bg-green-700 hover:bg-green-900 focus:ring-4 focus:blue-green-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2  focus:outline-none "
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
