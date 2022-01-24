import React, { useEffect } from "react";
import NextLink from "next/link";
import { ReactElement } from "react";
import { useQuery } from "react-query";
import { getCart } from "../../apis/shoppingCartApi";
import ShoppingCartIcon from "@mui/icons-material/ShoppingCart";
import CloseIcon from "@mui/icons-material/Close";
import {
  Box,
  Button,
  Divider,
  Drawer,
  IconButton,
  Stack,
  Typography,
} from "@mui/material";
import ShoppingCartItem from "./ShoppingCartItem";
import { addEvent } from "../../utils/events";

const ShoppingCart: React.FC<{}> = (): ReactElement => {
  const [toggle, setToggle] = React.useState(false);
  const { data: cartResponse } = useQuery(["cart"], () => getCart());

  useEffect(() => {
    addEvent("shoppingCart:open", () => {
      setToggle(true);
    });
  }, []);

  const toggleDrawer =
    (open: boolean) => (event: React.KeyboardEvent | React.MouseEvent) => {
      if (
        event.type === "keydown" &&
        ((event as React.KeyboardEvent).key === "Tab" ||
          (event as React.KeyboardEvent).key === "Shift")
      ) {
        return;
      }

      setToggle(open);
    };

  return (
    <React.Fragment>
      <IconButton aria-label="Open Shopping Cart" onClick={toggleDrawer(true)}>
        <ShoppingCartIcon />
      </IconButton>
      <Drawer anchor="right" open={toggle} onClose={toggleDrawer(false)}>
        <Stack direction="row-reverse" sx={{ pr: 1 }}>
          <IconButton
            aria-label="Close Shopping Cart"
            onClick={toggleDrawer(false)}
          >
            <CloseIcon />
          </IconButton>
        </Stack>
        <Box sx={{ mx: 2, my: 2, maxWidth: "500px" }}>
          {!cartResponse || cartResponse.shoppingCartItems.length === 0 ? (
            <Typography variant="h5" alignContent="center">
              Shopping cart is empty
            </Typography>
          ) : (
            <React.Fragment>
              {cartResponse.shoppingCartItems.map((item) => (
                <ShoppingCartItem key={item.id} item={item} />
              ))}
              <Typography variant="h5" alignContent="center">
                Subtotal: <strong>${cartResponse.subtotal.toFixed(2)}</strong>
              </Typography>
              <Divider sx={{ py: 1, mb: 2 }} />
              <NextLink href="/checkout">
                <Button variant="contained" size="large" fullWidth>
                  Checkout
                </Button>
              </NextLink>
            </React.Fragment>
          )}
        </Box>
      </Drawer>
    </React.Fragment>
  );
};

export default ShoppingCart;
