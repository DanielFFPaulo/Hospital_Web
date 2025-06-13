using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Web.Migrations
{
    /// <inheritdoc />
    public partial class CorrigirCampoGeneroParaSexo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sexo",
                table: "Pessoa",
                newName: "genero");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "genero",
                table: "Pessoa",
                newName: "sexo");
        }
    }
}
