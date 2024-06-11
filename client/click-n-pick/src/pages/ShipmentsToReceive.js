import { Link } from "react-router-dom";
import { useState, useEffect } from "react";
import { shipmentsToReceive } from "../fetch/requests/delivery";

function ShipmentsToRecieve() {
  const [shipments, setShipments] = useState([]);

  useEffect(() => {
    (async function getShipments() {
      const response = await shipmentsToReceive();
      var data = await response.json();
      console.log(data);
      setShipments(data.shipments);
    })();
  }, []);

  return (
    <>
      <div class="flex justify-center items-center h-screen">
        <div class="flex flex-col">
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
                          {shipment.sellerUsername}
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
                            <Link to={`/Shipment/Details/${shipment.id}`}>
                              <button
                                type="button"
                                class="text-white bg-blue-700 hover:bg-blue-900 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 focus:outline-none"
                              >
                                View
                              </button>
                            </Link>
                          )}
                          <button
                            type="button"
                            class="text-white bg-red-700 hover:bg-red-900 focus:ring-4 focus:ring-red-300 font-medium rounded-lg text-sm px-5 py-2.5 me-2 focus:outline-none"
                          >
                            Cancel
                          </button>
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

export default ShipmentsToRecieve;
