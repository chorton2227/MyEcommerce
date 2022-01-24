import { Container, Grid, Typography } from "@mui/material";
import { useRouter } from "next/dist/client/router";
import React, { useState, useEffect } from "react";
import { ProductReadDto } from "../../generated/product-service/dist/index";
import Layout from "../../components/Layout";
import Image from "next/image";
import { getProductById } from "../../apis/productsApi";
import AddToCart from "../../components/shopping-cart/AddToCart";
import ProductPrice from "../../components/products/ProductPrice";

const Product = () => {
  const router = useRouter();
  const productId = router.query.id as string;
  const [isLoading, setIsLoading] = useState(true);
  const [product, setProduct] = useState<ProductReadDto>();

  useEffect(() => {
    getProduct();
  }, []); // eslint-disable-line react-hooks/exhaustive-deps

  const getProduct = async () => {
    if (!productId) {
      return;
    }

    setIsLoading(true);
    const response = await getProductById(productId);
    setProduct(response);
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
          <Grid item xs={12} md={6}>
            <Image
              src={product.imageUri!}
              alt="Product image"
              width={640}
              height={480}
            />
          </Grid>
          <Grid item xs={12} md={6}>
            <Typography className="product-title" variant="h4" component="h1">
              {product.name}
            </Typography>
            <Typography variant="h5">
              <ProductPrice
                price={product.price}
                salePrice={product.salePrice}
              />
            </Typography>
            <Typography variant="body1">{product.description}</Typography>
            <AddToCart
              model={{
                imageUrl: product.imageUri as string,
                name: product.name as string,
                price: product.price as number,
                productId: product.id as string,
                salePrice: product.salePrice || null,
                quantity: 1,
              }}
            />
          </Grid>
        </Grid>
      </Container>
    </Layout>
  );
};

export default Product;
