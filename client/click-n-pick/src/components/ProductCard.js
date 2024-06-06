import { Link } from "react-router-dom";
import Buttons from "./Buttons";

function ProductCard({ props, renderButtons }) {
  console.log(props);
  return (
    <div class="w-72 bg-white shadow-md rounded-xl duration-500 hover:scale-105 hover:shadow-xl">
      <Link to={`/products/details/${props.id}`}>
        <img
          src={props.imageUrl}
          alt="Product"
          class=" h-80 w-72 object-fit rounded-t-xl"
        />
      </Link>
      <div class="px-4 py-3 w-72">
        <span class="text-gray-400 mr-3 uppercase text-xs">
          {props.creatorName}
        </span>
        <p class="text-lg font-bold text-black truncate block capitalize">
          {props.title}
        </p>
        {props.isOnDiscount === true ? (
          <div class="flex items-center justify-center text-center align-middle">
            <p class="text-xl text-black cursor-auto my-3 font-bold align-middle text-center flex flex-col justify-center">
              ${props.discountPrice}
            </p>
            {props.isOnDiscount && (
              <del>
                <p class="text-sm text-red-600 font-bold cursor-auto ml-2">
                  {props.price}
                </p>
              </del>
            )}
          </div>
        ) : (
          <div class="flex items-center">
            <p class="text-xl text-black cursor-auto my-3 font-bold">
              ${props.price}
            </p>
          </div>
        )}
      </div>
      {renderButtons && <Buttons id={props.id} isPromoted={props.isPromoted} />}
    </div>
  );
}

export default ProductCard;
