import {MigrationInterface, QueryRunner} from "typeorm";

export class AddCascadeOnDelete1642620770034 implements MigrationInterface {
    name = 'AddCascadeOnDelete1642620770034'

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "shopping_cart_item" DROP CONSTRAINT "FK_74abdeac2e0e8ecf7d2294641f4"`);
        await queryRunner.query(`ALTER TABLE "shopping_cart_item" ADD CONSTRAINT "FK_74abdeac2e0e8ecf7d2294641f4" FOREIGN KEY ("shoppingCartId") REFERENCES "shopping_cart"("id") ON DELETE CASCADE ON UPDATE NO ACTION`);
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`ALTER TABLE "shopping_cart_item" DROP CONSTRAINT "FK_74abdeac2e0e8ecf7d2294641f4"`);
        await queryRunner.query(`ALTER TABLE "shopping_cart_item" ADD CONSTRAINT "FK_74abdeac2e0e8ecf7d2294641f4" FOREIGN KEY ("shoppingCartId") REFERENCES "shopping_cart"("id") ON DELETE NO ACTION ON UPDATE NO ACTION`);
    }

}
