import { useRouter } from "next/dist/client/router";
import { useState, useEffect } from "react";
import {
  ProductReadDto,
  ProductsApi,
  Configuration,
} from "typescript-axios-product-service";
import Layout from "../../components/Layout";

const Product = () => {
  const router = useRouter();
  const productId = router.query.id as string;
  const [isLoading, setIsLoading] = useState(true);
  const [product, setProduct] = useState<ProductReadDto>();

  useEffect(() => {
    getProduct();
  }, [productId]);

  const getProduct = async () => {
    if (!productId) {
      return;
    }

    setIsLoading(true);
    const productsApiService = new ProductsApi(
      new Configuration({
        basePath: "https://localhost:6001",
      })
    );
    const response = await productsApiService.getById(productId);
    setProduct(response.data);
    setIsLoading(false);
  };

  if (isLoading) {
    return <Layout>loading...</Layout>;
  }

  if (!product) {
    return <Layout>product not found</Layout>;
  }

  return (
    <Layout>
      <h1>{product.name}</h1>
      <div>{product.description}</div>
    </Layout>
  );
};

export default Product;
