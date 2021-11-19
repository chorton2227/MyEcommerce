import { LoadingButton } from "@mui/lab";
import { Box, TextField } from "@mui/material";
import React, { ReactElement } from "react";
import { useMutation, useQueryClient } from "react-query";
import { addToCart } from "../../apis/shoppingCartApi";
import { AddToCartModel } from "../../generated/shopping-cart-service/dist";
import isAuth from "../../utils/isAuth";

type AddToCartProps = {
  model: AddToCartModel;
};

const AddToCart: React.FC<AddToCartProps> = ({ model }): ReactElement => {
  const queryClient = useQueryClient();

  const { isLoading, mutate: addToCartMutate } = useMutation(addToCart, {
    // onMutate: () => {},
    onSuccess: (data) => {
      queryClient.setQueryData(["cart"], data);
    },
    // onError: (error, variables, context) => {},
  });

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const data = new FormData(event.currentTarget);
    addToCartMutate({
      ...model,
      quantity: parseInt(data.get("quantity")?.toString() || "1"),
    });
  };

  const isDisabled = !isAuth();

  return (
    <Box component="form" onSubmit={handleSubmit} sx={{ mt: 2 }}>
      {!isDisabled ? null : "Login to add items to your cart"}
      <TextField
        name="quantity"
        label="Quantity"
        type="number"
        defaultValue={1}
        disabled={isDisabled}
      />
      <LoadingButton
        type="submit"
        variant="contained"
        sx={{ my: 2 }}
        loading={isLoading}
        disabled={isDisabled}
      >
        Add to Cart
      </LoadingButton>
    </Box>
  );
};

export default AddToCart;
