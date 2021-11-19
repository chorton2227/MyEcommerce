import {
  BaseEntity,
  Column,
  CreateDateColumn,
  Entity,
  OneToMany,
  PrimaryGeneratedColumn,
  UpdateDateColumn,
} from "typeorm";
import { ShoppingCartItem } from "./ShoppingCartItem";

@Entity()
export class ShoppingCart extends BaseEntity {
  @PrimaryGeneratedColumn()
  id!: string;

  @CreateDateColumn()
  createdAt: Date;

  @UpdateDateColumn()
  updatedAt: Date;

  @Column({ unique: true })
  userId!: string;

  @OneToMany(
    () => ShoppingCartItem,
    (shoppingCartItem) => shoppingCartItem.shoppingCart
  )
  shoppingCartItems: ShoppingCartItem[];
}
