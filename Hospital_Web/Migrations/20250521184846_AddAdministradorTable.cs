using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Web.Migrations
{
    /// <inheritdoc />
    public partial class AddAdministradorTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AspNetUserId",
                table: "Utilizador",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Administrador",
                columns: table => new
                {
                    N_Processo = table.Column<int>(type: "int", nullable: false),
                    Departamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Funcao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrador", x => x.N_Processo);
                    table.ForeignKey(
                        name: "FK_Administrador_Pessoa_N_Processo",
                        column: x => x.N_Processo,
                        principalTable: "Pessoa",
                        principalColumn: "N_Processo",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrador");

            migrationBuilder.DropColumn(
                name: "AspNetUserId",
                table: "Utilizador");
        }
    }
}
