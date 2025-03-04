using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SaleContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SaleNumber = table.Column<string>(type: "text", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsCancelled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sales_items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    PercentDiscount = table.Column<int>(type: "integer", nullable: false),
                    IsCancelled = table.Column<bool>(type: "boolean", nullable: false),
                    SaleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sales_items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sales_items_sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_Id",
                table: "products",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_products_Title",
                table: "products",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sales_Id",
                table: "sales",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sales_SaleNumber",
                table: "sales",
                column: "SaleNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sales_items_Id",
                table: "sales_items",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sales_items_ProductId",
                table: "sales_items",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_sales_items_SaleId",
                table: "sales_items",
                column: "SaleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "sales_items");

            migrationBuilder.DropTable(
                name: "sales");
        }
    }
}
