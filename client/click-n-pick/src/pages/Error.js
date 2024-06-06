import { Link } from "react-router-dom";
import Robot from "../img/Robot.jpg";

function Error() {
  return (
    <>
      <div
        className="flex items-center justify-center min-h-screen bg-contain bg-center bg-no-repeat"
        style={{ backgroundImage: `url(${Robot})` }}
      >
        <div className="max-w-md mx-auto text-center bg-white bg-opacity-70 p-8 rounded-lg shadow-lg">
          <div className="text-9xl font-bold text-indigo-600 mb-4">404</div>
          <h1 className="text-4xl font-bold text-gray-800 mb-6">
            Oops! Page Not Found
          </h1>
          <p className="text-lg text-grey-900 mb-8">
            The page you're looking for seems to have gone on a little
            adventure. Don't worry, we'll help you find your way back home.
          </p>
          <Link
            className="inline-block bg-indigo-600 text-white font-semibold px-6 py-3 rounded-md hover:bg-indigo-700 transition-colors duration-300"
            to="/"
          >
            Go Back Home
          </Link>
        </div>
      </div>
    </>
  );
}

export default Error;
