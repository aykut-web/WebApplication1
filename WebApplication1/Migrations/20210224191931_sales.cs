using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.PresentationLayer.Migrations
{
    public partial class sales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sales",
                table: "Movies",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sales",
                table: "Movies");
        }
    }
}
