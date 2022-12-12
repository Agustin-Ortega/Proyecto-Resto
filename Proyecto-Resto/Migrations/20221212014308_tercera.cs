using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Resto.Migrations
{
    public partial class tercera : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isProcessed",
                table: "Reservas",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isProcessed",
                table: "Reservas");
        }
    }
}
