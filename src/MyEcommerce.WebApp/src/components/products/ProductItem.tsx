import {
  Grid,
  Card,
  CardMedia,
  CardContent,
  Typography,
  Chip,
  Skeleton,
} from "@mui/material";
import React, { ReactElement } from "react";
import { ProductReadDto } from "../../generated/product-service/dist";
import NextLink from "next/link";
import ProductPrice from "./ProductPrice";

interface ProductItemProps {
  isLoading: boolean;
  product: ProductReadDto;
}

const ProductItem: React.FC<ProductItemProps> = ({
  isLoading,
  product,
}): ReactElement => {
  const linkToProduct = (productId: any, children: any) => {
    return (
      <NextLink href="/product/[id]" as={`/product/${productId}`}>
        <a>{children}</a>
      </NextLink>
    );
  };

  return (
    <React.Fragment>
      <Grid item xs={12} sm={6} md={4}>
        <Card>
          {linkToProduct(
            product.id,
            isLoading ? (
              <Skeleton
                sx={{ height: 200 }}
                animation="wave"
                variant="rectangular"
              />
            ) : (
              <CardMedia component="img" image={product.imageUri!} />
            )
          )}
          {isLoading ? (
            <CardContent>
              <Skeleton
                animation="wave"
                height={20}
                style={{ marginBottom: 6 }}
              />
              <Skeleton animation="wave" height={20} />
            </CardContent>
          ) : (
            <CardContent>
              {!product.isNew && !product.onSale ? null : (
                <Typography mb={1}>
                  {!product.isNew ? null : (
                    <Chip label="New" color="primary" sx={{ mr: 1 }} />
                  )}
                  {!product.onSale ? null : (
                    <Chip label="On Sale" color="error" sx={{ mr: 1 }} />
                  )}
                </Typography>
              )}
              <Typography>{linkToProduct(product.id, product.name)}</Typography>
              <Typography>
                <ProductPrice
                  price={product.price}
                  salePrice={product.salePrice}
                />
              </Typography>
            </CardContent>
          )}
        </Card>
      </Grid>
    </React.Fragment>
  );
};

export default ProductItem;
