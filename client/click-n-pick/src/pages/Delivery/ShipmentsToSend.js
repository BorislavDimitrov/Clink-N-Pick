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
      console.log(response);

      if (response.status !== 200) {
        throw new Error("Network response was not ok");
      }

      // setResponseResult("ok");
      // modal.current.open();
    } catch (error) {
      // setResponseResult("bad");
      // modal.current.open();
    }
  }

  async function handleOnClickCancel(id) {
    try {
      const response = await cancelShipment(id);
      console.log(response);

      if (response.status !== 200) {
        throw new Error("Network response was not ok");
      }

      // setResponseResult("ok");
      // modal.current.open();
    } catch (error) {
      // setResponseResult("bad");
      // modal.current.open();
    }
  }

  function redirectTo() {
    window.location.reload();
  }

  useEffect(() => {
    (async function getShipments() {
      const response = await shipmentsToSend();
      var data = await response.json();
      console.log(data);
      setShipments(data.shipments);
    })();
  }, []);
  return (
    <>
      <div class="flex justify-center items-center h-screen">
        <div class="flex flex-col">
          <h1 class="text-3xl font-bold mb-6">Shipments to send</h1>
          <div class="overflow-x-auto sm:-mx-6 lg:-mx-8">
            <div class="inline-block max-w-4xl py-2 sm:px-6 lg:px-8">
              <div class="overflow-hidden bg-red-50">
                <table class="min-w-full text-center text-sm font-light text-surface dark:text-white">
                  <thead class="border-b border-neutral-200 bg-[#332D2D] font-medium text-white dark:border-white/10">
                    <tr>
                      <th scope="col" class="px-6 py-4">
                        User
                      </th>
                      <th scope="col" class="px-6 py-4">
                        Product
                      </th>
                      <th scope="col" class="px-6 py-4">
                        Shipment Status
                      </th>
                      <th scope="col" class="px-6 py-4">
                        <Link to="/Administration/Orders/Create">
                          <button
                            type="button"
                            class="text-white bg-blue-700 hover:bg-blue-900 focus:ring-4 focus:blue-red-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2  focus:outline-none "
                          >
                            Create
                          </button>
                        </Link>
                      </th>
                    </tr>
                  </thead>
                  <tbody>
                    {shipments.map((shipment) => (
                      <tr class="border-b border-neutral-200 dark:border-white/10">
                        <td class="whitespace-nowrap px-6 py-4 font-medium">
                          {shipment.buyerUsername}
                        </td>
                        <td class="whitespace-nowrap px-6 py-4">
                          <Link to={`/Products/Details/${shipment.productId}`}>
                            {shipment.productTitle}
                          </Link>
                        </td>
                        <td class="whitespace-nowrap px-6 py-4">
                          {shipment.status}
                        </td>
                        <td class="whitespace-nowrap px-6 py-4">
                          {shipment.status === "Accepted" && (
                            <>
                              <a href={`/delivery/details/${shipment.id}`}>
                                <button
                                  type="button"
                                  class="text-white bg-blue-700 hover:bg-blue-900 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 focus:outline-none"
                                >
                                  View
                                </button>
                              </a>

                              <button
                                onClick={() => handleOnClickCancel(shipment.id)}
                                type="button"
                                class="text-white bg-red-700 hover:bg-red-900 focus:ring-4 focus:ring-red-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 focus:outline-none"
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
                                  class="text-white bg-green-700 hover:bg-blue-900 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 focus:outline-none"
                                >
                                  Accept
                                </button>
                              </Link>
                              <button
                                onClick={() =>
                                  handleOnClickDecline(shipment.id)
                                }
                                type="button"
                                class="text-white bg-red-700 hover:bg-red-900 focus:ring-4 focus:ring-red-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 focus:outline-none"
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
