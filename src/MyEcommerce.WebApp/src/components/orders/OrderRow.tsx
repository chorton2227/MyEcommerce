import { KeyboardArrowDown, KeyboardArrowUp } from "@mui/icons-material";
import {
  Box,
  Collapse,
  Grid,
  IconButton,
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
  Typography,
} from "@mui/material";
import React, { useState } from "react";
import { OrderDto } from "../../generated/order-service/dist";
import Image from "next/image";

interface OrderRowProps {
  order: OrderDto;
}

const OrderRow: React.FC<OrderRowProps> = ({ order }) => {
  const [open, setOpen] = useState(false);

  return (
    <React.Fragment>
      <TableRow sx={{ "& > *": { borderBottom: "unset" } }}>
        <TableCell>
          <IconButton
            aria-label="Expand Row"
            size="small"
            onClick={() => setOpen(!open)}
          >
            {open ? <KeyboardArrowUp /> : <KeyboardArrowDown />}
          </IconButton>
        </TableCell>
        <TableCell>{order.id}</TableCell>
        <TableCell>{order.orderDate}</TableCell>
        <TableCell>{order.status?.name}</TableCell>
        <TableCell>${order.total}</TableCell>
      </TableRow>
      <TableRow>
        <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={5}>
          <Collapse in={open} timeout="auto" unmountOnExit>
            <Grid container spacing={4} sx={{ py: 2 }}>
              <Grid item xs={12} md={3}>
                <Typography variant="h6" gutterBottom component="div">
                  Delivery Address
                </Typography>
                <Typography>
                  {order.deliveryAddress?.firstName}{" "}
                  {order.deliveryAddress?.lastName}
                </Typography>
                <Typography>{order.deliveryAddress?.street1}</Typography>
                <Typography>{order.deliveryAddress?.street2}</Typography>
                <Typography>
                  {order.deliveryAddress?.city}, {order.deliveryAddress?.state}{" "}
                  {order.deliveryAddress?.zipCode}
                </Typography>
                <Typography>{order.deliveryAddress?.country}</Typography>
              </Grid>
              <Grid item xs={12} md={9}>
                <Typography variant="h6" gutterBottom component="div">
                  Line Items
                </Typography>
                <Table size="small" aria-label="Order Line Items">
                  <TableHead>
                    <TableRow>
                      <TableCell colSpan={2}>Product</TableCell>
                      <TableCell>Price</TableCell>
                      <TableCell>Quantity</TableCell>
                      <TableCell>Line Total</TableCell>
                    </TableRow>
                  </TableHead>
                  <TableBody>
                    {order.orderItems?.map((item) => (
                      <TableRow key={item.id}>
                        <TableCell>
                          <Image
                            src={item.imageUrl!}
                            alt={item.name!}
                            loading="lazy"
                            width={113}
                            height={84}
                          />
                        </TableCell>
                        <TableCell>{item.name}</TableCell>
                        <TableCell>${item.price}</TableCell>
                        <TableCell>{item.quantity}</TableCell>
                        <TableCell>${item.total}</TableCell>
                      </TableRow>
                    ))}
                  </TableBody>
                </Table>
              </Grid>
            </Grid>
          </Collapse>
        </TableCell>
      </TableRow>
    </React.Fragment>
  );
};

export default OrderRow;
