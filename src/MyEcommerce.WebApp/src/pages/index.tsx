import type { NextPage } from "next";
import React from "react";
import Hero from "../components/Hero";
import Layout from "../components/Layout";
import ProductList from "../components/products/ProductList";

const Home: NextPage = () => {
  return (
    <Layout>
      <Hero />
      <ProductList />
    </Layout>
  );
};

export default Home;
