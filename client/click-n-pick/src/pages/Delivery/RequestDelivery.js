import ReactIframe from "react-iframe";
import { useState, useEffect, useRef } from "react";
import { useParams } from "react-router-dom";
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
  const params = useParams();

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
    console.log(event.data);
    setAddress(event.data);
  }

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

    var response = await requestShipment(inputInfo);
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

  return (
    <>
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
                  setClientReceiverProfile((prevClientProfile) => ({
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
                  setClientReceiverProfile((prevClientProfile) => ({
                    ...prevClientProfile,
                    ["Phones"]: [e.target.value],
                  }));
                }}
                className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
                placeholder="Receiver Phone"
              />
            </div>
            {deliverTo === "Address" && (
              <>
                <div>
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

                <div>
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

                <div>
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

                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    Street Number
                  </label>
                  <input
                    required
                    type="text"
                    name="StreetNumber"
                    className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
                    placeholder="Additional Info"
                  />
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    Deliver Address Info
                  </label>
                  <input
                    required
                    type="text"
                    name="DeliverAddressInfo"
                    className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
                    placeholder="Additional Info"
                  />
                </div>
              </>
            )}
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
            <div className="flex justify-center mt-4">
              <button
                type="submit"
                className="w-full md:w-auto flex justify-center py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
              >
                Submit
              </button>
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
