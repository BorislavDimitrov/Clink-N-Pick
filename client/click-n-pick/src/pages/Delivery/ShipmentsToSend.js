import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import {
  shipmentsToSend,
  cancelShipment,
  declineShipment,
} from "../../fetch/requests/delivery";

function ShipmentsToSend() {
  const [shipments, setShipments] = useState([]);

  async function handleOnClickDecline(id) {
    try {
      const response = await declineShipment(id);

      if (response.status !== 200) {
        throw new Error("Network response was not ok");
      }

      redirectTo();
    } catch (error) {
      alert("Some problem occurred.");
    }
  }

  async function handleOnClickCancel(id) {
    try {
      const response = await cancelShipment(id);

      if (response.status !== 200) {
        throw new Error("Network response was not ok");
      }

      redirectTo();
    } catch (error) {
      alert("Some problem occurred.");
    }
  }

  function redirectTo() {
    window.location.reload();
  }

  useEffect(() => {
    (async function getShipments() {
      try {
        const response = await shipmentsToSend();

        if (response.status !== 200) {
          throw new Error("Network response was not ok");
        }

        var data = await response.json();
        setShipments(data.shipments);
      } catch (error) {
        alert("Some problem occurred.");
      }
    })();
  }, []);
  return (
    <>
      <div className="flex justify-center items-center h-screen">
        <div className="flex flex-col">
          <h1 className="text-3xl font-bold mb-6">Shipments to send</h1>
          <div className="overflow-x-auto sm:-mx-6 lg:-mx-8">
            <div className="inline-block max-w-4xl py-2 sm:px-6 lg:px-8">
              <div className="overflow-hidden bg-red-50">
                <table className="min-w-full text-center text-sm font-light text-surface dark:text-white">
                  <thead className="border-b border-neutral-200 bg-[#332D2D] font-medium text-white dark:border-white/10">
                    <tr>
                      <th scope="col" className="px-6 py-4">
                        User
                      </th>
                      <th scope="col" className="px-6 py-4">
                        Product
                      </th>
                      <th scope="col" className="px-6 py-4">
                        Shipment Status
                      </th>
                      <th scope="col" className="px-6 py-4">
                        <Link to="/Administration/Orders/Create">
                          <button
                            type="button"
                            className="text-white bg-blue-700 hover:bg-blue-900 focus:ring-4 focus:blue-red-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2  focus:outline-none "
                          >
                            Create
                          </button>
                        </Link>
                      </th>
                    </tr>
                  </thead>
                  <tbody>
                    {shipments.map((shipment) => (
                      <tr className="border-b border-neutral-200 dark:border-white/10">
                        <td className="whitespace-nowrap px-6 py-4 font-medium">
                          {shipment.buyerUsername}
                        </td>
                        <td className="whitespace-nowrap px-6 py-4">
                          <Link to={`/Products/Details/${shipment.productId}`}>
                            {shipment.productTitle}
                          </Link>
                        </td>
                        <td className="whitespace-nowrap px-6 py-4">
                          {shipment.status}
                        </td>
                        <td className="whitespace-nowrap px-6 py-4">
                          {shipment.status === "Accepted" && (
                            <>
                              <a href={`/delivery/details/${shipment.id}`}>
                                <button
                                  type="button"
                                  className="text-white bg-blue-700 hover:bg-blue-900 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 focus:outline-none"
                                >
                                  View
                                </button>
                              </a>

                              <button
                                onClick={() => handleOnClickCancel(shipment.id)}
                                type="button"
                                className="text-white bg-red-700 hover:bg-red-900 focus:ring-4 focus:ring-red-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 focus:outline-none"
                              >
                                Cancel
                              </button>
                            </>
                          )}
                          {shipment.status === "Requested" && (
                            <>
                              <Link to={`/Delivery/Accept/${shipment.id}`}>
                                <button
                                  type="button"
                                  className="text-white bg-green-700 hover:bg-blue-900 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 focus:outline-none"
                                >
                                  Accept
                                </button>
                              </Link>
                              <button
                                onClick={() =>
                                  handleOnClickDecline(shipment.id)
                                }
                                type="button"
                                className="text-white bg-red-700 hover:bg-red-900 focus:ring-4 focus:ring-red-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 focus:outline-none"
                              >
                                Decline
                              </button>
                            </>
                          )}
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

export default ShipmentsToSend;
