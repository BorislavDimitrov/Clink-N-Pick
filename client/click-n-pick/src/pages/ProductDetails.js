import { useParams } from "react-router-dom";
import { useEffect } from "react";
import { Link } from "react-router-dom";
import Carousel from "../components/Carousel";
import "tippy.js/dist/tippy.css";
import { useState } from "react";
import { details } from "../fetch/requests/products";
import Comments from "../components/Comments";

function ProductDetail() {
  const params = useParams();

  const [product, setProduct] = useState(null);

  console.log();
  useEffect(() => {
    (async function getProductDetails() {
      try {
        const response = await details(params.id);
        console.log();
        if (response.status !== 200) {
          throw new Error("Fetching details failed.");
        }

        const data = await response.json();

        console.log(data);
        setProduct(data);
      } catch (error) {
        alert("Some problem occurred.");
      }
    })();
  }, []);

  return (
    <>
      <div className="font-[sans-serif] bg-white">
        <div className="p-5 lg:max-w-7xl max-w-4xl mx-auto">
          <div className="grid items-start grid-cols-1 lg:grid-cols-5 gap-12 shadow-[0_2px_10px_-3px_rgba(6,81,237,0.3)] p-6">
            <div className="lg:col-span-3 w-full lg:sticky top-0 text-center">
              <div className="px-4 rounded-xl shadow-[0_2px_10px_-3px_rgba(6,81,237,0.3)] relative">
                {product && <Carousel slides={product.imageUrls} />}
              </div>
              <div className="mt-6 flex flex-wrap justify-center gap-6 mx-auto"></div>
            </div>
            <div className="lg:col-span-2">
              <h2 className="text-2xl font-extrabold text-[#333]">
                {product && product.title}
              </h2>
              {product && product.isOnDiscount && (
                <div className="flex flex-wrap gap-4 mt-6">
                  <p className="text-[#333] text-4xl font-bold">
                    $ {product.discountPrice}
                  </p>
                  <p className="font-extrabold text-xl text-red-500">
                    <strike>${product.price}</strike>
                  </p>
                </div>
              )}
              {product && product.isOnDiscount === false && (
                <div className="flex flex-wrap gap-4 mt-6">
                  <p className="text-[#333] text-4xl font-bold">
                    $ {product.price}
                  </p>
                </div>
              )}

              <p className="text-gray-500 dark:text-gray-400 py-10 max-w-full overflow-hidden text-ellipsis whitespace-normal break-words">
                {product !== null && product.description}
              </p>

              <a href={`http://localhost:3000/Delivery/Request/${params.id}`}>
                <div className="flex flex-wrap gap-4 mt-10  justify-center">
                  <button
                    type="button"
                    className="min-w-[200px] px-4 py-3 bg-[#333] hover:bg-[#111] text-white text-sm font-bold rounded"
                  >
                    Request Delivery
                  </button>
                </div>
              </a>

              <div class="mt-10 mx-auto">
                <div class="p-5 border rounded text-center text-gray-500 max-w-sm">
                  <img
                    class="w-32 h-32 rounded-full mx-auto"
                    src={product && product.creatorImageUrl}
                    alt="profile"
                  />
                  <div class="text-sm mt-5">
                    <a
                      href={`/users/profile/${product && product.creatorId}`}
                      class="font-medium leading-none text-gray-900 hover:text-indigo-600 transition duration-500 ease-in-out"
                    >
                      {product && product.creatorUsername}
                    </a>
                  </div>

                  <p class="mt-2 text-sm text-gray-900">
                    {product && product.creatorEmail}
                  </p>
                  <p class="mt-2 text-sm text-gray-900">
                    {product && product.creatorPhoneNumber}
                  </p>

                  <div class="flex mt-4 justify-center"></div>
                </div>
              </div>
            </div>
          </div>
          <div className="shadow-[0_2px_10px_-3px_rgba(6,81,237,0.3)] mt-3">
            <Comments productId={params.id} />

            {/* <div class="antialiased mx-auto max-w-screen-sm">
              <h3 class="mb-4 text-lg font-semibold text-gray-900">Comments</h3>

              <div class="space-y-4">
                <div class="flex">
                  <div class="flex-shrink-0 mr-3">
                    <img
                      class="mt-2 rounded-full w-8 h-8 sm:w-10 sm:h-10"
                      src="https://images.unsplash.com/photo-1604426633861-11b2faead63c?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=200&h=200&q=80"
                      alt=""
                    />
                  </div>
                  <div class="flex-1 border rounded-lg px-4 py-2 sm:px-6 sm:py-4 leading-relaxed">
                    <strong>Sarah</strong>{" "}
                    <span class="text-xs text-gray-400">3:34 PM</span>
                    <p class="text-sm">
                      Lorem ipsum dolor sit amet, consetetur sadipscing elitr,
                      sed diam nonumy eirmod tempor invidunt ut labore et dolore
                      magna aliquyam erat, sed diam voluptua.
                    </p>
                    <div class="mt-4 flex items-center">
                      <div class="flex -space-x-2 mr-2">
                        <img
                          class="rounded-full w-6 h-6 border border-white"
                          src="https://images.unsplash.com/photo-1554151228-14d9def656e4?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=100&h=100&q=80"
                          alt=""
                        />
                        <img
                          class="rounded-full w-6 h-6 border border-white"
                          src="https://images.unsplash.com/photo-1513956589380-bad6acb9b9d4?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=100&h=100&q=80"
                          alt=""
                        />
                      </div>
                      <div class="text-sm text-gray-500 font-semibold">
                        5 Replies
                      </div>
                    </div>
                  </div>
                </div>

                <div class="flex">
                  <div class="flex-shrink-0 mr-3">
                    <img
                      class="mt-2 rounded-full w-8 h-8 sm:w-10 sm:h-10"
                      src="https://images.unsplash.com/photo-1604426633861-11b2faead63c?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=200&h=200&q=80"
                      alt=""
                    />
                  </div>
                  <div class="flex-1 border rounded-lg px-4 py-2 sm:px-6 sm:py-4 leading-relaxed">
                    <strong>Sarah</strong>{" "}
                    <span class="text-xs text-gray-400">3:34 PM</span>
                    <p class="text-sm">
                      Lorem ipsum dolor sit amet, consetetur sadipscing elitr,
                      sed diam nonumy eirmod tempor invidunt ut labore et dolore
                      magna aliquyam erat, sed diam voluptua.
                    </p>
                    <h4 class="my-5 uppercase tracking-wide text-gray-400 font-bold text-xs">
                      Replies
                    </h4>
                    <div class="space-y-4">
                      <div class="flex">
                        <div class="flex-shrink-0 mr-3">
                          <img
                            class="mt-3 rounded-full w-6 h-6 sm:w-8 sm:h-8"
                            src="https://images.unsplash.com/photo-1604426633861-11b2faead63c?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=200&h=200&q=80"
                            alt=""
                          />
                        </div>
                        <div class="flex-1 bg-gray-100 rounded-lg px-4 py-2 sm:px-6 sm:py-4 leading-relaxed">
                          <strong>Sarah</strong>{" "}
                          <span class="text-xs text-gray-400">3:34 PM</span>
                          <p class="text-xs sm:text-sm">
                            Lorem ipsum dolor sit amet, consetetur sadipscing
                            elitr, sed diam nonumy eirmod tempor invidunt ut
                            labore et dolore magna aliquyam erat, sed diam
                            voluptua.
                          </p>
                        </div>
                      </div>
                      <div class="flex">
                        <div class="flex-shrink-0 mr-3">
                          <img
                            class="mt-3 rounded-full w-6 h-6 sm:w-8 sm:h-8"
                            src="https://images.unsplash.com/photo-1604426633861-11b2faead63c?ixid=MXwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHw%3D&ixlib=rb-1.2.1&auto=format&fit=crop&w=200&h=200&q=80"
                            alt=""
                          />
                        </div>
                        <div class="flex-1 bg-gray-100 rounded-lg px-4 py-2 sm:px-6 sm:py-4 leading-relaxed">
                          <strong>Sarah</strong>{" "}
                          <span class="text-xs text-gray-400">3:34 PM</span>
                          <p class="text-xs sm:text-sm">
                            Lorem ipsum dolor sit amet, consetetur sadipscing
                            elitr, sed diam nonumy eirmod tempor invidunt ut
                            labore et dolore magna aliquyam erat, sed diam
                            voluptua.
                          </p>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div> */}
          </div>
        </div>
      </div>
    </>
  );
}

export default ProductDetail;
