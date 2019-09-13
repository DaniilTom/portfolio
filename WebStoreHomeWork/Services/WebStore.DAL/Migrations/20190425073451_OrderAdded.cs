using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.DAL.Migrations
{
    public partial class OrderAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MCDescriptions_Microcontrollers_ProductId",
                table: "MCDescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Microcontrollers_Categories_CategoryId",
                table: "Microcontrollers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Microcontrollers",
                table: "Microcontrollers");

            migrationBuilder.RenameTable(
                name: "Microcontrollers",
                newName: "ProductBase");

            migrationBuilder.RenameIndex(
                name: "IX_Microcontrollers_CategoryId",
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

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(nullable: true),
                    Contact = table.Column<string>(nullable: true),
                    TotalPrice = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_ProductBase_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_MCDescriptions_ProductBase_ProductId",
                table: "MCDescriptions",
                column: "ProductId",
                principalTable: "ProductBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBase_Categories_CategoryId",
                table: "ProductBase",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MCDescriptions_ProductBase_ProductId",
                table: "MCDescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductBase_Categories_CategoryId",
                table: "ProductBase");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductBase",
                table: "ProductBase");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "ProductBase");

            migrationBuilder.RenameTable(
                name: "ProductBase",
                newName: "Microcontrollers");

            migrationBuilder.RenameIndex(
                name: "IX_ProductBase_CategoryId",
                table: "Microcontrollers",
                newName: "IX_Microcontrollers_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Microcontrollers",
                table: "Microcontrollers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MCDescriptions_Microcontrollers_ProductId",
                table: "MCDescriptions",
                column: "ProductId",
                principalTable: "Microcontrollers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Microcontrollers_Categories_CategoryId",
                table: "Microcontrollers",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
