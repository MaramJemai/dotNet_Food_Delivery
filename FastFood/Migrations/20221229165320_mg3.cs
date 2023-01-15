using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFood.Migrations
{
    public partial class mg3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                schema: "FFUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total = table.Column<float>(type: "real", nullable: false),
                    PayementMode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Users_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "FFUser",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Plate",
                schema: "FFUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meal",
                schema: "FFUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    PlateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meal_Order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "FFUser",
                        principalTable: "Order",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Meal_Plate_PlateId",
                        column: x => x.PlateId,
                        principalSchema: "FFUser",
                        principalTable: "Plate",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meal_OrderId",
                schema: "FFUser",
                table: "Meal",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Meal_PlateId",
                schema: "FFUser",
                table: "Meal",
                column: "PlateId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ClientId",
                schema: "FFUser",
                table: "Order",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meal",
                schema: "FFUser");

            migrationBuilder.DropTable(
                name: "Order",
                schema: "FFUser");

            migrationBuilder.DropTable(
                name: "Plate",
                schema: "FFUser");
        }
    }
}
