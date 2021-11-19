import {MigrationInterface, QueryRunner} from "typeorm";

export class Initial1637082721829 implements MigrationInterface {
    name = 'Initial1637082721829'

    public async up(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`CREATE TABLE "shopping_cart" ("id" SERIAL NOT NULL, "createdAt" TIMESTAMP NOT NULL DEFAULT now(), "updatedAt" TIMESTAMP NOT NULL DEFAULT now(), "userId" character varying NOT NULL, CONSTRAINT "UQ_bee83828c1e181ac7ba97267ca2" UNIQUE ("userId"), CONSTRAINT "PK_40f9358cdf55d73d8a2ad226592" PRIMARY KEY ("id"))`);
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
        await queryRunner.query(`DROP TABLE "shopping_cart"`);
    }

}
