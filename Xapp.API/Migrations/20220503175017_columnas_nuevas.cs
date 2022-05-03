using Microsoft.EntityFrameworkCore.Migrations;

namespace Xapp.API.Migrations
{
    public partial class columnas_nuevas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "PTOs");

            migrationBuilder.AddColumn<bool>(
                name: "IsPTO",
                table: "PTOs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVacation",
                table: "PTOs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPTO",
                table: "PTOs");

            migrationBuilder.DropColumn(
                name: "IsVacation",
                table: "PTOs");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "PTOs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
