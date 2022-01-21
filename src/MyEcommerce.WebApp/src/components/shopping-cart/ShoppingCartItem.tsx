import { LoadingButton } from "@mui/lab";
import { Box, Divider, Grid, Typography } from "@mui/material";
import React, { ReactElement } from "react";
import { useMutation, useQueryClient } from "react-query";
import { removeFromCart } from "../../apis/shoppingCartApi";
import { ShoppingCartItem } from "../../generated/shopping-cart-service/dist";
import Image from "next/image";

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
      container
      spacing={2}
      direction="row"
      justifyContent="center"
      alignItems="center"
    >
      <Grid item xs={3}>
        <Box sx={{ maxWidth: "100%" }}>
          <Image
            src={item.imageUrl}
            alt={item.name}
            loading="lazy"
            width={113}
            height={84}
          />
        </Box>
      </Grid>
      <Grid item xs={6}>
        <Typography>{item.name}</Typography>
        <Typography>
          {item.quantity} x <strong>${item.unitPrice}</strong>
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
