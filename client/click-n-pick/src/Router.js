import { createBrowserRouter } from "react-router-dom";
import RootLayout from "./components/RootLayout";
import Home from "./pages/Home";
import Register from "./pages/Register";
import Login from "./pages/Login";
import ConfirmEmail from "./pages/ConfirmEmail";
import Error from "./pages/Error";
import ForgotPasswordInput from "./pages/ForgotPasswordInput";
import ForgotPasswordChange from "./pages/ForgotPasswordChange";
import UserSettings from "./pages/UserSettings";
import CreateProduct from "./pages/CreateProduct";
import ProductDetail from "./pages/ProductDetails";
import MyProducts from "./pages/MyProducts";
import EditProduct from "./pages/EditProduct";
import Promotion from "./pages/Promotion";
import Payment from "./pages/Payment";
import Search from "./pages/Search";
import Profile from "./pages/Profile";
import AdminCategories from "./pages/AdminCategories";
import EditCategory from "./pages/EditCategory";
import CreateCategory from "./pages/CreateCategory";
import RequestDelivery from "./pages/RequestDelivery";
import ShipmentsToRecieve from "./pages/ShipmentsToReceive";
import ShipmentsToSend from "./pages/ShipmentsToSend";
import AcceptDelivery from "./pages/AcceptDelivery";
import ShipmentDetails from "./pages/ShipmentDetails";

const Router = createBrowserRouter([
  {
    path: "/",
    element: <RootLayout />,
    errorElement: <Error />,
    children: [
      { path: "/", element: <Home /> },
      { path: "/register", element: <Register /> },
      { path: "/login", element: <Login /> },
      {
        path: "/Identity/ConfirmEmail/:userId/:emailConfirmationToken",
        element: <ConfirmEmail />,
      },
      {
        path: "/Identity/ForgotPassword",
        element: <ForgotPasswordInput />,
      },
      {
        path: "/Identity/ForgotPasswordChange",
        element: <ForgotPasswordChange />,
      },
      {
        path: "/Identity/UserSettings",
        element: <UserSettings />,
      },
      {
        path: "/Products/Create",
        element: <CreateProduct />,
      },
      { path: "/Products/Details/:id", element: <ProductDetail /> },
      {
        path: "/Products/MyProducts",
        element: <MyProducts />,
      },
      {
        path: "/Products/Edit/:id",
        element: <EditProduct />,
      },
      {
        path: "/Products/Promotion/:id",
        element: <Promotion />,
      },
      {
        path: "/Products/Search",
        element: <Search />,
      },
      {
        path: "/Payment/:productId/:promotionId",
        element: <Payment />,
      },
      {
        path: "/Users/Profile/:id",
        element: <Profile />,
      },
      {
        path: "/Delivery/Request/:id",
        element: <RequestDelivery />,
      },
      {
        path: "/Delivery/Accept/:id",
        element: <AcceptDelivery />,
      },
      {
        path: "/Delivery/ShipmentsToReceive",
        element: <ShipmentsToRecieve />,
      },
      {
        path: "/Delivery/ShipmentsToSend",
        element: <ShipmentsToSend />,
      },
      {
        path: "/Delivery/Details/:id",
        element: <ShipmentDetails />,
      },
      {
        path: "/Administration/Categories",
        element: <AdminCategories />,
      },
      {
        path: "/Administration/Categories/Edit/:id",
        element: <EditCategory />,
      },
      {
        path: "/Administration/Categories/Create",
        element: <CreateCategory />,
      },
    ],
  },
]);

export default Router;
