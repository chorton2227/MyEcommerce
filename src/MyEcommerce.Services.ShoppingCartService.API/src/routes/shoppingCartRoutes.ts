// import express from "express";
// import authenticateUser from "../middlewares/authenticateUser";
// import {
//   addToCartForCurrentUser,
//   getCartForCurrentUser,
//   removeFromCartForCurrentUser,
//   setQuantityOnCartForCurrentUser,
// } from "../controllers/shoppingCartController";

// const router = express.Router();

// router
//   .route("/myCart")
//   .get(authenticateUser, getCartForCurrentUser)
//   .post(authenticateUser, addToCartForCurrentUser);

// router
//   .route("/myCart/:id")
//   .delete(authenticateUser, removeFromCartForCurrentUser);

// router
//   .route("/myCart/:id/quantity")
//   .patch(authenticateUser, setQuantityOnCartForCurrentUser);

// export default router;
