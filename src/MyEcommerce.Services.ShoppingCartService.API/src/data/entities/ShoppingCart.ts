import roundTo from "../../utils/roundTo";
import {
  AfterLoad,
  BaseEntity,
  BeforeInsert,
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

  subtotal: number;

  @OneToMany(
    () => ShoppingCartItem,
    (shoppingCartItem) => shoppingCartItem.shoppingCart
  )
  shoppingCartItems: ShoppingCartItem[];

  @AfterLoad()
  OnAfterLoad() {
    if (this.shoppingCartItems) {
      this.subtotal = roundTo(
        this.shoppingCartItems.reduce<number>(
          (acc, item) => item.total + acc,
          0
        ),
        2
      );
    }
  }

  @BeforeInsert()
  OnBeforeInsert() {
    if (!this.shoppingCartItems) {
      this.shoppingCartItems = [];
      this.subtotal = 0;
    }
  }
}
