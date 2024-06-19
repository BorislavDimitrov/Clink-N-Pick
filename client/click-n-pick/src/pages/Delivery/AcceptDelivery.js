import ReactIframe from "react-iframe";
import { useState, useEffect, useRef } from "react";
import { useParams } from "react-router-dom";

import {
  acceptShipment,
  getCities,
  getQuarters,
  getStreets,
} from "../../fetch/requests/delivery";

function AcceptDelivery() {
  const [sendFrom, setSendFrom] = useState("Office");
  const [address, setAddress] = useState();
  const [cities, setCities] = useState([]);
  const [quarters, setQuarters] = useState([]);
  const [streets, setStreets] = useState([]);
  const [cityId, setCityId] = useState(1);
  const [cityPostCode, setCityPostCode] = useState();
  const params = useParams();

  const [clientSenderProfile, setClientSenderProfile] = useState({
    name: "",
    phones: "",
  });

  useEffect(() => {
    (async function () {
      try {
        const response = await getCities();

        if (response.status !== 200) {
          throw new Error("Network response was not ok");
        }

        var data = await response.json();

        setCities(data.cities);
        setCityId(data.cities[0].id);
      } catch (error) {
        alert("Some problem occurred.");
      }
    })();
  }, []);

  useEffect(() => {
    (async function () {
      try {
        const params = new URLSearchParams({
          cityId: cityId,
        });

        const response = await getQuarters(params);

        if (response.status !== 200) {
          throw new Error("Network response was not ok");
        }

        var data = await response.json();
        setQuarters(data.quarters);
      } catch (error) {
        alert("Some problem occurred.");
      }
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

  const formRef = useRef(null);

  async function handleSubmit(event) {
    event.preventDefault();
    const formData = new FormData(formRef.current);
    const inputInfo = {};
    formData.forEach((value, key) => {
      inputInfo[key] = value;
    });

    if (sendFrom === "Office") {
      inputInfo["SenderOfficeCode"] = address.office.code;
      inputInfo["DeliveryLocation"] = "Office";
    }

    if (sendFrom === "Address") {
      inputInfo["PostCode"] = cityPostCode;
      inputInfo["DeliveryLocation"] = "Address";
    }

    inputInfo["RequestShipmentId"] = params.id;
    inputInfo["ReceiverName"] = clientSenderProfile.Name;
    inputInfo["ReceiverPhoneNumber"] = clientSenderProfile.Phones[0];

    try {
      var response = await acceptShipment(inputInfo);

      if (response.status !== 200) {
        throw new Error("Network response was not ok");
      }
    } catch (error) {
      alert("Some problem occurred.");
    }
  }

  useEffect(() => {
    window.addEventListener("message", getAddress);

    return () => {
      window.removeEventListener("message", getAddress);
    };
  }, []);

  return (
    <>
      <div className="flex flex-col items-center h-screen">
        <div className="w-full max-w-5xl p-4 mt-20">
          <div className="flex justify-center mb-10">
            <button
              disabled={sendFrom === "Office"}
              onClick={() => setSendFrom("Office")}
              className="mx-2 py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
            >
              To Office
            </button>
            <button
              disabled={sendFrom === "Address"}
              onClick={() => setSendFrom("Address")}
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
                Sender Client Name
              </label>
              <input
                type="text"
                name="SenderName"
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
                name="SenderPhoneNumber"
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

            {sendFrom === "Office" && (
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
            )}

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
                step="0.01"
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
            {sendFrom === "Address" && (
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
                    Request Courier From Date
                  </label>
                  <input
                    type="datetime-local"
                    min={`${new Date().toISOString().slice(0, 11)}09:00`}
                    name="RequestTimeFrom"
                    className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
                  />
                </div>

                <div>
                  <label className="block text-sm font-medium text-gray-700">
                    Request Courier To Date
                  </label>
                  <input
                    type="datetime-local"
                    min={`${new Date().toISOString().slice(0, 11)}09:00`}
                    name="RequestTimeTo"
                    className="mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
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
              <button
                type="submit"
                className="w-full flex justify-center py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-indigo-600 hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500"
              >
                Submit
              </button>
            </div>
          </form>
        </div>
        <div
          className={`flex-grow w-full flex items-center justify-center ${
            sendFrom === "Office" ? "block" : "hidden"
          }`}
        >
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
    </>
  );
}

export default AcceptDelivery;
