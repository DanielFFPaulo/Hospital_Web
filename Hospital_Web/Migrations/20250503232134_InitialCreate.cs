using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Funcionario",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Idade = table.Column<int>(type: "int", nullable: true),
                    Morada = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Telefone1 = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Telefone2 = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Licenciatura = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado_Civil = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NIF = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionario", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Gabinete",
                columns: table => new
                {
                    N_Identificador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Equipamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Disponivel = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gabinete", x => x.N_Identificador);
                });

            migrationBuilder.CreateTable(
                name: "QuartoInternagem",
                columns: table => new
                {
                    N_Identificador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacidade = table.Column<int>(type: "int", nullable: true),
                    Ocupado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuartoInternagem", x => x.N_Identificador);
                });

            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bloco = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Andar = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Utente",
                columns: table => new
                {
                    N_Processo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Idade = table.Column<int>(type: "int", nullable: true),
                    Data_Nascimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Morada = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Estado_Clinico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone1 = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Telefone2 = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NIF = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utente", x => x.N_Processo);
                });

            migrationBuilder.CreateTable(
                name: "FuncionarioLimpeza",
                columns: table => new
                {
                    ID_Funcionario = table.Column<int>(type: "int", nullable: false),
                    Turno = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncionarioLimpeza", x => x.ID_Funcionario);
                    table.ForeignKey(
                        name: "FK_FuncionarioLimpeza_Funcionario_ID_Funcionario",
                        column: x => x.ID_Funcionario,
                        principalTable: "Funcionario",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medico",
                columns: table => new
                {
                    ID_Medico = table.Column<int>(type: "int", nullable: false),
                    Especialidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medico", x => x.ID_Medico);
                    table.ForeignKey(
                        name: "FK_Medico_Funcionario_ID_Medico",
                        column: x => x.ID_Medico,
                        principalTable: "Funcionario",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LimpezaSala",
                columns: table => new
                {
                    Funcionario = table.Column<int>(type: "int", nullable: false),
                    Sala = table.Column<int>(type: "int", nullable: false),
                    Produto1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Produto2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Produto3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Hora = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LimpezaSala", x => new { x.Funcionario, x.Sala });
                    table.ForeignKey(
                        name: "FK_LimpezaSala_Funcionario_Funcionario",
                        column: x => x.Funcionario,
                        principalTable: "Funcionario",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LimpezaSala_Sala_Sala",
                        column: x => x.Sala,
                        principalTable: "Sala",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UtenteQuarto",
                columns: table => new
                {
                    Utente = table.Column<int>(type: "int", nullable: false),
                    Quarto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UtenteQuarto", x => new { x.Utente, x.Quarto });
                    table.ForeignKey(
                        name: "FK_UtenteQuarto_QuartoInternagem_Quarto",
                        column: x => x.Quarto,
                        principalTable: "QuartoInternagem",
                        principalColumn: "N_Identificador",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UtenteQuarto_Utente_Utente",
                        column: x => x.Utente,
                        principalTable: "Utente",
                        principalColumn: "N_Processo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consulta",
                columns: table => new
                {
                    Episodio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Hora = table.Column<TimeSpan>(type: "time", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID_Medico = table.Column<int>(type: "int", nullable: false),
                    ID_Utente = table.Column<int>(type: "int", nullable: false),
                    Gabinete = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consulta", x => x.Episodio);
                    table.ForeignKey(
                        name: "FK_Consulta_Gabinete_Gabinete",
                        column: x => x.Gabinete,
                        principalTable: "Gabinete",
                        principalColumn: "N_Identificador",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consulta_Medico_ID_Medico",
                        column: x => x.ID_Medico,
                        principalTable: "Medico",
                        principalColumn: "ID_Medico",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consulta_Utente_ID_Utente",
                        column: x => x.ID_Utente,
                        principalTable: "Utente",
                        principalColumn: "N_Processo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistoClinico",
                columns: table => new
                {
                    Episodio = table.Column<int>(type: "int", nullable: false),
                    Utente = table.Column<int>(type: "int", nullable: false),
                    Medico = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Diagnostico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tratamento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistoClinico", x => x.Episodio);
                    table.ForeignKey(
                        name: "FK_RegistoClinico_Consulta_Episodio",
                        column: x => x.Episodio,
                        principalTable: "Consulta",
                        principalColumn: "Episodio");
                    table.ForeignKey(
                        name: "FK_RegistoClinico_Medico_Medico",
                        column: x => x.Medico,
                        principalTable: "Medico",
                        principalColumn: "ID_Medico");
                    table.ForeignKey(
                        name: "FK_RegistoClinico_Utente_Utente",
                        column: x => x.Utente,
                        principalTable: "Utente",
                        principalColumn: "N_Processo");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consulta_Gabinete",
                table: "Consulta",
                column: "Gabinete");

            migrationBuilder.CreateIndex(
                name: "IX_Consulta_ID_Medico",
                table: "Consulta",
                column: "ID_Medico");

            migrationBuilder.CreateIndex(
                name: "IX_Consulta_ID_Utente",
                table: "Consulta",
                column: "ID_Utente");

            migrationBuilder.CreateIndex(
                name: "IX_LimpezaSala_Sala",
                table: "LimpezaSala",
                column: "Sala");

            migrationBuilder.CreateIndex(
                name: "IX_RegistoClinico_Medico",
                table: "RegistoClinico",
                column: "Medico");

            migrationBuilder.CreateIndex(
                name: "IX_RegistoClinico_Utente",
                table: "RegistoClinico",
                column: "Utente");

            migrationBuilder.CreateIndex(
                name: "IX_UtenteQuarto_Quarto",
                table: "UtenteQuarto",
                column: "Quarto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuncionarioLimpeza");

            migrationBuilder.DropTable(
                name: "LimpezaSala");

            migrationBuilder.DropTable(
                name: "RegistoClinico");

            migrationBuilder.DropTable(
                name: "UtenteQuarto");

            migrationBuilder.DropTable(
                name: "Sala");

            migrationBuilder.DropTable(
                name: "Consulta");

            migrationBuilder.DropTable(
                name: "QuartoInternagem");

            migrationBuilder.DropTable(
                name: "Gabinete");

            migrationBuilder.DropTable(
                name: "Medico");

            migrationBuilder.DropTable(
                name: "Utente");

            migrationBuilder.DropTable(
                name: "Funcionario");
        }
    }
}
