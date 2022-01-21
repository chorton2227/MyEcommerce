using Microsoft.EntityFrameworkCore.Migrations;

namespace MyEcommerce.Services.OrderService.Data.Migrations
{
    public partial class AddEmailFirstNameLastName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress_FirstName",
                schema: "OrderService",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress_LastName",
                schema: "OrderService",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "OrderService",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryAddress_FirstName",
                schema: "OrderService",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress_LastName",
                schema: "OrderService",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "OrderService",
                table: "Orders");
        }
    }
}
