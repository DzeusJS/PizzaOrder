using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PizzaOrder.Data.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TotalOrderCost = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "Pizza",
                columns: table => new
                {
                    PizzaId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Size = table.Column<string>(type: "TEXT", nullable: false),
                    TotalPizzaCost = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pizza", x => x.PizzaId);
                });

            migrationBuilder.CreateTable(
                name: "Topping",
                columns: table => new
                {
                    ToppingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ToppingName = table.Column<string>(type: "TEXT", nullable: false),
                    ToppingPrice = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topping", x => x.ToppingId);
                });

            migrationBuilder.CreateTable(
                name: "Pizza_Order",
                columns: table => new
                {
                    PizzaId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pizza_Order", x => new { x.OrderId, x.PizzaId });
                    table.ForeignKey(
                        name: "FK_Pizza_Order_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pizza_Order_Pizza_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "Pizza",
                        principalColumn: "PizzaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pizza_Topping",
                columns: table => new
                {
                    PizzaId = table.Column<int>(type: "INTEGER", nullable: false),
                    ToppingId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pizza_Topping", x => new { x.PizzaId, x.ToppingId });
                    table.ForeignKey(
                        name: "FK_Pizza_Topping_Pizza_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "Pizza",
                        principalColumn: "PizzaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pizza_Topping_Topping_ToppingId",
                        column: x => x.ToppingId,
                        principalTable: "Topping",
                        principalColumn: "ToppingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "TotalOrderCost" },
                values: new object[,]
                {
                    { 1, 20.0 },
                    { 2, 22.0 },
                    { 3, 18.0 }
                });

            migrationBuilder.InsertData(
                table: "Pizza",
                columns: new[] { "PizzaId", "Size", "TotalPizzaCost" },
                values: new object[,]
                {
                    { 1, "Small", 8.0 },
                    { 2, "Medium", 10.0 },
                    { 3, "Large", 12.0 }
                });

            migrationBuilder.InsertData(
                table: "Topping",
                columns: new[] { "ToppingId", "ToppingName", "ToppingPrice" },
                values: new object[,]
                {
                    { 1, "Mushrooms", 1.0 },
                    { 2, "Peppers", 1.0 },
                    { 3, "Cucumbers", 1.0 },
                    { 4, "Olives", 1.0 },
                    { 5, "Onions", 1.0 }
                });

            migrationBuilder.InsertData(
                table: "Pizza_Order",
                columns: new[] { "OrderId", "PizzaId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 3 },
                    { 2, 2 },
                    { 2, 3 },
                    { 3, 1 },
                    { 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Pizza_Topping",
                columns: new[] { "PizzaId", "ToppingId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 2, 3 },
                    { 2, 4 },
                    { 3, 3 },
                    { 3, 4 },
                    { 3, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pizza_Order_PizzaId",
                table: "Pizza_Order",
                column: "PizzaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pizza_Topping_ToppingId",
                table: "Pizza_Topping",
                column: "ToppingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pizza_Order");

            migrationBuilder.DropTable(
                name: "Pizza_Topping");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Pizza");

            migrationBuilder.DropTable(
                name: "Topping");
        }
    }
}
