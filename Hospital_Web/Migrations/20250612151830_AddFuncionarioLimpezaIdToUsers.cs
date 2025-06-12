using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Web.Migrations
{
    /// <inheritdoc />
    public partial class AddFuncionarioLimpezaIdToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FuncionarioLimpezaId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FuncionarioLimpezaId",
                table: "AspNetUsers",
                column: "FuncionarioLimpezaId");

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

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FuncionarioLimpezaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FuncionarioLimpezaId",
                table: "AspNetUsers");
        }
    }
}
