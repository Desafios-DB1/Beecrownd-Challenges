using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BatalhaDePokemons.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaDataCriacaoENovasTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataHoraCriacao",
                table: "Pokemons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHoraCriacao",
                table: "Ataques",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Batalhas",
                columns: table => new
                {
                    BatalhaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Pokemon1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Pokemon2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VencedorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsFinalizada = table.Column<bool>(type: "bit", nullable: false),
                    ProximoTurnoDoPokemonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batalhas", x => x.BatalhaId);
                });

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    TurnoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BatalhaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroTurno = table.Column<int>(type: "int", nullable: false),
                    AtacanteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlvoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AtaqueUtilizadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DanoCausado = table.Column<int>(type: "int", nullable: false),
                    DataHoraCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.TurnoId);
                    table.ForeignKey(
                        name: "FK_Turnos_Batalhas_BatalhaId",
                        column: x => x.BatalhaId,
                        principalTable: "Batalhas",
                        principalColumn: "BatalhaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Turnos_BatalhaId",
                table: "Turnos",
                column: "BatalhaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropTable(
                name: "Batalhas");

            migrationBuilder.DropColumn(
                name: "DataHoraCriacao",
                table: "Pokemons");

            migrationBuilder.DropColumn(
                name: "DataHoraCriacao",
                table: "Ataques");
        }
    }
}
