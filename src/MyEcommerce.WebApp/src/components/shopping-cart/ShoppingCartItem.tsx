import { LoadingButton } from "@mui/lab";
import { Button, Divider, Grid, Typography } from "@mui/material";
import React, { ReactElement } from "react";
import { useMutation, useQueryClient } from "react-query";
import { removeFromCart } from "../../apis/shoppingCartApi";
import { ShoppingCartItem } from "../../generated/shopping-cart-service/dist";

type ShoppingCartItemProps = {
  item: ShoppingCartItem;
};

const ShoppingCartItemComponent: React.FC<ShoppingCartItemProps> = ({
  item,
}): ReactElement => {
  const queryClient = useQueryClient();

  const { isLoading, mutate: removeFromCartMutation } = useMutation(
    removeFromCart,
    {
      // onMutate: () => {},
      onSuccess: (data) => {
        queryClient.setQueryData(["cart"], data);
      },
      // onError: (error, variables, context) => {},
    }
  );

  const handleRemoveItem = () => {
    removeFromCartMutation(item.id);
  };

  return (
    <Grid
      key={item.id}
      container
      spacing={2}
      direction="row"
      justifyContent="center"
      alignItems="center"
    >
      <Grid item xs={3}>
        <img
          src={item.imageUrl}
          alt={item.name}
          loading="lazy"
          style={{ maxWidth: "100%" }}
        />
      </Grid>
      <Grid item xs={6}>
        <Typography>{item.name}</Typography>
        <Typography>
          {item.quantity} x{" "}
          <strong>${item.salePrice ? item.salePrice : item.price}</strong>
        </Typography>
      </Grid>
      <Grid item xs={3}>
        <LoadingButton
          variant="outlined"
          size="small"
          color="error"
          loading={isLoading}
          onClick={handleRemoveItem}
        >
          Remove item
        </LoadingButton>
      </Grid>
      <Grid item xs={12} sx={{ py: 2 }}>
        <Divider />
      </Grid>
    </Grid>
  );
};

export default ShoppingCartItemComponent;
