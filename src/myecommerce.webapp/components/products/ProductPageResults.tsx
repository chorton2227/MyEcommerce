import { Skeleton, Typography } from "@mui/material";
import React from "react";

interface ProductPageResultsProps {
  isLoading: boolean;
  pageIndex: number;
  pageLimit: number;
  totalProducts: number;
}

const ProductPageResults: React.FC<ProductPageResultsProps> = ({
  isLoading,
  pageIndex,
  pageLimit,
  totalProducts,
}) => {
  if (isLoading) {
    return <Skeleton animation="wave" />;
  }

  if (totalProducts <= 0) {
    return null;
  }

  const pageFrom = 1;
  let pageTo = pageIndex * pageLimit + pageLimit;
  if (pageTo > totalProducts) {
    pageTo = totalProducts;
  }

  return (
    <Typography>
      <React.Fragment>
        Showing {pageFrom}-{pageTo} of {totalProducts} results
      </React.Fragment>
    </Typography>
  );
};

export default ProductPageResults;
