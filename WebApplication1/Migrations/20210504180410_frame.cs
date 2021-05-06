using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.PresentationLayer.Migrations
{
    public partial class frame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Frame",
                table: "Movies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Frame",
                table: "Movies");
        }
    }
}
