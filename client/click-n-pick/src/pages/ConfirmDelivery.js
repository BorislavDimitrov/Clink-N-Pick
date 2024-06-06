import ReactIframe from "react-iframe";
import { useState, useEffect, useRef } from "react";
import { createLabel } from "../fetch/requests/delivery";
import { useParams } from "react-router-dom";

function ConfirmDelivery() {
  const [address, setAddress] = useState();
  const params = useParams();

  const [shippingLabelServices, setShippingLabelServices] = useState({
    cdAmount: 0,
    cdType: "",
    sms: "",
    goodsReceipt: "",
    deliveryReceipt: "",
    invoice: "",
  });

  const [clientSenderProfile, setClientSenderProfile] = useState({
    name: "",
    phones: "",
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
    event.preventDefault();
    const formData = new FormData(formRef.current);
    const label = {};
    formData.forEach((value, key) => {
      label[key] = value;
    });
    console.log(label);

    label["SenderOfficeCode"] = address.office.code;
    label["ReceiverOfficeCode"] = address.office.code;

    label["SenderClient"] = {
      Name: clientSenderProfile.Name,
      Phones: clientSenderProfile.Phones,
    };

    label["ReceiverClient"] = {
      Name: clientRecieverProfile.Name,
      Phones: clientRecieverProfile.Phones,
    };

    var response = await createLabel({ Label: label, Mode: "create" });
  }

  useEffect(() => {
    window.addEventListener("message", getAddress);

    return () => {
      window.removeEventListener("message", getAddress);
    };
  }, []);

  function handleChangeShippingLabelServices(event) {
    const { name, value } = event.target;

    setShippingLabelServices((prevClientProfile) => ({
      ...prevClientProfile,
      [name]: value,
    }));
  }
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
              Sender Client Name
            </label>
            <input
              type="text"
              name="name"
              onChange={(e) => {
                setClientSenderProfile((prevClientProfile) => ({
                  ...prevClientProfile,
                  ["Name"]: e.target.value,
                }));
              }}
              className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
              placeholder="Sender Client Name"
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700">
              Sender Phone
            </label>
            <input
              type="text"
              name="phone"
              className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
              placeholder=""
              onChange={(e) => {
                setClientSenderProfile((prevClientProfile) => ({
                  ...prevClientProfile,
                  ["Phones"]: [e.target.value],
                }));
              }}
            />
          </div>

          <div>
            <label className="block text-sm font-medium text-gray-700">
              Send Date
            </label>
            <input
              type="date"
              name="SendDate"
              className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
            />
          </div>
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
              name="name"
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
              name="receiverClient.phones[0]"
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
              Pack Count
            </label>
            <input
              type="number"
              name="PackCount"
              className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
              placeholder="Pack Count"
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700">
              Payment Receiver Amount
            </label>
            <input
              type="number"
              name="PaymentReceiverAmount"
              className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
              placeholder="PaymentReceiverAmount"
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700">
              Shipment Type
            </label>
            <select
              name="ShipmentType"
              className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
            >
              <option value="document">Document</option>
              <option value="pack">Pack</option>
              <option value="post_pack">Post Pack</option>
              <option value="pallet">Pallet</option>
              <option value="cargo">Cargo</option>
              <option value="documentpallet">Document Pallet</option>
              <option value="big_letter">Big Letter</option>
              <option value="small_letter">Small Letter</option>
              <option value="money_transfer">Money Transfer</option>
              <option value="pp">Post Transfer</option>
            </select>
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700">
              Weight
            </label>
            <input
              type="number"
              name="Weight"
              className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
              placeholder="Weight"
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700">
              Shipment Description
            </label>
            <input
              type="text"
              name="ShipmentDescription"
              className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
              placeholder="Shipment Description"
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700">
              Order Number
            </label>
            <input
              type="text"
              name="OrderNumber"
              className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
              placeholder="Order Number"
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700">
              SMS Notification
            </label>
            <input
              type="checkbox"
              name="sms"
              onChange={handleChangeShippingLabelServices}
              className="mt-1 block"
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700">
              CD Amount
            </label>
            <input
              type="number"
              name="cdAmount"
              //value={formData.services.cdAmount}
              onChange={handleChangeShippingLabelServices}
              className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
              placeholder="CD Amount"
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700">
              CD Type
            </label>
            <select
              name="cdType"
              // value={formData.shipmentType}
              onChange={handleChangeShippingLabelServices}
              className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
            >
              <option selected value="get">
                Get
              </option>
              <option value="give">Give</option>
            </select>
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700">
              Goods Receipt
            </label>
            <input
              type="checkbox"
              name="goodsReceipt"
              //checked={formData.services.goodsReceipt}
              //onChange={handleChange}
              className="mt-1 block"
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700">
              Delivery Receipt
            </label>
            <input
              type="checkbox"
              name="deliveryReceipt"
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
              name="invoice"
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
            src="https://staging.officelocator.econt.com?shopUrl=https://example.staging.officelocator.econt.com&officeType=office&lang=en"
          ></ReactIframe>
        </div>
      </div>
    </div>
  </>;
}

export default ConfirmDelivery;
