import ReactIframe from "react-iframe";
import { useState, useEffect, useRef } from "react";
import { requestShipment } from "../fetch/requests/delivery";
import { useParams } from "react-router-dom";

function RequestDelivery() {
  const [address, setAddress] = useState();
  const params = useParams();

  const [shippingLabelServices, setShippingLabelServices] = useState({
    SmsNotification: false,
    GoodsReceipt: false,
    DeliveryReceipt: false,
    InvoiceBeforePayCD: false,
  });

  const [clientRecieverProfile, setClientRecieverProfile] = useState({
    name: "",
    phones: "",
  });

  function getAddress(event) {
    console.log(event.data);
    setAddress(event.data);
  }

  const formRef = useRef(null);

  async function handleSubmit(event) {
    console.log("shipments service:");
    console.log(shippingLabelServices);
    event.preventDefault();
    const formData = new FormData(formRef.current);
    const inputInfo = {};
    formData.forEach((value, key) => {
      inputInfo[key] = value === "on" ? true : value;
    });
    console.log(inputInfo);
    inputInfo["ReceiverOfficeCode"] = address.office.code;
    inputInfo["ReceiverName"] = clientRecieverProfile.Name;
    inputInfo["ReceiverPhoneNumber"] = clientRecieverProfile.Phones[0];
    inputInfo["SmsNotification"] = shippingLabelServices.SmsNotification;
    inputInfo["InvoiceBeforePayCD"] = shippingLabelServices.InvoiceBeforePayCD;
    inputInfo["GoodsReceipt"] = shippingLabelServices.GoodsReceipt;
    inputInfo["DeliveryReceipt"] = shippingLabelServices.DeliveryReceipt;
    inputInfo["ProductId"] = params.id;

    var response = await requestShipment(inputInfo);
  }

  useEffect(() => {
    debugger;
    window.addEventListener("message", getAddress);

    return () => {
      window.removeEventListener("message", getAddress);
    };
  }, []);

  function handleChangeShippingLabelServices(event) {
    debugger;
    const { name, checked } = event.target;
    console.log(name);
    console.log(checked);
    setShippingLabelServices((prevServices) => ({
      ...prevServices,
      [name]: checked,
    }));

    console.log(shippingLabelServices);
  }

  return (
    <>
      <div className="flex flex-col items-center h-screen">
        <div className="w-full max-w-5xl p-4">
          <form
            ref={formRef}
            className="grid grid-cols-1 md:grid-cols-3 gap-4"
            onSubmit={handleSubmit}
          >
            <div>
              <label className="block text-sm font-medium text-gray-700">
                Email on Delivery
              </label>
              <input
                type="email"
                name="EmailOnDelivery"
                className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
                placeholder="Email on Delivery"
              />
            </div>
            <div>
              <label className="block text-sm font-medium text-gray-700">
                SMS on Delivery
              </label>
              <input
                type="text"
                name="SmsOnDelivery"
                className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
                placeholder="SMS on Delivery"
              />
            </div>
            <div>
              <label className="block text-sm font-medium text-gray-700">
                Receiver Client Name
              </label>
              <input
                type="text"
                name="ReceiverName"
                onChange={(e) => {
                  setClientRecieverProfile((prevClientProfile) => ({
                    ...prevClientProfile,
                    ["Name"]: e.target.value,
                  }));
                }}
                className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
                placeholder="Receiver Client Name"
              />
            </div>
            <div>
              <label className="block text-sm font-medium text-gray-700">
                Receiver Phone
              </label>
              <input
                type="text"
                name="ReceiverPhoneNumber"
                onChange={(e) => {
                  setClientRecieverProfile((prevClientProfile) => ({
                    ...prevClientProfile,
                    ["Phones"]: [e.target.value],
                  }));
                }}
                className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
                placeholder="Receiver Phone"
              />
            </div>

            <div>
              <label className="block text-sm font-medium text-gray-700">
                SMS Notification
              </label>
              <input
                type="checkbox"
                name="SmsNotification"
                value={"false"}
                onChange={handleChangeShippingLabelServices}
                className="mt-1 block"
              />
            </div>
            <div>
              <label className="block text-sm font-medium text-gray-700">
                Goods Receipt
              </label>
              <input
                type="checkbox"
                name="GoodsReceipt"
                value={"false"}
                onChange={handleChangeShippingLabelServices}
                className="mt-1 block"
              />
            </div>
            <div>
              <label className="block text-sm font-medium text-gray-700">
                Delivery Receipt
              </label>
              <input
                type="checkbox"
                name="DeliveryReceipt"
                value={"false"}
                onChange={handleChangeShippingLabelServices}
                className="mt-1 block"
              />
            </div>
            <div>
              <label className="block text-sm font-medium text-gray-700">
                Invoice Before Pay CD
              </label>
              <input
                type="checkbox"
                value={"false"}
                name="InvoiceBeforePayCD"
                onChange={handleChangeShippingLabelServices}
                className="mt-1 block"
              />
            </div>
            <div>
              <button
                type="submit"
                className="w-full flex justify-center py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
              >
                Submit
              </button>
            </div>
          </form>
        </div>
        <div className="flex-grow w-full flex items-center justify-center">
          <div className="w-3/4 h-3/4">
            <ReactIframe
              className="w-full h-full"
              title="Econt Office Locator"
              allow="geolocation;"
              src="https://officelocator.econt.com/?shopUrl=https://example.staging.officelocator.econt.com&officeType=office&lang=en"
            ></ReactIframe>
          </div>
        </div>
      </div>
    </>
  );
}

export default RequestDelivery;
