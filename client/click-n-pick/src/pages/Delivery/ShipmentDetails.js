import { useParams } from "react-router-dom";
import { useState, useEffect } from "react";
import { getShipmentDetails } from "../../fetch/requests/delivery";

function ShipmentDetails() {
  const params = useParams();

  const [shipment, setShipment] = useState();

  useEffect(() => {
    (async function getShipments() {
      try {
        const response = await getShipmentDetails(params.id);

        if (response.status !== 200) {
          throw new Error("Network response was not ok");
        }

        var data = await response.json();
        setShipment(data);
      } catch (error) {
        alert("Some problem occurred.");
      }
    })();
  }, []);

  return (
    <>
      {shipment && (
        <div className="container mx-auto p-6">
          <div className="bg-white rounded-lg shadow-lg p-6">
            <h1 className="text-2xl font-bold mb-4">Shipment Details</h1>
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
              <div>
                <h2 className="text-xl font-semibold mb-2">
                  Shipment Information
                </h2>
                <p>
                  <strong>Shipment Type:</strong> {shipment.shipmentType}
                </p>
                <p>
                  <strong>Pack Count:</strong> {shipment.packCount}
                </p>
                <p>
                  <strong>Description:</strong> {shipment.shipmentDescription}
                </p>
                <p>
                  <strong>Weight:</strong> {shipment.weight} kg
                </p>
                <p>
                  <strong>Courier Status:</strong> {shipment.courierStatus}
                </p>
              </div>
              <div>
                <h2 className="text-xl font-semibold mb-2">
                  Sender Information
                </h2>
                <p>
                  <strong>Delivery Type:</strong> {shipment.senderDeliveryType}
                </p>
                <p>
                  <strong>Name:</strong> {shipment.senderClient.name}
                </p>
              </div>
              <div>
                <h2 className="text-xl font-semibold mb-2">
                  Receiver Information
                </h2>
                <p>
                  <strong>Delivery Type:</strong>{" "}
                  {shipment.receiverDeliveryType}
                </p>
                <p>
                  <strong>Name:</strong> {shipment.receiverClient.name}
                </p>
              </div>
              <div>
                <h2 className="text-xl font-semibold mb-2">
                  Financial Information
                </h2>
                <p>
                  <strong>Total Price:</strong> {shipment.totalPrice}
                </p>
                <p>
                  <strong>Sender Due Amount:</strong> {shipment.senderDueAmount}
                </p>
              </div>
              <div>
                <h2 className="text-xl font-semibold mb-2">
                  Services Information
                </h2>
                {shipment.services.map((service) => (
                  <>
                    <p>
                      <strong>Service Type:</strong>{" "}
                      {service.type === "C" ? "Courier" : service.type}
                    </p>
                    <p>
                      <strong>Service Price:</strong> {service.price}{" "}
                    </p>
                  </>
                ))}
              </div>
              <div>
                <h2 className="text-xl font-semibold mb-2">
                  Destination Details
                </h2>
                <p>
                  <strong>Destination Type:</strong>{" "}
                  {shipment.trackingEvents[0].destinationType}
                </p>
                <p>
                  <strong>Details:</strong>{" "}
                  {shipment.trackingEvents[0].destinationDetailsEn}
                </p>
                <p>
                  <strong>Expected Delivery Date:</strong>{" "}
                  {shipment.expectedDeliveryDate}
                </p>
              </div>
            </div>
          </div>
        </div>
      )}
    </>
  );
}

export default ShipmentDetails;
