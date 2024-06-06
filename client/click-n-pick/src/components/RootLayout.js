import { Outlet } from "react-router-dom";
import MainNavigation from "../components/MainNavigation.js";
import Footer from "../components/Footer.js";

function RootLayout() {
  return (
    <div className="min-h-screen flex flex-col">
      <MainNavigation />
      <Outlet />
      <Footer />
    </div>
  );
}

export default RootLayout;
