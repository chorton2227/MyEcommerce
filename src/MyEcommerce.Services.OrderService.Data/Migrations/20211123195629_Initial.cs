using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MyEcommerce.Services.OrderService.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "OrderService");

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                schema: "OrderService",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, defaultValue: "submitted")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "OrderService",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    BillingAddress_Street1 = table.Column<string>(type: "text", nullable: true),
                    BillingAddress_Street2 = table.Column<string>(type: "text", nullable: true),
                    BillingAddress_City = table.Column<string>(type: "text", nullable: true),
                    BillingAddress_State = table.Column<string>(type: "text", nullable: true),
                    BillingAddress_Country = table.Column<string>(type: "text", nullable: true),
                    BillingAddress_ZipCode = table.Column<string>(type: "text", nullable: true),
                    BillingAddress_OrderId = table.Column<string>(type: "text", nullable: true),
                    DeliveryAddress_Street1 = table.Column<string>(type: "text", nullable: true),
                    DeliveryAddress_Street2 = table.Column<string>(type: "text", nullable: true),
                    DeliveryAddress_City = table.Column<string>(type: "text", nullable: true),
                    DeliveryAddress_State = table.Column<string>(type: "text", nullable: true),
                    DeliveryAddress_Country = table.Column<string>(type: "text", nullable: true),
                    DeliveryAddress_ZipCode = table.Column<string>(type: "text", nullable: true),
                    DeliveryAddress_OrderId = table.Column<string>(type: "text", nullable: true),
                    OrderDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ChargeId = table.Column<string>(type: "text", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatuses_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "OrderService",
                        principalTable: "OrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                schema: "OrderService",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    OrderId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "OrderService",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                schema: "OrderService",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StatusId",
                schema: "OrderService",
                table: "Orders",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems",
                schema: "OrderService");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "OrderService");

            migrationBuilder.DropTable(
                name: "OrderStatuses",
                schema: "OrderService");
        }
    }
}
