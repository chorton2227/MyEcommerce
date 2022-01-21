import {
  PaymentsApi,
  StripePaymentRequest,
  StripePaymentResponse,
} from "../generated/payment-service/dist";

const paymentsApi = new PaymentsApi(
  undefined,
  process.env.NEXT_PUBLIC_PAYMENT_SERVICE_URL
);

export const createPayment = (
  request: StripePaymentRequest,
  options?: any
): Promise<StripePaymentResponse> =>
  paymentsApi.createAsync(request, options).then((response) => response.data);
