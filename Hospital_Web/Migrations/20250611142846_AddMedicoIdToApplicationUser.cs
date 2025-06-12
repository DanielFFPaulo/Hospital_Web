using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Web.Migrations
{
    /// <inheritdoc />
    public partial class AddMedicoIdToApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MedicoId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MedicoId",
                table: "AspNetUsers",
                column: "MedicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Medico_MedicoId",
                table: "AspNetUsers",
                column: "MedicoId",
                principalTable: "Medico",
                principalColumn: "N_Processo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Medico_MedicoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MedicoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MedicoId",
                table: "AspNetUsers");
        }
    }
}
