import { useState, useEffect, useRef } from "react";
import { getAll } from "../fetch/requests/categories";
import { Link } from "react-router-dom";
import { deleteCategory } from "../fetch/requests/categories";
import Modal from "../components/Modal";

function AdminCategories() {
  const [categories, setCategories] = useState([]);
  const [responseResult, setResponseResult] = useState();

  const modal = useRef();

  useEffect(() => {
    (async function getCategories() {
      const response = await getAll();
      var data = await response.json();
      console.log(data);
      setCategories(data.categories);
    })();
  }, []);

  async function handleOnClick(id) {
    debugger;
    try {
      const response = await deleteCategory(id);
      console.log(response);

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
            <p className="text-stone-600 mb-4">Successful Registration!</p>
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
      <div class="flex justify-center items-center h-screen">
        <div class="flex flex-col">
          <div class="overflow-x-auto sm:-mx-6 lg:-mx-8">
            <div class="inline-block max-w-4xl py-2 sm:px-6 lg:px-8">
              <div class="overflow-hidden bg-red-50">
                <table class="min-w-full text-center text-sm font-light text-surface dark:text-white">
                  <thead class="border-b border-neutral-200 bg-[#332D2D] font-medium text-white dark:border-white/10">
                    <tr>
                      <th scope="col" class=" px-6 py-4">
                        Id
                      </th>
                      <th scope="col" class=" px-6 py-4">
                        Name
                      </th>
                      <th scope="col" class=" px-6 py-4">
                        <Link to="/Administration/Categories/Create">
                          <button
                            type="button"
                            class="text-white bg-blue-700 hover:bg-blue-900 focus:ring-4 focus:blue-red-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2  focus:outline-none "
                          >
                            Create
                          </button>
                        </Link>
                      </th>
                      <th scope="col" class=" px-6 py-4"></th>
                    </tr>
                  </thead>
                  <tbody>
                    {categories.map((category) => (
                      <tr class="border-b border-neutral-200 dark:border-white/10">
                        <td class="whitespace-nowrap  px-6 py-4 font-medium">
                          {category.id}
                        </td>
                        <td class="whitespace-nowrap  px-6 py-4">
                          {category.name}
                        </td>
                        <td class="whitespace-nowrap  px-6 py-4">
                          <button
                            type="button"
                            class="text-white bg-red-700 hover:bg-red-900 focus:ring-4 focus:ring-red-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2  focus:outline-none "
                            onClick={() => handleOnClick(category.id)}
                          >
                            Delete
                          </button>
                        </td>

                        <td class="whitespace-nowrap  px-6 py-4">
                          <Link
                            to={`/Administration/Categories/Edit/${category.id}`}
                          >
                            <button
                              type="button"
                              class="text-white bg-green-700 hover:bg-green-900 focus:ring-4 focus:blue-green-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2  focus:outline-none "
                            >
                              Edit
                            </button>
                          </Link>
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
