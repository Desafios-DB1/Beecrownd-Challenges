using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BatalhaDePokemons.Infra.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnPP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PP",
                table: "Ataques",
                newName: "QuantUsos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuantUsos",
                table: "Ataques",
                newName: "PP");
        }
    }
}
