import Buttons from "./Buttons";

function ProductCard({ props, renderButtons }) {
  console.log(props);
  return (
    <div
      className={`relative w-72 bg-white shadow-md rounded-xl duration-500 hover:scale-105 hover:shadow-xl ${
        props.isPromoted ? "border-4 border-yellow-500" : ""
      }`}
    >
      {props.isPromoted && (
        <div className="absolute top-0 right-0 bg-yellow-500 text-white text-xs font-bold px-2 py-1 rounded-bl-xl">
          Promoted
        </div>
      )}
      <a href={`/products/details/${props.id}`}>
        <img
          src={props.imageUrl}
          alt="Product"
          className="h-80 w-72 object-cover rounded-t-xl"
        />
      </a>
      <div className="px-4 py-3 w-72">
        <span className="text-gray-500 mr-3 uppercase text-xs">
          {props.creatorName}
        </span>
        <span className="text-gray-500 mr-3 uppercase text-xs">
          {props.categoryName}
        </span>
        <p className="text-lg font-bold text-black truncate block capitalize">
          {props.title}
        </p>
        {props.isOnDiscount === true ? (
          <div className="flex items-center justify-center text-center align-middle">
            <p className="text-xl text-black cursor-auto my-3 font-bold align-middle text-center flex flex-col justify-center">
              ${props.discountPrice}
            </p>
            {props.isOnDiscount && (
              <del>
                <p className="text-sm text-red-600 font-bold cursor-auto ml-2">
                  {props.price}
                </p>
              </del>
            )}
          </div>
        ) : (
          <div className="flex items-center">
            <p className="text-xl text-black cursor-auto my-3 font-bold">
              €{props.price}
            </p>
          </div>
        )}
      </div>
      {renderButtons && <Buttons id={props.id} isPromoted={props.isPromoted} />}
    </div>
  );
}

export default ProductCard;
