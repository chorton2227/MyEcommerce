import {
  OrderCreateDto,
  OrderDto,
  OrdersApi,
  PaginatedOrdersDto,
} from "../generated/order-service/dist";
import axios from "../generated/order-service/node_modules/axios";

const orderInstance = axios.create({
  baseURL: process.env.NEXT_PUBLIC_ORDER_SERVICE_URL,
});

const ordersApi = new OrdersApi(
  undefined,
  process.env.NEXT_PUBLIC_ORDER_SERVICE_URL,
  orderInstance
);

const setAuthorization = (): boolean => {
  const token = localStorage.getItem("jwt") || null;
  if (!token) {
    orderInstance.defaults.headers.common["Authorization"] = "";
    return false;
  }

  orderInstance.defaults.headers.common["Authorization"] = `Bearer ${token}`;
  return true;
};

export const createOrder = async (
  dto: OrderCreateDto,
  options?: any
): Promise<OrderDto> => {
  const isAuth = setAuthorization();
  if (!isAuth) {
    return Promise.reject(null);
  }

  const response = await ordersApi.createAsync(dto, options);
  return response.data;
};

export const getAllOrders = (
  pageIndex?: number,
  pageLimit?: number,
  userId?: string,
  options?: any
): Promise<PaginatedOrdersDto> => {
  const isAuth = setAuthorization();
  if (!isAuth) {
    return Promise.reject(null);
  }

  return ordersApi
    .getAll(pageIndex, pageLimit, userId, options)
    .then((response) => response.data);
};

export const getOrderById = (id: string, options?: any): Promise<OrderDto> => {
  const isAuth = setAuthorization();
  if (!isAuth) {
    return Promise.reject(null);
  }

  return ordersApi.getById(id, options).then((response) => response.data);
};
