export interface AddToCartModel {
  productId: string;
  name: string;
  price: number;
  salePrice: number | null;
  quantity: number;
  imageUrl: string;
}
