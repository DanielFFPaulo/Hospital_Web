using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utilizador_Pessoa_Pessoa_Id",
                table: "Utilizador");

            migrationBuilder.DropIndex(
                name: "IX_Utilizador_Pessoa_Id",
                table: "Utilizador");

            migrationBuilder.AlterColumn<int>(
                name: "Pessoa_Id",
                table: "Utilizador",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Nome_Utilizador",
                table: "Utilizador",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Utilizador_Pessoa_Id",
                table: "Utilizador",
                column: "Pessoa_Id",
                unique: true,
                filter: "[Pessoa_Id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Utilizador_Pessoa_Pessoa_Id",
                table: "Utilizador",
                column: "Pessoa_Id",
                principalTable: "Pessoa",
                principalColumn: "N_Processo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Utilizador_Pessoa_Pessoa_Id",
                table: "Utilizador");

            migrationBuilder.DropIndex(
                name: "IX_Utilizador_Pessoa_Id",
                table: "Utilizador");

            migrationBuilder.AlterColumn<int>(
                name: "Pessoa_Id",
                table: "Utilizador",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome_Utilizador",
                table: "Utilizador",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_Utilizador_Pessoa_Id",
                table: "Utilizador",
                column: "Pessoa_Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Utilizador_Pessoa_Pessoa_Id",
                table: "Utilizador",
                column: "Pessoa_Id",
                principalTable: "Pessoa",
                principalColumn: "N_Processo",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
