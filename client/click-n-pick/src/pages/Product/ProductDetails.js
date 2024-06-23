import { useParams } from "react-router-dom";
import { useEffect } from "react";
import "tippy.js/dist/tippy.css";
import { useState } from "react";

import Carousel from "../../components/Carousel";
import { productDetails } from "../../fetch/requests/products";
import Comments from "../../components/Comments";
import { GetCurrentUserId, GetAuthToken } from "../../Utility/auth";

function ProductDetail() {
  const params = useParams();
  const token = GetAuthToken();

  const [product, setProduct] = useState(null);
  var currentUserId = GetCurrentUserId();

  useEffect(() => {
    (async function getProductDetails() {
      try {
        const response = await productDetails(params.id);

        if (response.status !== 200) {
          throw new Error("Network response was not ok");
        }

        const data = await response.json();
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
          <div className="grid items-start grid-cols-1 lg:grid-cols-5 gap-12 rounded-lg border-4 shadow-lg p-6">
            <div className="lg:col-span-3 w-full lg:sticky top-0 text-center">
              <div className="px-4 rounded-xl border-4 shadow-lg relative">
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
                    € {product.discountPrice}
                  </p>
                  <p className="font-extrabold text-xl text-red-500">
                    <strike>€{product.price}</strike>
                  </p>
                </div>
              )}
              {product && product.isOnDiscount === false && (
                <div className="flex flex-wrap gap-4 mt-6">
                  <p className="text-[#333] text-4xl font-bold">
                    € {product.price}
                  </p>
                </div>
              )}

              <p className="text-gray-500 dark:text-gray-400 py-10 max-w-full overflow-hidden text-ellipsis whitespace-normal break-words">
                {product !== null && product.description}
              </p>

              {product && token && product.creatorId !== currentUserId && (
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
              )}

              <div className="mt-10 mx-auto">
                <div className="p-5 border text-center text-gray-900 rounded-lg shadow-lg  max-w-sm">
                  <img
                    className="w-32 h-32 rounded-full mx-auto"
                    src={product && product.creatorImageUrl}
                    alt="profile"
                  />
                  <div className="text-sm mt-5">
                    <a
                      href={`/users/profile/${product && product.creatorId}`}
                      className=" text-lg leading-none font-bold text-gray-900 hover:text-indigo-600 transition duration-500 ease-in-out"
                    >
                      {product && product.creatorUsername}
                    </a>
                  </div>

                  <p className="mt-2 text-sm text-gray-900">
                    {product && product.creatorEmail}
                  </p>
                  <p className="mt-2 text-sm text-gray-900">
                    {product && product.creatorPhoneNumber}
                  </p>

                  <div className="flex mt-4 justify-center"></div>
                </div>
              </div>
            </div>
          </div>
          <div className="rounded-lg shadow-lg border-2 mt-3 pt-10 pb-10">
            <Comments productId={params.id} currentUserId={currentUserId} />
          </div>
        </div>
      </div>
    </>
  );
}

export default ProductDetail;
