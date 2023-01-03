using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class mig7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Colors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Brands",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Brands");
        }
    }
}
