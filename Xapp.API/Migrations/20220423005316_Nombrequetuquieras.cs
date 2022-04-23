using Microsoft.EntityFrameworkCore.Migrations;

namespace Xapp.API.Migrations
{
    public partial class Nombrequetuquieras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "User",
                table: "Skills",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User",
                table: "Skills");
        }
    }
}
