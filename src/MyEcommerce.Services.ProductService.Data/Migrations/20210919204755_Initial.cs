using Microsoft.EntityFrameworkCore.Migrations;

namespace MyEcommerce.Services.ProductService.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ProductService");

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "ProductService",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvailableStock = table.Column<long>(type: "bigint", nullable: false),
                    RestockTreshold = table.Column<long>(type: "bigint", nullable: false),
                    MaxStockThreshold = table.Column<long>(type: "bigint", nullable: false),
                    OnReorder = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductBrands",
                schema: "ProductService",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBrands", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductBrands_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "ProductService",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                schema: "ProductService",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductTypes_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "ProductService",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductBrands",
                schema: "ProductService");

            migrationBuilder.DropTable(
                name: "ProductTypes",
                schema: "ProductService");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "ProductService");
        }
    }
}
