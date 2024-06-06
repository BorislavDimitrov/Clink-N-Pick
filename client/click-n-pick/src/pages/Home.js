import { Link, useNavigate } from "react-router-dom";
import { GetAuthToken } from "../Utility/auth";
import ReactIframe from "react-iframe";

function Home() {
  const navigate = useNavigate();

  function navigateHandler() {
    navigate("/products");
  }

  return (
    <>
      <div class="max-w-6xl px-4 py-10 sm:px-6 lg:px-8 lg:py-14 mx-auto">
        <div class="grid sm:grid-cols-12 gap-6">
          <div class="sm:self-end col-span-12 sm:col-span-7 md:col-span-8 lg:col-span-5 lg:col-start-3">
            <div class="group relative block rounded-xl overflow-hidden">
              <div class="aspect-w-12 aspect-h-7 sm:aspect-none rounded-xl overflow-hidden">
                <img
                  class="group-hover:scale-105 transition-transform duration-500 ease-in-out rounded-xl w-full object-cover"
                  src="https://images.pexels.com/photos/4158/apple-iphone-smartphone-desk.jpg?auto=compress&cs=tinysrgb&w=600"
                  alt="Electronics"
                />
              </div>
              <div class="absolute bottom-0 start-0 end-0 p-2 sm:p-4">
                <div class="text-sm font-bold text-gray-800 rounded-lg bg-white p-4 md:text-xl dark:bg-neutral-800 dark:text-neutral-200">
                  Electronics
                </div>
              </div>
            </div>
          </div>

          <div class="sm:self-end col-span-12 sm:col-span-5 md:col-span-4 lg:col-span-3">
            <div
              class="group relative block rounded-xl overflow-hidden"
              href="#"
            >
              <div class="aspect-w-12 aspect-h-7 sm:aspect-none rounded-xl overflow-hidden">
                <img
                  class="group-hover:scale-105 transition-transform duration-500 ease-in-out rounded-xl w-full object-cover"
                  src="https://airtasker-seo-assets-prod.s3.amazonaws.com/en_AU/1647924773686_shutterstock_1512869501.jpg"
                  alt="For the car"
                />
              </div>
              <div class="absolute bottom-0 start-0 end-0 p-2 sm:p-4">
                <div class="text-sm font-bold text-gray-800 rounded-lg bg-white p-4 md:text-xl dark:bg-neutral-800 dark:text-neutral-200">
                  For The Car
                </div>
              </div>
            </div>
          </div>

          <div class="col-span-12 md:col-span-4">
            <div class="group relative block rounded-xl overflow-hidden">
              <div class="aspect-w-12 aspect-h-7 sm:aspect-none rounded-xl overflow-hidden">
                <img
                  class="group-hover:scale-105 transition-transform duration-500 ease-in-out rounded-xl w-full object-cover"
                  src="https://images.pexels.com/photos/3621104/pexels-photo-3621104.jpeg?auto=compress&cs=tinysrgb&w=600"
                  alt="Sports equipment"
                />
              </div>
              <div class="absolute bottom-0 start-0 end-0 p-2 sm:p-4">
                <div class="text-sm font-bold text-gray-800 rounded-lg bg-white p-4 md:text-xl dark:bg-neutral-800 dark:text-neutral-200">
                  Sports Equipment
                </div>
              </div>
            </div>
          </div>

          <div class="col-span-12 sm:col-span-6 md:col-span-4">
            <div class="group relative block rounded-xl overflow-hidden">
              <div class="aspect-w-12 aspect-h-7 sm:aspect-none rounded-xl overflow-hidden">
                <img
                  class="group-hover:scale-105 transition-transform duration-500 ease-in-out rounded-xl w-full object-cover"
                  src="https://images.pexels.com/photos/2079293/pexels-photo-2079293.jpeg?auto=compress&cs=tinysrgb&w=600"
                  alt="Furniture"
                />
              </div>
              <div class="absolute bottom-0 start-0 end-0 p-2 sm:p-4">
                <div class="text-sm font-bold text-gray-800 rounded-lg bg-white p-4 md:text-xl dark:bg-neutral-800 dark:text-neutral-200">
                  Furniture
                </div>
              </div>
            </div>
          </div>

          <div class="col-span-12 sm:col-span-6 md:col-span-4">
            <div class="group relative block rounded-xl overflow-hidden">
              <div class="aspect-w-12 aspect-h-7 sm:aspect-none rounded-xl overflow-hidden">
                <img
                  class="group-hover:scale-105 transition-transform duration-500 ease-in-out rounded-xl w-full object-cover"
                  src="https://images.pexels.com/photos/7718841/pexels-photo-7718841.jpeg?auto=compress&cs=tinysrgb&w=600"
                  alt="office supplies"
                />
              </div>
              <div class="absolute bottom-0 start-0 end-0 p-2 sm:p-4">
                <div class="text-sm font-bold text-gray-800 rounded-lg bg-white p-4 md:text-xl dark:bg-neutral-800 dark:text-neutral-200">
                  Office supplies
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <body>
        <h2 class="mb-8 lg:mb-16 text-3xl font-extrabold tracking-tight leading-tight text-center text-gray-900 dark:text-white md:text-4xl">
          And many other cateoreis
        </h2>
      </body>
    </>
  );
}

export default Home;
