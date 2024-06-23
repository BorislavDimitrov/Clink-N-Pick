function Home() {
  return (
    <>
      <body>
        <div className="bg-slate-100 shadow-md py-12 mt-10">
          <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
            <div className="lg:text-center">
              <h2 className=" text-3xl text-blue-700 font-semibold tracking-wide uppercase">
                Welcome to Click N Pick
              </h2>
              <p className="mt-2 text-3xl leading-8 font-extrabold tracking-tight text-gray-900 sm:text-4xl">
                Your Marketplace for Product Listings and Shipments
              </p>
              <p className="mt-4 max-w-2xl text-xl text-gray-500 lg:mx-auto">
                Create ads for your products and reach out to buyers who can
                request shipments effortlessly.
              </p>
            </div>

            <div className="mt-10">
              <dl className="space-y-10 md:space-y-0 md:grid md:grid-cols-2 md:gap-x-8 md:gap-y-10">
                <div className="relative">
                  <dt>
                    <div className="absolute flex items-center justify-center h-12 w-12 rounded-md bg-indigo-500 text-white">
                      <svg
                        className="h-6 w-6"
                        xmlns="http://www.w3.org/2000/svg"
                        fill="none"
                        viewBox="0 0 24 24"
                        stroke="currentColor"
                        aria-hidden="true"
                      >
                        <path
                          stroke-linecap="round"
                          stroke-linejoin="round"
                          stroke-width="2"
                          d="M11 5H7m4 0h.01M12 4v1m0 0v2m0-2h2.5M10 5v1m0 0v2m0-2H7m2.5-1h.01M17 16h.01M7 20h.01M7 16h.01M7 12h.01M17 12h.01M17 20h.01M3 3h.01M3 7h.01M3 11h.01M21 3h.01M21 7h.01M21 11h.01M3 15h.01M3 19h.01M3 23h.01M21 15h.01M21 19h.01M21 23h.01M12 8v8m0 0h2m-2 0H9"
                        />
                      </svg>
                    </div>
                    <p className="ml-16 text-lg leading-6 font-medium text-gray-900">
                      Create Ads
                    </p>
                  </dt>
                  <dd className="mt-2 ml-16 text-base text-gray-500">
                    Easily create ads for your products with our simple and
                    intuitive interface.
                  </dd>
                </div>

                <div className="relative">
                  <dt>
                    <div className="absolute flex items-center justify-center h-12 w-12 rounded-md bg-indigo-500 text-white">
                      <svg
                        className="h-6 w-6"
                        xmlns="http://www.w3.org/2000/svg"
                        fill="none"
                        viewBox="0 0 24 24"
                        stroke="currentColor"
                        aria-hidden="true"
                      >
                        <path
                          stroke-linecap="round"
                          stroke-linejoin="round"
                          stroke-width="2"
                          d="M15 12l-3-3m0 0l-3 3m3-3v12m9-4a9 9 0 11-18 0 9 9 0 0118 0z"
                        />
                      </svg>
                    </div>
                    <p className="ml-16 text-lg leading-6 font-medium text-gray-900">
                      View Listings
                    </p>
                  </dt>
                  <dd className="mt-2 ml-16 text-base text-gray-500">
                    Browse through a variety of product ads posted by users
                    across different categories.
                  </dd>
                </div>

                <div className="relative">
                  <dt>
                    <div className="absolute flex items-center justify-center h-12 w-12 rounded-md bg-indigo-500 text-white">
                      <svg
                        className="h-6 w-6"
                        xmlns="http://www.w3.org/2000/svg"
                        fill="none"
                        viewBox="0 0 24 24"
                        stroke="currentColor"
                        aria-hidden="true"
                      >
                        <path
                          stroke-linecap="round"
                          stroke-linejoin="round"
                          stroke-width="2"
                          d="M3 3h2l.31 2M7 13h10l1.38-5H6.6M16 18a2 2 0 100 4 2 2 0 000-4zM6 18a2 2 0 100 4 2 2 0 000-4z"
                        />
                      </svg>
                    </div>
                    <p className="ml-16 text-lg leading-6 font-medium text-gray-900">
                      Request Shipments
                    </p>
                  </dt>
                  <dd className="mt-2 ml-16 text-base text-gray-500">
                    Easily request shipments for products you are interested in,
                    right from the ad page.
                  </dd>
                </div>

                <div className="relative">
                  <dt>
                    <div className="absolute flex items-center justify-center h-12 w-12 rounded-md bg-indigo-500 text-white">
                      <svg
                        className="h-6 w-6"
                        xmlns="http://www.w3.org/2000/svg"
                        fill="none"
                        viewBox="0 0 24 24"
                        stroke="currentColor"
                        aria-hidden="true"
                      >
                        <path
                          stroke-linecap="round"
                          stroke-linejoin="round"
                          stroke-width="2"
                          d="M12 2l9 4.5-9 4.5-9-4.5L12 2zM2 12l9 4.5L20 12m-8 9l-9-4.5L12 12l9 4.5L12 21z"
                        />
                      </svg>
                    </div>
                    <p className="ml-16 text-lg leading-6 font-medium text-gray-900">
                      Secure Transactions
                    </p>
                  </dt>
                  <dd className="mt-2 ml-16 text-base text-gray-500">
                    Enjoy a secure and reliable platform for listing and
                    requesting product shipments.
                  </dd>
                </div>
              </dl>
            </div>
          </div>
        </div>

        <div className="max-w-6xl px-4 py-10 sm:px-6 lg:px-8 lg:py-14 mx-auto mt-6 ">
          <div className="grid sm:grid-cols-12 gap-6">
            <div className="sm:self-end col-span-12 sm:col-span-7 md:col-span-8 lg:col-span-5 lg:col-start-3">
              <div className="group relative block rounded-xl overflow-hidden">
                <div className="aspect-w-12 aspect-h-7 sm:aspect-none rounded-xl overflow-hidden">
                  <img
                    className="group-hover:scale-105 transition-transform duration-500 ease-in-out rounded-xl w-full object-cover"
                    src="https://res.cloudinary.com/dtaqyp4b6/image/upload/v1718744583/apple-iphone-smartphone-desk_slmein.jpg"
                    alt="Electronics"
                  />
                </div>
                <div className="absolute bottom-0 start-0 end-0 p-2 sm:p-4">
                  <div className="text-sm font-bold text-gray-800 rounded-lg bg-white p-4 md:text-xl dark:bg-neutral-800 dark:text-neutral-200">
                    Electronics
                  </div>
                </div>
              </div>
            </div>

            <div className="sm:self-end col-span-12 sm:col-span-5 md:col-span-4 lg:col-span-3">
              <div
                className="group relative block rounded-xl overflow-hidden"
                href="#"
              >
                <div className="aspect-w-12 aspect-h-7 sm:aspect-none rounded-xl overflow-hidden">
                  <img
                    className="group-hover:scale-105 transition-transform duration-500 ease-in-out rounded-xl w-full object-cover"
                    src="https://res.cloudinary.com/dtaqyp4b6/image/upload/v1718744618/1647924773686_shutterstock_1512869501_qacuol.jpg"
                    alt="For the car"
                  />
                </div>
                <div className="absolutebottom-0 start-0 end-0 p-2 sm:p-4">
                  <div className="text-sm font-bold text-gray-800 rounded-lg bg-white p-4 md:text-xl dark:bg-neutral-800 dark:text-neutral-200">
                    For The Car
                  </div>
                </div>
              </div>
            </div>

            <div className="col-span-12 md:col-span-4">
              <div className="group relative block rounded-xl overflow-hidden">
                <div className="aspect-w-12 aspect-h-7 sm:aspect-none rounded-xl overflow-hidden">
                  <img
                    className="group-hover:scale-105 transition-transform duration-500 ease-in-out rounded-xl w-full object-cover"
                    src="https://res.cloudinary.com/dtaqyp4b6/image/upload/v1718744645/pexels-photo-3621104_hffwvp.jpg"
                    alt="Sports equipment"
                  />
                </div>
                <div className="absolute bottom-0 start-0 end-0 p-2 sm:p-4">
                  <div className="text-sm font-bold text-gray-800 rounded-lg bg-white p-4 md:text-xl dark:bg-neutral-800 dark:text-neutral-200">
                    Sports Equipment
                  </div>
                </div>
              </div>
            </div>

            <div className="col-span-12 sm:col-span-6 md:col-span-4">
              <div className="group relative block rounded-xl overflow-hidden">
                <div className="aspect-w-12 aspect-h-7 sm:aspect-none rounded-xl overflow-hidden">
                  <img
                    className="group-hover:scale-105 transition-transform duration-500 ease-in-out rounded-xl w-full object-cover"
                    src="https://res.cloudinary.com/dtaqyp4b6/image/upload/v1718744660/pexels-photo-2079293_c8kkhu.jpg"
                    alt="Furniture"
                  />
                </div>
                <div className="absolute bottom-0 start-0 end-0 p-2 sm:p-4">
                  <div className="text-sm font-bold text-gray-800 rounded-lg bg-white p-4 md:text-xl dark:bg-neutral-800 dark:text-neutral-200">
                    Furniture
                  </div>
                </div>
              </div>
            </div>

            <div className="col-span-12 sm:col-span-6 md:col-span-4">
              <div className="group relative block rounded-xl overflow-hidden">
                <div className="aspect-w-12 aspect-h-7 sm:aspect-none rounded-xl overflow-hidden">
                  <img
                    className="group-hover:scale-105 transition-transform duration-500 ease-in-out rounded-xl w-full object-cover"
                    src="https://res.cloudinary.com/dtaqyp4b6/image/upload/v1718744676/pexels-photo-7718841_orbbr5.jpg"
                    alt="office supplies"
                  />
                </div>
                <div className="absolute bottom-0 start-0 end-0 p-2 sm:p-4">
                  <div className="text-sm font-bold text-gray-800 rounded-lg bg-white p-4 md:text-xl dark:bg-neutral-800 dark:text-neutral-200">
                    Office supplies
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <h2 className="lg:mb-8 text-2xl font-extrabold tracking-tight leading-tight text-center text-gray-900 dark:text-white md:text-3xl">
          And many other cateoreis
        </h2>
        <div className=" py-12 bg-slate-100 shadow-md mb-10">
          <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
            <div className="lg:text-center">
              <h2 className="text-xl text-blue-600 font-semibold tracking-wide uppercase">
                Promote Your Ads
              </h2>
              <p className="mt-2 text-3xl leading-8 font-extrabold tracking-tight text-gray-900 sm:text-4xl">
                Get More Visibility with Promoted Ads
              </p>
              <p className="mt-4 max-w-2xl text-xl text-gray-500 lg:mx-auto">
                After posting your product ad, you can choose to promote it.
                Promoted ads are shown on top of other non-promoted ads,
                increasing your visibility and chances of a quick sale.
              </p>
            </div>

            <div className="mt-10">
              <dl className="space-y-10 md:space-y-0 md:grid md:grid-cols-2 md:gap-x-8 md:gap-y-10">
                <div className="relative">
                  <dt>
                    <div className="absolute flex items-center justify-center h-12 w-12 rounded-md bg-indigo-500 text-white">
                      <svg
                        className="h-6 w-6"
                        xmlns="http://www.w3.org/2000/svg"
                        fill="none"
                        viewBox="0 0 24 24"
                        stroke="currentColor"
                        aria-hidden="true"
                      >
                        <path
                          stroke-linecap="round"
                          stroke-linejoin="round"
                          stroke-width="2"
                          d="M13 10V3L4 14h7v7l9-11h-7z"
                        />
                      </svg>
                    </div>
                    <p className="ml-16 text-lg leading-6 font-medium text-gray-900">
                      Boost Your Ad
                    </p>
                  </dt>
                  <dd className="mt-2 ml-16 text-base text-gray-500">
                    Pay a small fee to boost your ad and have it displayed
                    prominently on top of search results and category listings.
                  </dd>
                </div>

                <div className="relative">
                  <dt>
                    <div className="absolute flex items-center justify-center h-12 w-12 rounded-md bg-indigo-500 text-white">
                      <svg
                        className="h-6 w-6"
                        xmlns="http://www.w3.org/2000/svg"
                        fill="none"
                        viewBox="0 0 24 24"
                        stroke="currentColor"
                        aria-hidden="true"
                      >
                        <path
                          stroke-linecap="round"
                          stroke-linejoin="round"
                          stroke-width="2"
                          d="M3 17l6-6 4 4 8-8"
                        />
                      </svg>
                    </div>
                    <p className="ml-16 text-lg leading-6 font-medium text-gray-900">
                      Increase Visibility
                    </p>
                  </dt>
                  <dd className="mt-2 ml-16 text-base text-gray-500">
                    Promoted ads are highlighted and appear at the top of
                    relevant pages, ensuring they catch the eye of potential
                    buyers first.
                  </dd>
                </div>

                <div className="relative">
                  <dt>
                    <div className="absolute flex items-center justify-center h-12 w-12 rounded-md bg-indigo-500 text-white">
                      <svg
                        className="h-6 w-6"
                        xmlns="http://www.w3.org/2000/svg"
                        fill="none"
                        viewBox="0 0 24 24"
                        stroke="currentColor"
                        aria-hidden="true"
                      >
                        <path
                          stroke-linecap="round"
                          stroke-linejoin="round"
                          stroke-width="2"
                          d="M12 8c-1.657 0-3 .895-3 2 0 .739.456 1.41 1.106 1.751C11.354 12.88 12 13.5 12 14c0 1.105-1.343 2-3 2m0 0V7m0 7v3m0 3c-1.657 0-3-.895-3-2 0-.739.456-1.41 1.106-1.751C11.354 14.88 12 14.26 12 13.5c0-1.105-1.343-2-3-2m0 0V4m0 2V3"
                        />
                      </svg>
                    </div>
                    <p className="ml-16 text-lg leading-6 font-medium text-gray-900">
                      Affordable Pricing
                    </p>
                  </dt>
                  <dd className="mt-2 ml-16 text-base text-gray-500">
                    Our promotion packages are competitively priced to provide
                    great value and maximize your return on investment.
                  </dd>
                </div>
                <div className="relative">
                  <dt>
                    <div className="absolute flex items-center justify-center h-12 w-12 rounded-md bg-indigo-500 text-white">
                      <svg
                        className="h-6 w-6"
                        xmlns="http://www.w3.org/2000/svg"
                        fill="none"
                        viewBox="0 0 24 24"
                        stroke="currentColor"
                        aria-hidden="true"
                      >
                        <path
                          stroke-linecap="round"
                          stroke-linejoin="round"
                          stroke-width="2"
                          d="M21 10c0-3.866-3.134-7-7-7S7 6.134 7 10c0 3.035 2.014 5.598 4.781 6.572-.444-.866-.781-1.806-.781-2.572 0-3.866 3.134-7 7-7s7 3.134 7 7c0 .766-.337 1.706-.781 2.572C18.986 15.598 21 13.035 21 10zm-1 0c0-3.314-2.686-6-6-6S8 6.686 8 10c0 1.201.439 2.317 1.172 3.189C10.38 14.761 11.642 15.5 13 15.5c1.357 0 2.619-.739 3.828-2.311C19.561 12.317 20 11.201 20 10zM2 10c0 2.071 1.124 3.906 2.782 5.031-.037-.256-.282-.748-.28-1.031C4.5 13.107 6 11.5 6 10s-1.5-3.107-1.498-4.5c-.002-.283.243-.775.28-1.031C3.124 6.094 2 7.929 2 10zm.999 0C4 7.134 6.134 5 9 5c2.86 0 5 2.14 5 5s-2.14 5-5 5c-2.866 0-5-2.134-5-5zM12 21c0-3.314 2.686-6 6-6s6 2.686 6 6c0 .766-.337 1.706-.781 2.572C18.986 27.598 16 25.035 16 21zM5 21c0 1.201.439 2.317 1.172 3.189C6.38 25.761 7.642 26.5 9 26.5c1.357 0 2.619-.739 3.828-2.311C14.561 23.317 15 22.201 15 21c0-3.866-3.134-7-7-7S1 17.134 1 21c0 .766.337 1.706.781 2.572C3.986 20.598 5 18.035 5 21z"
                        />
                      </svg>
                    </div>
                    <p className="ml-16 text-lg leading-6 font-medium text-gray-900">
                      Extended Reach
                    </p>
                  </dt>
                  <dd className="mt-2 ml-16 text-base text-gray-500">
                    Promoted ads reach a wider audience, ensuring more potential
                    buyers see your product.
                  </dd>
                </div>
              </dl>
            </div>
          </div>
        </div>
      </body>
    </>
  );
}

export default Home;
