using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Web.Migrations
{
    /// <inheritdoc />
    public partial class SexAddition2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grupo_Sanguineo",
                table: "Utente");

            migrationBuilder.AddColumn<int>(
                name: "Grupo_Sanguineo",
                table: "Pessoa",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grupo_Sanguineo",
                table: "Pessoa");

            migrationBuilder.AddColumn<int>(
                name: "Grupo_Sanguineo",
                table: "Utente",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
