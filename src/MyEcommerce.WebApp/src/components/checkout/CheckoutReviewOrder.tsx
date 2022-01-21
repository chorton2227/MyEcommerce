import {
  Divider,
  List,
  ListItem,
  ListItemText,
  Typography,
} from "@mui/material";
import React, { ReactElement } from "react";
import { ShoppingCart } from "../../generated/shopping-cart-service/dist";

export interface CheckoutReviewOrderProps {
  shoppingCart: ShoppingCart | undefined;
}

const CheckoutReviewOrder: React.FC<CheckoutReviewOrderProps> = ({
  shoppingCart,
}): ReactElement => {
  return (
    <React.Fragment>
      <Typography variant="h6" gutterBottom>
        Order summary
      </Typography>
      <List disablePadding>
        {shoppingCart?.shoppingCartItems.map((item) => (
          <ListItem key={item.name} sx={{ py: 1, px: 0 }}>
            <ListItemText>{item.name}</ListItemText>
            <Typography variant="body2">
              {item.quantity} x <strong>${item.unitPrice}</strong>
            </Typography>
          </ListItem>
        ))}
        <Divider />
        <ListItem sx={{ py: 1, px: 0 }}>
          <ListItemText primary="Total" />
          <Typography variant="subtitle1" sx={{ fontWeight: 700 }}>
            ${shoppingCart?.subtotal}
          </Typography>
        </ListItem>
      </List>
    </React.Fragment>
  );
};

export default CheckoutReviewOrder;
