import React from "react";
import { Typography } from "@mui/material";

interface ProductPriceProps {
  price?: number;
  salePrice?: number | null;
}

const ProductPrice: React.FC<ProductPriceProps> = ({ price, salePrice }) => {
  return (
    <React.Fragment>
      {salePrice ? (
        <React.Fragment>
          <span className="product-price-old">${price}</span>
          &nbsp;
          <span className="product-price">${salePrice}</span>
        </React.Fragment>
      ) : (
        <span className="product-price">${price}</span>
      )}
    </React.Fragment>
  );
};

export default ProductPrice;
