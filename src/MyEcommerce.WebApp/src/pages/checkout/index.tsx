import { LoadingButton } from "@mui/lab";
import { Alert, Box, Container, Paper, Typography } from "@mui/material";
import { Elements } from "@stripe/react-stripe-js";
import { loadStripe } from "@stripe/stripe-js";
import type { NextPage } from "next";
import React, { useEffect, useRef, useState } from "react";
import { useMutation, useQuery } from "react-query";
import { createPayment } from "../../apis/paymentApi";
import { createOrder } from "../../apis/orderApi";
import { getCart, removeCart } from "../../apis/shoppingCartApi";
import CheckoutAddress, {
  CheckoutAddressFunctions,
} from "../../components/checkout/CheckoutAddress";
import CheckoutPayment, {
  CheckoutPaymentFunctions,
} from "../../components/checkout/CheckoutPayment";
import CheckoutReviewOrder from "../../components/checkout/CheckoutReviewOrder";
import Layout from "../../components/Layout";
import { StripePaymentRequest } from "../../generated/payment-service/dist";
import {
  OrderCreateDto,
  OrderDto,
  OrderItemDto,
} from "../../generated/order-service/dist";
import { me } from "../../apis/accountApi";
import jwtDecode, { JwtPayload } from "jwt-decode";

const stripePromise = loadStripe(
  process.env.NEXT_PUBLIC_STRIPE_PUBLISHABLE_KEY!
);

const CheckoutPage: NextPage = () => {
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
  const [clientSecret, setClientSecret] = useState<string>();
  const [errorMessage, setErrorMessage] = useState<string>();
  const [order, setOrder] = useState<OrderDto>();
  const checkoutAddressRef = useRef<CheckoutAddressFunctions>(null);
  const checkoutPaymentRef = useRef<CheckoutPaymentFunctions>(null);

  const { data: meResponse } = useQuery(["account", "me"], () => me());

  const { data: shoppingCart, isLoading: isCartLoading } = useQuery(
    ["cart"],
    () => getCart()
  );

  const createPaymentMutation = useMutation(
    (request: StripePaymentRequest) => createPayment(request),
    {
      onSuccess: (data) => {
        setClientSecret(data.clientSecret!);
      },
    }
  );

  const createOrderMutation = useMutation(
    (dto: OrderCreateDto) => createOrder(dto),
    {
      onSuccess: (data) => {
        setOrder(data);
        removeCartMutation.mutate();
      },
      onError: (error) => {
        console.error(error);
        setIsSubmitting(false);
        setErrorMessage("Failed to create order");
      },
    }
  );

  const removeCartMutation = useMutation(() => removeCart(), {
    onSuccess: (data) => {
      console.log("Cart removed", data);
    },
  });

  useEffect(() => {
    if (isCartLoading) {
      return;
    }

    createPaymentMutation.mutate({
      amount: Math.ceil(shoppingCart!.subtotal * 100),
    });
  }, [isCartLoading]); // eslint-disable-line react-hooks/exhaustive-deps

  if (isCartLoading || !clientSecret) {
    return <>Loading</>;
  }

  const decodedJwt = meResponse?.loggedIn
    ? (jwtDecode<JwtPayload>(meResponse?.jwt as string) as any)
    : undefined;

  const handleSubmit = async (e: any) => {
    e.preventDefault();

    setIsSubmitting(true);
    setErrorMessage(undefined);

    const deliveryAddress = checkoutAddressRef?.current?.submitForm(e);
    if (deliveryAddress === undefined) {
      setIsSubmitting(false);
      return;
    }

    const paymentIntent = await checkoutPaymentRef?.current?.submitForm(e);
    if (paymentIntent === undefined) {
      setIsSubmitting(false);
      return;
    }

    createOrderMutation.mutate({
      chargeId: paymentIntent.id,
      email: decodedJwt?.email,
      deliveryAddress: {
        firstName: deliveryAddress.firstName,
        lastName: deliveryAddress.lastName,
        city: deliveryAddress.city,
        country: deliveryAddress.country,
        state: deliveryAddress.state,
        street1: deliveryAddress.address1,
        street2: deliveryAddress.address2,
        zipCode: deliveryAddress.zip,
      },
      orderItems: shoppingCart!.shoppingCartItems.map<OrderItemDto>((item) => {
        return {
          productId: item.productId,
          name: item.name,
          price: item.unitPrice,
          quantity: item.quantity,
          imageUrl: item.imageUrl,
        };
      }),
    });
  };

  return (
    <Layout>
      <Container component="main" maxWidth="sm" sx={{ mb: 4, mt: 4 }}>
        <Box component="form" onSubmit={handleSubmit}>
          <Typography component="h1" variant="h4" align="center">
            Checkout
          </Typography>
          {errorMessage && (
            <Alert severity="error" sx={{ mt: 2 }}>
              {errorMessage}
            </Alert>
          )}
          {order ? (
            <Paper
              variant="elevation"
              elevation={4}
              sx={{ my: 3, p: { xs: 2, md: 3 } }}
            >
              <Typography variant="h6" gutterBottom>
                Thank you for your order
              </Typography>
              <Typography>
                Your order ({order.id}) has been submitted. We have emailed your
                order confirmation, and will send you an update when your order
                has shipped.
              </Typography>
            </Paper>
          ) : (
            <React.Fragment>
              <Paper
                variant="elevation"
                elevation={4}
                sx={{ my: 3, p: { xs: 2, md: 3 } }}
              >
                <CheckoutReviewOrder shoppingCart={shoppingCart} />
              </Paper>
              <Paper
                variant="elevation"
                elevation={4}
                sx={{ my: 3, p: { xs: 2, md: 3 } }}
              >
                <CheckoutAddress ref={checkoutAddressRef} />
              </Paper>
              <Paper
                variant="elevation"
                elevation={4}
                sx={{ my: 3, p: { xs: 2, md: 3 } }}
              >
                <Elements stripe={stripePromise} options={{ clientSecret }}>
                  <CheckoutPayment ref={checkoutPaymentRef} />
                </Elements>
              </Paper>
              <LoadingButton
                fullWidth
                type="submit"
                variant="contained"
                loading={isSubmitting}
              >
                Pay ${shoppingCart?.subtotal.toFixed(2)}
              </LoadingButton>
            </React.Fragment>
          )}
        </Box>
      </Container>
    </Layout>
  );
};

export default CheckoutPage;
