import { useState, useEffect } from "react";
import { getAll } from "../../fetch/requests/categories";
import { viewProfile } from "../../fetch/requests/users";
import { userProducts } from "../../fetch/requests/products";
import { useParams } from "react-router-dom";
import ProductCard from "../../components/ProductCard";

function Profile() {
  function handleOnChange() {}
  const PAGE_SIZE = 9;
  const params = useParams();
  const userId = params.id;

  const [user, setUser] = useState(null);

  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(false);
  const [pageNumber, setPageNumber] = useState(1);
  const [totalPages, setTotalPages] = useState();
  const [categories, setCategories] = useState([]);

  const [search, setSearch] = useState("");
  const [categoryIds, setCategoryIds] = useState([]);
  const [minPrice, setMinPrice] = useState(0);
  const [maxPrice, setMaxPrice] = useState(0);
  const [orderBy, setOrderBy] = useState("");

  useEffect(() => {
    (async () => {
      const response = await viewProfile(userId);
      var data = await response.json();

      console.log(data.categories);

      setUser(data);
    })();
  }, []);

  useEffect(() => {
    (async function getCategories() {
      const response = await getAll();
      var data = await response.json();
      console.log(data.categories);

      setCategories(data.categories);
    })();
  }, []);

  useEffect(() => {
    fetchProducts();
  }, [pageNumber]);

  async function fetchProducts(event) {
    if (event) {
      event.preventDefault();
    }

    console.log(categoryIds);
    try {
      const params = new URLSearchParams({
        pageNumber: pageNumber,
        search: search,
        minPrice: minPrice,
        maxPrice: maxPrice,
        orderBy: orderBy,
      });

      params.append("UserId", userId);

      if (categoryIds && categoryIds.length > 0) {
        categoryIds.forEach((category) => {
          params.append("categoryIds", category);
        });
      }

      const response = await userProducts(params);

      if (response.status !== 200) {
        throw new Error("The email confirmation failed");
      }

      var data = await response.json();
      console.log(data.products);
      var products = data.products;
      console.log(products);

      setLoading(false);

      setTotalPages(Math.ceil(data.totalItems / PAGE_SIZE));
      setProducts(products);
    } catch (error) {}
  }

  if (loading) {
    return (
      <div class="text-center p-10">
        <h1 class="font-bold text-4xl mb-4">Products are fetching</h1>
      </div>
    );
  }

  function handleOnChange(e) {
    const selectedOptions = Array.from(
      e.target.selectedOptions,
      (option) => option.value
    );
    setCategoryIds(selectedOptions);
  }

  return (
    <div className="bg-gray-50 min-h-screen">
      <div className="container mx-auto my-5 p-5">
        <div className="md:flex no-wrap md:-mx-2">
          <div className="w-full md:w-3/12 md:mx-2">
            <div className="bg-white p-3 border-t-4 border-blue-500 rounded-3xl shadow-lg">
              <div className="flex flex-col items-center space-y-5 sm:flex-row sm:space-y-0 justify-center">
                {user == null ? (
                  <img alt="User" />
                ) : (
                  <img
                    className="object-cover w-40 h-40 p-1 rounded-full ring-2 ring-indigo-300 dark:ring-indigo-500"
                    src={user && user.profileImageUrl}
                    alt="Bordered avatar"
                  />
                )}
              </div>
              <h1 className="text-gray-900 font-bold text-xl leading-8 my-1">
                {user && user.username}
              </h1>

              <ul class="bg--100 text-gray-600 hover:text-gray-700 hover:shadow py-2 px-3 mt-3 divide-y rounded shadow-sm">
                <li class="flex items-center py-3">
                  <span class="font-bold">Email</span>
                  <span class="ml-auto">
                    <span class=" py-2 px-3 mt-3 divide-y font-semibold">
                      {user && user.email}
                    </span>
                  </span>
                </li>
                <li class="flex items-center py-3">
                  <span class="font-bold">Phone number</span>
                  <span class="ml-auto">
                    <span class=" py-2 px-3 mt-3 divide-y font-semibold">
                      {user && user.phoneNumber}
                    </span>
                  </span>
                </li>
                <li class="flex items-center py-3">
                  <span class="font-bold">Address</span>
                  <span class="ml-auto">
                    <span class=" py-2 px-3 mt-3 divide-y font-semibold">
                      {user && user.address}
                    </span>
                  </span>
                </li>
              </ul>
            </div>
          </div>

          <div className="w-full md:w-9/12 mx-2 border-t-4 border shadow-lg border-t-blue-500 rounded-2xl bg-blue-50 h-full">
            <div className=" p-3 shadow-sm rounded-sm ">
              <div className="flex items-center space-x-2 font-semibold text-gray-900 leading-8">
                <span className="text-blue-700">
                  <svg
                    className="h-5"
                    xmlns="http://www.w3.org/2000/svg"
                    fill="none"
                    viewBox="0 0 24 24"
                    stroke="currentColor"
                  >
                    <path
                      strokeLinecap="round"
                      strokeLinejoin="round"
                      strokeWidth="2"
                      d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z"
                    />
                  </svg>
                </span>
                <span className="tracking-wide">Bio</span>
              </div>
              <div className="">
                <p class="font-semibold p-6">{user && user.bio}</p>
              </div>
            </div>
            {/* here render */}
          </div>
        </div>
      </div>
      <>
        <form
          onSubmit={fetchProducts}
          id="filters"
          class=" max-w-screen-md mx-auto m-20 "
        >
          <div class="rounded-xl border border-gray-200 bg-white p-6 shadow-lg ">
            <p class="mt-1 text-sm"></p>
            <div class="mt-8 grid grid-cols-1 gap-6 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 ">
              <div class="flex flex-col">
                <label for="search" class="text-stone-600 text-lg font-bold">
                  Search
                </label>
                <input
                  onChange={(event) => setSearch(event.target.value)}
                  type="text"
                  id="search"
                  placeholder="search"
                  class="mt-2 block w-full rounded-md border border-gray-200 px-2 py-2 shadow-sm outline-none focus:border-blue-500 focus:ring focus:ring-blue-200 focus:ring-opacity-50"
                />
              </div>

              <div className="flex flex-col justify-center">
                <label className="block text-gray-700 text-lg font-bold">
                  Min Price
                </label>
                <input
                  onChange={(event) => setMinPrice(event.target.value)}
                  name="minPrice"
                  min="1"
                  max="100000"
                  placeholder="1"
                  type="number"
                  className="mt-2 block w-full rounded-md border border-gray-200 px-2 py-2 shadow-sm outline-none focus:border-blue-500 focus:ring focus:ring-blue-200 focus:ring-opacity-50"
                />
              </div>

              <div className="flex flex-col justify-center">
                <label className="block text-gray-700 text-lg font-bold">
                  Max Price
                </label>
                <input
                  onChange={(event) => setMaxPrice(event.target.value)}
                  name="maxPrice"
                  placeholder="100 000"
                  min="1"
                  max="100000"
                  type="number"
                  className="mt-2 block w-full rounded-md border border-gray-200 px-2 py-2 shadow-sm outline-none focus:border-blue-500 focus:ring focus:ring-blue-200 focus:ring-opacity-50"
                />
              </div>

              <div class="flex flex-col">
                <label for="status" class="text-stone-600 text-lg font-bold">
                  Order by
                </label>

                <select
                  onChange={(event) => setOrderBy(event.target.value)}
                  id="status"
                  class="mt-2 block w-full rounded-md border border-gray-200 px-2 py-2 shadow-sm outline-none focus:border-blue-500 focus:ring focus:ring-blue-200 focus:ring-opacity-50"
                >
                  <option value="DateDesc" selected>
                    Date Descending
                  </option>
                  <option value="DateAsc">Date Ascending</option>
                  <option value="PriceDesc">Price Descending</option>
                  <option value="PriceAsc">Price Ascending</option>
                </select>
              </div>

              <div class="max-w-sm mx-auto">
                <label
                  for="countries_multiple"
                  class="block mb-2 text-lg font-bold"
                >
                  Categories
                </label>
                <select
                  onChange={handleOnChange}
                  multiple
                  id="countries_multiple"
                  class=" border border-gray-200 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 "
                >
                  <option selected value="">
                    Choose Category
                  </option>
                  {categories.map((category) => (
                    <option key={category.id} value={category.id}>
                      {category.name}
                    </option>
                  ))}
                </select>
              </div>
            </div>

            <div class="mt-6 grid w-full grid-cols-2 justify-center space-x-4 md:flex">
              <button class="active:scale-95 rounded-lg bg-blue-600 px-8 py-2 font-medium text-white outline-none focus:ring hover:opacity-90">
                Search
              </button>
            </div>
          </div>
        </form>

        {products.length !== 0 && (
          <section
            id=""
            class="w-fit mx-auto grid grid-cols-1 lg:grid-cols-3 md:grid-cols-2 justify-items-center justify-center gap-y-20 gap-x-14 mt-10 mb-5"
          >
            {products.map((product) => (
              <ProductCard
                props={{
                  id: product.id,
                  imageUrl: product.imageUrl,
                  title: product.title,
                  price: product.price,
                  isOnDiscount: product.isOnDiscount,
                  creatorName: product.creatorName,
                  discountPrice: product.discountPrice,
                  isPromoted: product.isPromoted,
                }}
                renderButtons={false}
              />
            ))}
          </section>
        )}
        {products && totalPages > 1 && (
          <div className=" mx-auto my-10">
            <div class="flex items-center gap-4">
              <a href="#filters">
                <button
                  onClick={() => setPageNumber((prev) => Math.max(prev - 1, 1))}
                  disabled={pageNumber === 1}
                  class="flex items-center gap-2 px-6 py-3 font-sans text-xs font-bold text-center text-gray-900 uppercase align-middle transition-all rounded-lg select-none hover:bg-gray-900/10 active:bg-gray-900/20 disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
                  type="button"
                >
                  Previous
                </button>
              </a>

              <a href="#filters">
                <button
                  onClick={() => setPageNumber((prev) => prev + 1)}
                  disabled={pageNumber >= totalPages}
                  class="flex items-center gap-2 px-6 py-3 font-sans text-xs font-bold text-center text-gray-900 uppercase align-middle transition-all rounded-lg select-none hover:bg-gray-900/10 active:bg-gray-900/20 disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
                  type="button"
                >
                  Next
                </button>
              </a>
            </div>
          </div>
        )}
      </>
    </div>
  );
}

export default Profile;
