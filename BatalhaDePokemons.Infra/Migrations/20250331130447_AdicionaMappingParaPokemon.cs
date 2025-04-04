using BatalhaDePokemons.Crosscutting.Constantes;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BatalhaDePokemons.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaMappingParaPokemon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: Entidades.Pokemons,
                newName: "PokemonId");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: Entidades.Pokemons,
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: Entidades.Pokemons,
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PokemonId",
                table: Entidades.Pokemons,
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: Entidades.Pokemons,
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: Entidades.Pokemons,
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);
        }
    }
}
