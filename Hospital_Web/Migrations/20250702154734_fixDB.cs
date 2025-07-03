using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Web.Migrations
{
    /// <inheritdoc />
    public partial class fixDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FuncionarioLimpeza_FuncionarioLimpezaId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FuncionarioLimpeza_FuncionarioLimpezaId",
                table: "AspNetUsers",
                column: "FuncionarioLimpezaId",
                principalTable: "FuncionarioLimpeza",
                principalColumn: "N_Processo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FuncionarioLimpeza_FuncionarioLimpezaId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FuncionarioLimpeza_FuncionarioLimpezaId",
                table: "AspNetUsers",
                column: "FuncionarioLimpezaId",
                principalTable: "FuncionarioLimpeza",
                principalColumn: "N_Processo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
