import { useState, useEffect } from "react";

import ProductCard from "../../components/ProductCard";
import { searchAll } from "../../fetch/requests/products";
import { getAll } from "../../fetch/requests/categories";

function Search() {
  const PAGE_SIZE = 9;

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
    (async function getCategories() {
      try {
        const response = await getAll();

        if (response.status !== 200) {
          throw new Error("Network response was not ok");
        }

        var data = await response.json();
        setCategories(data.categories);
      } catch (error) {
        alert("Some problem occurred.");
      }
    })();
  }, []);

  useEffect(() => {
    fetchProducts();
  }, [pageNumber]);

  async function fetchProducts(event) {
    if (event) {
      event.preventDefault();
    }

    try {
      const params = new URLSearchParams({
        pageNumber: pageNumber,
        search: search,
        minPrice: minPrice,
        maxPrice: maxPrice,
        orderBy: orderBy,
      });

      if (categoryIds && categoryIds.length > 0) {
        categoryIds.forEach((category) => {
          params.append("categoryIds", category);
        });
      }

      const response = await searchAll(params);

      if (response.status !== 200) {
        throw new Error("Network response was not ok");
      }

      var data = await response.json();
      var products = data.products;

      setLoading(false);
      setTotalPages(Math.ceil(data.totalItems / PAGE_SIZE));
      setProducts(products);
    } catch (error) {
      alert("Some problem occurred.");
    }
  }

  if (loading) {
    return (
      <div className="text-center p-10">
        <h1 className="font-bold text-4xl mb-4">Products are fetching</h1>
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
    <>
      <form
        onSubmit={fetchProducts}
        id="filters"
        className=" max-w-screen-md mx-auto m-20 "
      >
        <div className="rounded-xl  bg-white p-6 shadow-lg border-t-2 ">
          <p className="mt-1 text-sm"></p>
          <div className="mt-8 grid grid-cols-1 gap-6 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 ">
            <div className="flex flex-col">
              <label for="search" className="text-stone-600 text-lg font-bold">
                Search
              </label>
              <input
                onChange={(event) => setSearch(event.target.value)}
                type="text"
                id="search"
                placeholder="search"
                className="mt-2 block w-full rounded-md border border-gray-200 px-2 py-2 shadow-sm outline-none focus:border-blue-500 focus:ring focus:ring-blue-200 focus:ring-opacity-50"
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
                max="50000"
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
                placeholder="50 000"
                min="1"
                max="50000"
                type="number"
                className="mt-2 block w-full rounded-md border border-gray-200 px-2 py-2 shadow-sm outline-none focus:border-blue-500 focus:ring focus:ring-blue-200 focus:ring-opacity-50"
              />
            </div>

            <div className="flex flex-col">
              <label for="status" className="text-stone-600 text-lg font-bold">
                Order by
              </label>

              <select
                onChange={(event) => setOrderBy(event.target.value)}
                id="status"
                className="mt-2 block w-full rounded-md border border-gray-200 px-2 py-2 shadow-sm outline-none focus:border-blue-500 focus:ring focus:ring-blue-200 focus:ring-opacity-50"
              >
                <option value="DateDesc" selected>
                  Date Descending
                </option>
                <option value="DateAsc">Date Ascending</option>
                <option value="PriceDesc">Price Descending</option>
                <option value="PriceAsc">Price Ascending</option>
              </select>
            </div>

            <div className="max-w-sm mx-auto">
              <label
                for="countries_multiple"
                className="block mb-2 text-lg font-bold"
              >
                Categories
              </label>
              <select
                onChange={handleOnChange}
                multiple
                id="countries_multiple"
                className=" border border-gray-200 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 "
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

          <div className="mt-6 grid w-full grid-cols-2 justify-center space-x-4 md:flex">
            <button className="active:scale-95 rounded-lg bg-blue-600 px-8 py-2 font-medium text-white outline-none focus:ring hover:opacity-90">
              Search
            </button>
          </div>
        </div>
      </form>

      {products.length !== 0 && (
        <section
          id=""
          className="w-fit mx-auto grid grid-cols-1 lg:grid-cols-3 md:grid-cols-2 justify-items-center justify-center gap-y-20 gap-x-14 mt-10 mb-28"
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
                categoryName: product.categoryName,
              }}
            />
          ))}
        </section>
      )}
      {products && totalPages > 1 && (
        <div className="mx-auto my-10">
          <div className="flex items-center gap-4">
            <a href="#filters">
              <button
                onClick={() => setPageNumber((prev) => Math.max(prev - 1, 1))}
                disabled={pageNumber === 1}
                className="flex items-center gap-2 px-6 py-3 font-sans text-xs font-bold text-center text-gray-900 uppercase align-middle transition-all rounded-lg select-none hover:bg-gray-900/10 active:bg-gray-900/20 disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
                type="button"
              >
                Previous
              </button>
            </a>

            <a href="#filters">
              <button
                onClick={() => setPageNumber((prev) => prev + 1)}
                disabled={pageNumber >= totalPages}
                className="flex items-center gap-2 px-6 py-3 font-sans text-xs font-bold text-center text-gray-900 uppercase align-middle transition-all rounded-lg select-none hover:bg-gray-900/10 active:bg-gray-900/20 disabled:pointer-events-none disabled:opacity-50 disabled:shadow-none"
                type="button"
              >
                Next
              </button>
            </a>
          </div>
        </div>
      )}
    </>
  );
}

export default Search;
