import { Alert, Box, Typography } from "@mui/material";
import {
  PaymentElement,
  useElements,
  useStripe,
} from "@stripe/react-stripe-js";
import { PaymentIntent } from "@stripe/stripe-js";
import React, {
  forwardRef,
  ReactElement,
  useImperativeHandle,
  useState,
} from "react";

export interface CheckoutPaymentFunctions {
  submitForm(e: any): Promise<PaymentIntent | undefined>;
}

export interface CheckoutPaymentProps {}

const CheckoutPayment = forwardRef<
  CheckoutPaymentFunctions,
  CheckoutPaymentProps
>(({}, ref): ReactElement => {
  const stripe = useStripe();
  const elements = useElements();
  const [errorMessage, setErrorMessage] = useState<string>();

  useImperativeHandle(ref, () => ({
    async submitForm(e: any) {
      setErrorMessage(undefined);

      if (!stripe || !elements) {
        return undefined;
      }

      const { paymentIntent, error } = await stripe.confirmPayment({
        elements,
        redirect: "if_required",
      });

      if (error) {
        setErrorMessage(error.message);
        return undefined;
      }

      return paymentIntent;
    },
  }));

  if (!stripe || !elements) {
    return <Typography>Loading</Typography>;
  }

  return (
    <React.Fragment>
      <Typography variant="h6" gutterBottom>
        Payment Details
      </Typography>
      {errorMessage && (
        <Alert severity="error" sx={{ mt: 2, mb: 2 }}>
          {errorMessage}
        </Alert>
      )}
      <PaymentElement />
    </React.Fragment>
  );
});

export default CheckoutPayment;
