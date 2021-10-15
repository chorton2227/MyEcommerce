import { Container, Grid, Typography } from "@mui/material";
import { useRouter } from "next/dist/client/router";
import React, { useState, useEffect } from "react";
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
      <Container className="product-page" sx={{ my: 4 }}>
        <Grid container spacing={4}>
          <Grid item xs={6}>
            <img src={product.imageUri!} style={{ width: "100%" }} />
          </Grid>
          <Grid item xs={6}>
            <Typography className="product-title" variant="h4" component="h1">
              {product.name}
            </Typography>
            <Typography className="product-price" variant="h5">
              ${product.price}
            </Typography>
            <Typography variant="body1">{product.description}</Typography>
          </Grid>
        </Grid>
      </Container>
    </Layout>
  );
};

export default Product;
