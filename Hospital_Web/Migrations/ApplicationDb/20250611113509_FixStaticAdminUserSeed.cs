using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Web.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class FixStaticAdminUserSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "admin",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "STATIC-CONCURRENCY-STAMP-5678", "AQAAAAIAAYagAAAAEJRAyJ3o3op6t8rxDtx+xUHAk0QEBrwldcG7zIylR2tnJDKTNf9jf+tGeKuzChiDrg==", "STATIC-SECURITY-STAMP-1234" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "admin",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "28b13665-f83f-4b40-acf8-647e3ed91126", "AQAAAAIAAYagAAAAEIDFAWsNplQ7+unLuVDCLc6aPGgQhuZ9ZmJPqIfJszF6C7PSvN/BvgxGFZvxa3pyaA==", "7317f3d5-1a1a-4ead-9b4e-f188cf409832" });
        }
    }
}
