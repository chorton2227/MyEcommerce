import {
  Body,
  Controller,
  Delete,
  Get,
  Path,
  Post,
  Put,
  Request,
  Route,
  Security,
} from "tsoa";
import { ShoppingCart } from "../data/entities/ShoppingCart";
import { ShoppingCartItem } from "../data/entities/ShoppingCartItem";
import { AddToCartModel } from "../models/AddToCartModel";
import NotFoundError from "../errors/NotFoundError";
import { UpdateItemInCartModel } from "../models/UpdateItemInCartModel";

@Route("shopping-carts/my-cart")
@Security("jwt")
export class ShoppingCartsController extends Controller {
  @Get()
  public async getCartForCurrentUser(
    @Request() request: any
  ): Promise<ShoppingCart> {
    return this.getOrCreateCartForCurrentUser(request);
  }

  @Post()
  public async addToCartForCurrentUser(
    @Request() request: any,
    @Body() model: AddToCartModel
  ): Promise<ShoppingCart> {
    const shoppingCart = await this.getOrCreateCartForCurrentUser(request);

    // Add item to cart or update quantity for existing item
    const existingItem = shoppingCart.shoppingCartItems?.find(
      (value) => value.productId === model.productId
    );
    if (existingItem) {
      existingItem.quantity += model.quantity;
      existingItem.save();
    } else {
      const shoppingCartItem = await ShoppingCartItem.create({
        ...model,
      }).save();
      shoppingCart.shoppingCartItems.push(shoppingCartItem);
      shoppingCart.save();
    }

    return shoppingCart;
  }

  @Delete("{shoppingCartItemId}")
  public async removeItemFromCartForCurrentUser(
    @Request() request: any,
    @Path() shoppingCartItemId: number
  ): Promise<ShoppingCart> {
    const shoppingCart = await this.getOrCreateCartForCurrentUser(request);
    const itemToRemove = shoppingCart.shoppingCartItems?.find(
      (value) => value.id === shoppingCartItemId
    );
    if (!itemToRemove) {
      throw new NotFoundError("Shopping cart item does not exist");
    }

    await ShoppingCartItem.delete({ id: shoppingCartItemId });
    return this.getOrCreateCartForCurrentUser(request);
  }

  @Put("{shoppingCartItemId}")
  public async updateItemInCartForCurrentUser(
    @Request() request: any,
    @Path() shoppingCartItemId: number,
    @Body() model: UpdateItemInCartModel
  ): Promise<ShoppingCart> {
    const shoppingCart = await this.getOrCreateCartForCurrentUser(request);
    const itemToUpdate = shoppingCart.shoppingCartItems?.find(
      (value) => value.id === shoppingCartItemId
    );
    if (!itemToUpdate) {
      throw new NotFoundError("Shopping cart item does not exist");
    }

    itemToUpdate.quantity = model.quantity;
    itemToUpdate.save();

    return shoppingCart;
  }

  private async getOrCreateCartForCurrentUser(
    request: any
  ): Promise<ShoppingCart> {
    const userId = request.user.sub;
    let shoppingCart = await ShoppingCart.findOne(
      {
        userId,
      },
      { relations: ["shoppingCartItems"] }
    );

    if (!shoppingCart) {
      shoppingCart = await ShoppingCart.create({
        userId,
      }).save();
    }

    return shoppingCart;
  }
}