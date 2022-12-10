using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Resto.Migrations
{
    public partial class segunda22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Restaurantes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Restaurantes");
        }
    }
}
