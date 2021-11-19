import {MigrationInterface, QueryRunner} from "typeorm";

export class Initial1637084125871 implements MigrationInterface {
    name = 'Initial1637084125871'

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`CREATE TABLE "shopping_cart_item" ("id" SERIAL NOT NULL, "createdAt" TIMESTAMP NOT NULL DEFAULT now(), "updatedAt" TIMESTAMP NOT NULL DEFAULT now(), "productId" character varying NOT NULL, "name" character varying NOT NULL, "price" numeric(12,2) NOT NULL, "salePrice" numeric(12,2), "quantity" integer NOT NULL, "imageUrl" character varying NOT NULL, "shoppingCartId" integer, CONSTRAINT "PK_15909d00f68f8f022e5545745aa" PRIMARY KEY ("id"))`);
        await queryRunner.query(`CREATE TABLE "shopping_cart" ("id" SERIAL NOT NULL, "createdAt" TIMESTAMP NOT NULL DEFAULT now(), "updatedAt" TIMESTAMP NOT NULL DEFAULT now(), "userId" character varying NOT NULL, CONSTRAINT "UQ_bee83828c1e181ac7ba97267ca2" UNIQUE ("userId"), CONSTRAINT "PK_40f9358cdf55d73d8a2ad226592" PRIMARY KEY ("id"))`);
        await queryRunner.query(`ALTER TABLE "shopping_cart_item" ADD CONSTRAINT "FK_74abdeac2e0e8ecf7d2294641f4" FOREIGN KEY ("shoppingCartId") REFERENCES "shopping_cart"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "shopping_cart_item" DROP CONSTRAINT "FK_74abdeac2e0e8ecf7d2294641f4"`);
        await queryRunner.query(`DROP TABLE "shopping_cart"`);
        await queryRunner.query(`DROP TABLE "shopping_cart_item"`);
    }

}
