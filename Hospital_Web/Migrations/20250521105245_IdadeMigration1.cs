using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Web.Migrations
{
    /// <inheritdoc />
    public partial class IdadeMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Idade",
                table: "Pessoa");

            migrationBuilder.RenameColumn(
                name: "Data_de_Nascimento",
                table: "Pessoa",
                newName: "DataDeNascimento");

            migrationBuilder.RenameColumn(
                name: "Data_Hora_Saida",
                table: "Internamento",
                newName: "DataHoraSaida");

            migrationBuilder.RenameColumn(
                name: "Data_Hora_Entrada",
                table: "Internamento",
                newName: "DataHoraEntrada");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataDeNascimento",
                table: "Pessoa",
                newName: "Data_de_Nascimento");

            migrationBuilder.RenameColumn(
                name: "DataHoraSaida",
                table: "Internamento",
                newName: "Data_Hora_Saida");

            migrationBuilder.RenameColumn(
                name: "DataHoraEntrada",
                table: "Internamento",
                newName: "Data_Hora_Entrada");

            migrationBuilder.AddColumn<int>(
                name: "Idade",
                table: "Pessoa",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
