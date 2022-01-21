using Microsoft.EntityFrameworkCore.Migrations;

namespace MyEcommerce.Services.OrderService.Data.Migrations
{
    public partial class RemoveBillingAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillingAddress_City",
                schema: "OrderService",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BillingAddress_Country",
                schema: "OrderService",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BillingAddress_State",
                schema: "OrderService",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BillingAddress_Street1",
                schema: "OrderService",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BillingAddress_Street2",
                schema: "OrderService",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BillingAddress_ZipCode",
                schema: "OrderService",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_City",
                schema: "OrderService",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_Country",
                schema: "OrderService",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_State",
                schema: "OrderService",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_Street1",
                schema: "OrderService",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_Street2",
                schema: "OrderService",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BillingAddress_ZipCode",
                schema: "OrderService",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
