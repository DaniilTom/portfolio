using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.DAL.Migrations
{
    public partial class Rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MCDescriptions_ProductBase_ProductId",
                table: "MCDescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductBase_ProductId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductBase_Categories_CategoryId",
                table: "ProductBase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductBase",
                table: "ProductBase");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "ProductBase");

            migrationBuilder.RenameTable(
                name: "ProductBase",
                newName: "Products");

            migrationBuilder.RenameIndex(
                name: "IX_ProductBase_CategoryId",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Products",
                table: "Products",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MCDescriptions_Products_ProductId",
                table: "MCDescriptions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MCDescriptions_Products_ProductId",
                table: "MCDescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Products",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "ProductBase");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "ProductBase",
                newName: "IX_ProductBase_CategoryId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "ProductBase",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductBase",
                table: "ProductBase",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MCDescriptions_ProductBase_ProductId",
                table: "MCDescriptions",
                column: "ProductId",
                principalTable: "ProductBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductBase_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "ProductBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBase_Categories_CategoryId",
                table: "ProductBase",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
