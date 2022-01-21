import {
  ShoppingCart,
  DefaultApi,
  AddToCartModel,
} from "../generated/shopping-cart-service/dist";
import axios from "../generated/shopping-cart-service/node_modules/axios";

const shoppingCartInstance = axios.create({
  baseURL: process.env.NEXT_PUBLIC_SHOPPING_CART_SERVICE_URL,
});

const shoppingCartApi = new DefaultApi(
  undefined,
  process.env.NEXT_PUBLIC_SHOPPING_CART_SERVICE_URL,
  shoppingCartInstance
);

const setAuthorization = (): boolean => {
  const token = localStorage.getItem("jwt") || null;
  if (!token) {
    shoppingCartInstance.defaults.headers.common["Authorization"] = "";
    return false;
  }

  shoppingCartInstance.defaults.headers.common[
    "Authorization"
  ] = `Bearer ${token}`;
  return true;
};

export const getCart = async (): Promise<ShoppingCart> => {
  const isAuth = setAuthorization();
  if (!isAuth) {
    return Promise.reject(null);
  }

  return shoppingCartApi
    .getCartForCurrentUser()
    .then((response) => response.data);
};

export const addToCart = async (
  model: AddToCartModel
): Promise<ShoppingCart> => {
  const isAuth = setAuthorization();
  if (!isAuth) {
    return Promise.reject(null);
  }

  return shoppingCartApi
    .addToCartForCurrentUser(model)
    .then((response) => response.data);
};

export const removeCart = async (): Promise<void> => {
  const isAuth = setAuthorization();
  if (!isAuth) {
    return Promise.reject(null);
  }

  return shoppingCartApi
    .removeCartForCurrentUser()
    .then((response) => response.data);
};

export const removeFromCart = async (
  shoppingCartItemId: number
): Promise<ShoppingCart> => {
  const isAuth = setAuthorization();
  if (!isAuth) {
    return Promise.reject(null);
  }

  return shoppingCartApi
    .removeItemFromCartForCurrentUser(shoppingCartItemId)
    .then((response) => response.data);
};
