import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";

import { getAll } from "../fetch/requests/promotions";

function Promotion() {
  const params = useParams();
  const [promotions, setPromotions] = useState([]);

  useEffect(() => {
    (async function getPromotionsInfo() {
      try {
        const response = await getAll();

        if (response.status !== 200) {
          throw new Error("Network response was not ok");
        }

        var data = await response.json();
        setPromotions(data.promotions);
      } catch (error) {
        alert("Some problem occurred.");
      }
    })();
  }, []);

  return (
    <>
      <div className="bg-white dark:bg-gray-800 pt-20 " id="pricing">
        <div className="relative mx-auto max-w-7xl px-6 text-center lg:px-8 mb-10">
          <div className="mx-auto max-w-2xl lg:max-w-4xl">
            <p className="mt-2 text-4xl font-bold tracking-tight text-gray-900 dark:text-gray-200">
              Find the right promotion for your product
            </p>
          </div>
        </div>
        <div className="mx-auto max-w-7xl px-6 lg:px-8 mb-6 mt-20"></div>
        <div className="">
          <div className="relative z-10 mx-auto max-w-7xl px-6 lg:px-8">
            <div className="mx-auto grid max-w-md grid-cols-1 gap-8 lg:max-w-7xl lg:grid-cols-3 lg:gap-8">
              {promotions &&
                promotions.map((promotion) => (
                  <div className="flex flex-col rounded-3xl bg-white dark:bg-gray-900 shadow-xl ring-1 ring-black/10">
                    <h2 className="flex flex-row justify-center align-middle text-center mt-7 text-xl font-bold">
                      {promotion.name}
                    </h2>
                    <div className="p-8 sm:p-10 flex flex-row justify-center align-middle text-center">
                      <div className=" flex items-baseline text-5xl tracking-tight text-gray-900 dark:text-gray-200 font-semibold">
                        €{promotion.price}
                        <span className="text-lg font-semibold leading-8 tracking-normal text-gray-500 dark:text-gray-400">
                          /{promotion.durationDays} days
                        </span>
                      </div>
                    </div>
                    <div className="flex flex-1 flex-col">
                      <div className="flex flex-1 flex-col justify-between rounded-2xl bg-gray-50 dark:bg-gray-700 p-6 sm:p-8">
                        <ul role="list" className="space-y-6">
                          <li className="">
                            <p className="flex ml-3 leading-6 text-gray-600 dark:text-gray-300 text-center align-middle justify-center text-lg">
                              {promotion.pricePerDay} € / day
                            </p>
                          </li>
                        </ul>
                        <div className="mt-8">
                          <a
                            href={`/Payment/${params.id}/${promotion.id}`}
                            className="inline-block w-full rounded-lg bg-teal-600 dark:bg-teal-400 px-4 py-2.5 text-center text-sm font-semibold leading-5 text-white shadow-md hover:bg-teal-700 dark:hover:bg-teal-500 cursor-pointer"
                            aria-describedby="tier-plus"
                          >
                            Proceed with payment
                          </a>
                        </div>
                      </div>
                    </div>
                  </div>
                ))}
            </div>
          </div>
        </div>
      </div>
    </>
  );
}

export default Promotion;
