import { useState, useEffect, useRef } from "react";
import Modal from "../../components/Modal";
import { getById } from "../../fetch/requests/categories";
import { useParams } from "react-router-dom";
import { edit } from "../../fetch/requests/categories";

function EditCategory() {
  const [category, setCategory] = useState();
  const [responseResult, setResponseResult] = useState();
  const params = useParams();

  useEffect(() => {
    (async function getCategory() {
      const response = await getById(params.id);
      var data = await response.json();
      console.log(data);
      setCategory(data.name);
    })();
  }, []);

  const modal = useRef();

  async function handleSubmit(event) {
    event.preventDefault();

    try {
      const response = await edit({ CategoryId: params.id, Name: category });
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
    window.location.href = "/Administration/Categories";
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
              Successful editing!
            </h2>
            <p className="text-stone-600 mb-4">Successful Registration!</p>
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
        <div className="sm:mx-auto sm:w-full sm:max-w-sm mb-10">
          <h2 className="mt-1 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900">
            Edit category
          </h2>
        </div>

        <div className="mt-1 sm:mx-auto sm:w-full sm:max-w-sm shadow-md border rounded-lg p-8">
          <form className="space-y-6">
            <div>
              <label className="block text-sm font-medium leading-6 text-gray-900">
                Name
              </label>
              <div className="mt-2">
                <input
                  id="text"
                  name="text"
                  type="text"
                  required
                  onChange={(e) => setCategory(e.currentTarget.value)}
                  value={category && category}
                  className="px-4 block w-full rounded-md border-0 py-1.5 text-gray-900 shadow-sm ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6"
                />
              </div>
            </div>

            <div className="mt-8 flex justify-center">
              <button
                onClick={handleSubmit}
                type="submit"
                className="flex justify-center rounded-md bg-indigo-600 px-7 py-2 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
              >
                Edit
              </button>
            </div>
          </form>
        </div>
      </div>
    </>
  );
}

export default EditCategory;
