import { deleteProduct } from "../fetch/requests/products";

function Buttons({ id, isPromoted }) {
  async function onDeleteHandler(event) {
    if (event) {
      event.preventDefault();
    }
    try {
      const response = await deleteProduct(id);

      if (response.status !== 200) {
        throw new Error("Network response was not ok");
      }

      window.location.reload();
    } catch (error) {
      alert("Some problem occurred.");
    }
  }

  return (
    <>
      <div className="flex flex-row">
        <div className="ml-auto">
          <a href={`/products/edit/${id}`}>
            <button
              type="button"
              className="focus:outline-none text-white bg-green-700 hover:bg-green-800 focus:ring-4 focus:ring-green-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 mb-2 dark:bg-green-600 dark:hover:bg-green-700 dark:focus:ring-green-800"
            >
              Edit
            </button>
          </a>
        </div>
        <div className="ml-auto">
          <button
            onClick={onDeleteHandler}
            type="button"
            className="focus:outline-none text-white bg-red-700 hover:bg-red-800 focus:ring-4 focus:ring-red-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 mb-2 dark:bg-red-600 dark:hover:bg-red-700 dark:focus:ring-red-900"
          >
            Delete
          </button>
        </div>
        <div className="ml-auto">
          <a href={`/products/promotion/${id}`}>
            <button
              disabled={isPromoted === true ? true : false}
              type="button"
              className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800"
            >
              Promote
            </button>
          </a>
        </div>
      </div>
    </>
  );
}

export default Buttons;
