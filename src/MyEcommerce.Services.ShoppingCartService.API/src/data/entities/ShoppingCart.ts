import roundTo from "../../utils/roundTo";
import {
  AfterInsert,
  AfterLoad,
  AfterUpdate,
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

  subtotal: number;

  @OneToMany(
    () => ShoppingCartItem,
    (shoppingCartItem) => shoppingCartItem.shoppingCart
  )
  shoppingCartItems: ShoppingCartItem[];

  public calculate(): void {
    if (this.shoppingCartItems) {
      this.subtotal = roundTo(
        this.shoppingCartItems.reduce<number>(
          (acc, item) => item.total + acc,
          0
        ),
        2
      );
    } else {
      this.shoppingCartItems = [];
      this.subtotal = 0;
    }
  }

  @AfterLoad()
  @AfterInsert()
  @AfterUpdate()
  Recalculate() {
    this.calculate();
  }
}
