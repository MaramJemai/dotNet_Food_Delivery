using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFood.Migrations
{
    public partial class mg8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Favorite",
                schema: "FFUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PlateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorite_Plate_PlateId",
                        column: x => x.PlateId,
                        principalSchema: "FFUser",
                        principalTable: "Plate",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Favorite_Users_ClientId",
                        column: x => x.ClientId,
                        principalSchema: "FFUser",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_ClientId",
                schema: "FFUser",
                table: "Favorite",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_PlateId",
                schema: "FFUser",
                table: "Favorite",
                column: "PlateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favorite",
                schema: "FFUser");
        }
    }
}
