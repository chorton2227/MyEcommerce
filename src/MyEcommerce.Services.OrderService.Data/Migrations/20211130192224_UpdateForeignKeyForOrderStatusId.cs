using Microsoft.EntityFrameworkCore.Migrations;

namespace MyEcommerce.Services.OrderService.Data.Migrations
{
    public partial class UpdateForeignKeyForOrderStatusId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatuses_StatusId",
                schema: "OrderService",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                schema: "OrderService",
                table: "Orders",
                newName: "_orderStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_StatusId",
                schema: "OrderService",
                table: "Orders",
                newName: "IX_Orders__orderStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatuses__orderStatusId",
                schema: "OrderService",
                table: "Orders",
                column: "_orderStatusId",
                principalSchema: "OrderService",
                principalTable: "OrderStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatuses__orderStatusId",
                schema: "OrderService",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "_orderStatusId",
                schema: "OrderService",
                table: "Orders",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders__orderStatusId",
                schema: "OrderService",
                table: "Orders",
                newName: "IX_Orders_StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatuses_StatusId",
                schema: "OrderService",
                table: "Orders",
                column: "StatusId",
                principalSchema: "OrderService",
                principalTable: "OrderStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
