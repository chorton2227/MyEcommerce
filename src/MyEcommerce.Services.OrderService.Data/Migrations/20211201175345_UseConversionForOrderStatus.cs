using Microsoft.EntityFrameworkCore.Migrations;

namespace MyEcommerce.Services.OrderService.Data.Migrations
{
    public partial class UseConversionForOrderStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatuses__orderStatusId",
                schema: "OrderService",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders__orderStatusId",
                schema: "OrderService",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "_orderStatusId",
                schema: "OrderService",
                table: "Orders",
                newName: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "OrderService",
                table: "Orders",
                newName: "_orderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders__orderStatusId",
                schema: "OrderService",
                table: "Orders",
                column: "_orderStatusId");

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
    }
}
