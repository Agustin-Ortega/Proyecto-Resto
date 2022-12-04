using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Resto.Migrations
{
    public partial class segunda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Platos_Menus_idMenu",
                table: "Platos");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Clientes_idCliente",
                table: "Reservas");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Menus_idMenu",
                table: "Reservas");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Restaurantes_idRestaurante",
                table: "Reservas");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_idMenu",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Platos_idMenu",
                table: "Platos");

            migrationBuilder.DropColumn(
                name: "idMenu",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "idMenu",
                table: "Platos");

            migrationBuilder.AlterColumn<int>(
                name: "idRestaurante",
                table: "Reservas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "idCliente",
                table: "Reservas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "Reservas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Platos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ItemReservas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemProcesado = table.Column<bool>(type: "bit", nullable: true),
                    idReserva = table.Column<int>(type: "int", nullable: true),
                    idPlato = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemReservas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemReservas_Platos_idPlato",
                        column: x => x.idPlato,
                        principalTable: "Platos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemReservas_Reservas_idReserva",
                        column: x => x.idReserva,
                        principalTable: "Reservas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemReservas_idPlato",
                table: "ItemReservas",
                column: "idPlato");

            migrationBuilder.CreateIndex(
                name: "IX_ItemReservas_idReserva",
                table: "ItemReservas",
                column: "idReserva");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Clientes_idCliente",
                table: "Reservas",
                column: "idCliente",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Restaurantes_idRestaurante",
                table: "Reservas",
                column: "idRestaurante",
                principalTable: "Restaurantes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Clientes_idCliente",
                table: "Reservas");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Restaurantes_idRestaurante",
                table: "Reservas");

            migrationBuilder.DropTable(
                name: "ItemReservas");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Platos");

            migrationBuilder.AlterColumn<int>(
                name: "idRestaurante",
                table: "Reservas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "idCliente",
                table: "Reservas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "idMenu",
                table: "Reservas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idMenu",
                table: "Platos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    importeTotal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_idMenu",
                table: "Reservas",
                column: "idMenu");

            migrationBuilder.CreateIndex(
                name: "IX_Platos_idMenu",
                table: "Platos",
                column: "idMenu");

            migrationBuilder.AddForeignKey(
                name: "FK_Platos_Menus_idMenu",
                table: "Platos",
                column: "idMenu",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Clientes_idCliente",
                table: "Reservas",
                column: "idCliente",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Menus_idMenu",
                table: "Reservas",
                column: "idMenu",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Restaurantes_idRestaurante",
                table: "Reservas",
                column: "idRestaurante",
                principalTable: "Restaurantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
