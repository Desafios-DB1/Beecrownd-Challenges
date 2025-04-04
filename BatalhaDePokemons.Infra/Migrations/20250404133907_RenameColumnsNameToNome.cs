using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BatalhaDePokemons.Infra.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnsNameToNome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Ataques",
                newName: "Nome");
            
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Pokemons",
                newName: "Nome");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Ataques",
                newName: "Name");
            
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Pokemons",
                newName: "Name");
        }
    }
}
