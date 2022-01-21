import roundTo from "../../utils/roundTo";
import {
  AfterLoad,
  BaseEntity,
  Column,
  CreateDateColumn,
  Entity,
  ManyToOne,
  PrimaryGeneratedColumn,
  UpdateDateColumn,
} from "typeorm";
import { ShoppingCart } from "./ShoppingCart";

@Entity()
export class ShoppingCartItem extends BaseEntity {
  @PrimaryGeneratedColumn()
  id: number;

  @CreateDateColumn()
  createdAt: Date;

  @UpdateDateColumn()
  updatedAt: Date;

  @Column()
  productId!: string;

  @Column()
  name!: string;

  @Column("decimal", { precision: 12, scale: 2 })
  price!: number;

  @Column("decimal", { precision: 12, scale: 2, nullable: true })
  salePrice: number | null;

  unitPrice: number;

  total: number;

  @Column()
  quantity!: number;

  @Column()
  imageUrl!: string;

  @ManyToOne(
    () => ShoppingCart,
    (shoppingCart) => shoppingCart.shoppingCartItems,
    { onDelete: "CASCADE" }
  )
  shoppingCart: ShoppingCart;

  @AfterLoad()
  setComputed() {
    this.unitPrice = this.salePrice ? this.salePrice : this.price;
    this.total = roundTo(this.unitPrice * this.quantity, 2);
  }
}
