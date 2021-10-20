import React from "react";
import Footer from "./Footer";
import Navigation from "./Navigation";

const Layout: React.FC<{}> = ({ children }) => {
  return (
    <>
      <Navigation />
      <main>{children}</main>
      <Footer />
    </>
  );
};

export default Layout;
