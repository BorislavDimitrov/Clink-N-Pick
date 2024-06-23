import ReactIframe from "react-iframe";
import { useState, useEffect, useRef } from "react";
import { useParams } from "react-router-dom";

import Modal from "../../components/Modal";
import {
  requestShipment,
  getCities,
  getQuarters,
  getStreets,
} from "../../fetch/requests/delivery";

function RequestDelivery() {
  const [deliverTo, setDeliverTo] = useState("Office");
  const [address, setAddress] = useState();
  const [cities, setCities] = useState([]);
  const [quarters, setQuarters] = useState([]);
  const [streets, setStreets] = useState([]);
  const [cityId, setCityId] = useState(1);
  const [cityPostCode, setCityPostCode] = useState();
  const [agree, setAgree] = useState(false);
  const [responseResult, setResponseResult] = useState();
  const params = useParams();

  var modal = useRef();

  const [shippingLabelServices, setShippingLabelServices] = useState({
    SmsNotification: false,
    GoodsReceipt: false,
    DeliveryReceipt: false,
    InvoiceBeforePayCD: false,
  });

  const [clientRecieverProfile, setClientReceiverProfile] = useState({
    name: "",
    phones: "",
  });

  useEffect(() => {
    (async function () {
      const response = await getCities();
      var data = await response.json();

      setCities(data.cities);
      setCityId(data.cities[0].id);
    })();
  }, []);

  useEffect(() => {
    (async function () {
      const params = new URLSearchParams({
        cityId: cityId,
      });

      const response = await getQuarters(params);
      var data = await response.json();

      setQuarters(data.quarters);
    })();
  }, [cityId]);

  useEffect(() => {
    (async function () {
      const params = new URLSearchParams({
        cityId: cityId,
      });

      const response = await getStreets(params);
      var data = await response.json();

      setStreets(data.streets);
    })();
  }, [cityId]);

  function getAddress(event) {
    setAddress(event.data);
  }

  const handleCheckboxChange = (event) => {
    setAgree(event.target.checked);
  };

  const formRef = useRef(null);

  async function handleSubmit(event) {
    event.preventDefault();
    const formData = new FormData(formRef.current);
    const inputInfo = {};
    formData.forEach((value, key) => {
      inputInfo[key] = value === "on" ? true : value;
    });

    if (deliverTo === "Office") {
      inputInfo["ReceiverOfficeCode"] = address.office.code;
      inputInfo["DeliveryLocation"] = "Office";
    }

    if (deliverTo === "Address") {
      inputInfo["PostCode"] = cityPostCode;
      inputInfo["DeliveryLocation"] = "Address";
    }

    inputInfo["ReceiverName"] = clientRecieverProfile.Name;
    inputInfo["ReceiverPhoneNumber"] = clientRecieverProfile.Phones[0];
    inputInfo["SmsNotification"] = shippingLabelServices.SmsNotification;
    inputInfo["InvoiceBeforePayCD"] = shippingLabelServices.InvoiceBeforePayCD;
    inputInfo["GoodsReceipt"] = shippingLabelServices.GoodsReceipt;
    inputInfo["DeliveryReceipt"] = shippingLabelServices.DeliveryReceipt;
    inputInfo["ProductId"] = params.id;

    try {
      var response = await requestShipment(inputInfo);

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

  useEffect(() => {
    window.addEventListener("message", getAddress);

    return () => {
      window.removeEventListener("message", getAddress);
    };
  }, []);

  function handleChangeShippingLabelServices(event) {
    const { name, checked } = event.target;

    setShippingLabelServices((prevServices) => ({
      ...prevServices,
      [name]: checked,
    }));
  }

  function redirectTo() {
    window.location.href = "/Delivery/ShipmentsToReceive";
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
              Successfully Requested Delivery!
            </h2>
          </>
        )}
        {responseResult === "bad" && (
          <>
            <h2 className="text-xl font-bold text-red-700 my-4">
              Requesting Delivery Failed!
            </h2>
            <p className="text-stone-600 mb-4">
              Please check the information you provide and try again.
            </p>
          </>
        )}
      </Modal>
      <div className="flex flex-col items-center h-screen">
        <div className="w-full max-w-5xl p-4 mt-20">
          <div className="flex justify-center mb-10">
            <button
              disabled={deliverTo === "Office"}
              onClick={() => setDeliverTo("Office")}
              className="mx-2 py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
            >
              To Office
            </button>
            <button
              disabled={deliverTo === "Address"}
              onClick={() => setDeliverTo("Address")}
              className="mx-2 py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-green-500"
            >
              To Address
            </button>
          </div>
          <form
            ref={formRef}
            className="grid grid-cols-1 md:grid-cols-3 gap-4"
            onSubmit={handleSubmit}
          >
            <div className="col-span-1">
              <label className="block text-sm font-medium text-gray-700">
                Receiver Name
              </label>
              <input
                type="text"
                required
                name="ReceiverName"
                onChange={(e) => {
                  setClientReceiverProfile((prevClientProfile) => ({
                    ...prevClientProfile,
                    ["Name"]: e.target.value,
                  }));
                }}
                className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
                placeholder="Receiver Client Name"
              />
            </div>
            <div className="col-span-1">
              <label className="block text-sm font-medium text-gray-700">
                Receiver Phone Number
              </label>
              <input
                required
                type="text"
                name="ReceiverPhoneNumber"
                onChange={(e) => {
                  setClientReceiverProfile((prevClientProfile) => ({
                    ...prevClientProfile,
                    ["Phones"]: [e.target.value],
                  }));
                }}
                className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
                placeholder="+359 88 456 7890"
              />
            </div>
            <div className="col-span-1">
              <label className="block text-sm font-medium text-gray-700">
                Receiver Email
              </label>
              <input
                type="email"
                required
                name="EmailOnDelivery"
                className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
                placeholder="your@email.com"
              />
            </div>

            {deliverTo === "Address" && (
              <>
                <div className="col-span-1">
                  <label className="block text-sm font-medium text-gray-700">
                    City / Village
                  </label>
                  <select
                    onChange={(event) => {
                      const selectedOption = event.target.selectedOptions[0];
                      const cityId = selectedOption.getAttribute("id");
                      const postCode =
                        selectedOption.getAttribute("cityPostCode");
                      setCityPostCode(postCode);
                      setCityId(cityId);
                    }}
                    name="CityOrVillage"
                    defaultValue={cities.length > 0 ? cities[0].nameEn : ""}
                    className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
                  >
                    {cities &&
                      cities.map((city) => (
                        <option
                          key={city.id}
                          value={city.nameEn}
                          id={city.id}
                          cityPostCode={city.postCode}
                        >
                          {`${city.nameEn}, ${city.regionNameEn}`}
                        </option>
                      ))}
                  </select>
                </div>

                <div className="col-span-1">
                  <label className="block text-sm font-medium text-gray-700">
                    Quarter
                  </label>
                  <select
                    defaultValue={quarters.length > 0 ? quarters[0].nameEn : ""}
                    name="Quarter"
                    className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
                  >
                    {cities &&
                      quarters.map((quarter) => (
                        <option key={quarter.id} value={quarter.nameEn}>
                          {`${quarter.nameEn}`}
                        </option>
                      ))}
                  </select>
                </div>

                <div className="col-span-1">
                  <label className="block text-sm font-medium text-gray-700">
                    Street
                  </label>
                  <select
                    name="Street"
                    className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
                  >
                    {cities &&
                      streets.map((street) => (
                        <option key={street.id} value={street.nameEn}>
                          {`${street.nameEn}`}
                        </option>
                      ))}
                  </select>
                </div>

                <div className="col-span-1">
                  <label className="block text-sm font-medium text-gray-700">
                    Street Number
                  </label>
                  <input
                    required
                    type="text"
                    name="StreetNumber"
                    className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
                    placeholder="Street Number"
                  />
                </div>
                <div className="col-span-1">
                  <label className="block text-sm font-medium text-gray-700">
                    Delivery Address Details Info
                  </label>
                  <input
                    required
                    type="text"
                    name="DeliverAddressInfo"
                    className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
                    placeholder="[Flat Number]/[Level/Floor Number]/[Block Number]/[Building/Complex Name]"
                  />
                </div>
              </>
            )}
            <div className="col-span-1">
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
            <div className="col-span-1">
              <label className="block text-sm font-medium text-gray-700">
                Incoming Product Receipt
              </label>
              <input
                type="checkbox"
                name="GoodsReceipt"
                value={"false"}
                onChange={handleChangeShippingLabelServices}
                className="mt-1 block"
              />
            </div>
            <div className="col-span-1">
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

            <div className="col-span-3">
              <div className="flex justify-center mt-4">
                <button
                  disabled={!agree}
                  type="submit"
                  className="py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
                >
                  Submit
                </button>
              </div>
              <div className="flex justify-center mt-4">
                <label className="flex items-center text-sm font-medium text-gray-700">
                  <input
                    onChange={handleCheckboxChange}
                    type="checkbox"
                    className="mr-2 h-4 w-4 text-indigo-600 border-gray-300 rounded focus:ring-indigo-500"
                  />
                  I agree my personal information to be stored and processed by
                  you. For more details why we need your permision click
                  <a
                    target="_blank"
                    rel="noreferrer"
                    className="pl-1 text-blue-700"
                    href="https://gdpr-info.eu/"
                  >
                    here.
                  </a>
                </label>
              </div>
            </div>
          </form>
        </div>

        <div
          className={`flex-grow w-full flex items-center justify-center ${
            deliverTo === "Office" ? "block" : "hidden"
          }`}
        >
          <div className=" w-4/5 h-4/5">
            <ReactIframe
              className="w-full h-full"
              title="Econt Office Locator"
              allow="geolocation;"
              src="https://staging.officelocator.econt.com?shopUrl=https://example.staging.officelocator.econt.com&officeType=office&lang=en"
            ></ReactIframe>
          </div>
        </div>
      </div>
    </>
  );
}

export default RequestDelivery;
