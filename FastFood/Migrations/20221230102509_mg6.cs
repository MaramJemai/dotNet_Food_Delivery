using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastFood.Migrations
{
    public partial class mg6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "FFUser",
                table: "Order");

            migrationBuilder.AlterColumn<string>(
                name: "PayementMode",
                schema: "FFUser",
                table: "Order",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "State",
                schema: "FFUser",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                schema: "FFUser",
                table: "Order");

            migrationBuilder.AlterColumn<string>(
                name: "PayementMode",
                schema: "FFUser",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "FFUser",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
