import { useState, useEffect } from "react";
import {
  shipmentsToReceive,
  cancelShipment,
} from "../../fetch/requests/delivery";

function ShipmentsToRecieve() {
  const [shipments, setShipments] = useState([]);

  async function handleOnClickCancel(id) {
    try {
      const response = await cancelShipment(id);

      if (response.status !== 200) {
        throw new Error("Network response was not ok");
      }
    } catch (error) {
      alert("Some problem occurred.");
    }
  }

  useEffect(() => {
    (async function getShipments() {
      try {
        const response = await shipmentsToReceive();

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
      <div className="flex justify-center items-center h-screen bg-gray-100">
        <div className="flex flex-col w-full max-w-6xl p-4">
          <h1 className="text-3xl font-bold mb-6 text-center">
            Shipments to Receive
          </h1>
          <div className="overflow-x-auto">
            <div className="inline-block min-w-full py-2">
              <div className="overflow-hidden rounded-lg shadow-lg">
                <table className="min-w-full text-center text-sm font-light text-gray-700 bg-white">
                  <thead className="border-b border-gray-200 bg-gray-800 text-white">
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
                      <th scope="col" className="px-6 py-4"></th>
                    </tr>
                  </thead>
                  <tbody className="divide-y divide-gray-200">
                    {shipments.map((shipment) => (
                      <tr key={shipment.id} className="hover:bg-gray-100">
                        <td className="whitespace-nowrap px-6 py-4 font-medium">
                          <a
                            href={`/Users/Details/${shipment.buyerId}`}
                            className="text-blue-500 hover:underline"
                          >
                            {shipment.buyerUsername}
                          </a>
                        </td>
                        <td className="whitespace-nowrap px-6 py-4">
                          <a
                            href={`/Products/Details/${shipment.productId}`}
                            className="text-blue-500 hover:underline"
                          >
                            {shipment.productTitle}
                          </a>
                        </td>
                        <td className="whitespace-nowrap px-6 py-4">
                          {shipment.status}
                        </td>
                        <td className="whitespace-nowrap px-6 py-4 space-x-2">
                          {shipment.status === "Accepted" && (
                            <>
                              <a href={`/delivery/details/${shipment.id}`}>
                                <button
                                  type="button"
                                  className="text-white bg-blue-600 hover:bg-blue-700 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5"
                                >
                                  View
                                </button>
                              </a>
                            </>
                          )}
                          {shipment.status === "Requested" && (
                            <button
                              onClick={() => handleOnClickCancel(shipment.id)}
                              type="button"
                              className="text-white bg-red-600 hover:bg-red-700 focus:ring-4 focus:ring-red-300 font-medium rounded-lg text-sm px-5 py-2.5"
                            >
                              Cancel
                            </button>
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

export default ShipmentsToRecieve;
