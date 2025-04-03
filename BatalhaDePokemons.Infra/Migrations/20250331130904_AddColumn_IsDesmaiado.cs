using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BatalhaDePokemons.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddColumn_IsDesmaiado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDesmaiado",
                table: "Pokemons",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDesmaiado",
                table: "Pokemons");
        }
    }
}
