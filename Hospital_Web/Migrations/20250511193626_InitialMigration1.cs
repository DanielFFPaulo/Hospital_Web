using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    N_Processo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Idade = table.Column<int>(type: "int", nullable: false),
                    Data_de_Nascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Morada = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Telefone1 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Telefone2 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NIF = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Cod_postal = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Localidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.N_Processo);
                });

            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bloco = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Andar = table.Column<int>(type: "int", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    Gabinete_Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Equipamento = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Disponivel = table.Column<bool>(type: "bit", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FuncionarioLimpeza",
                columns: table => new
                {
                    N_Processo = table.Column<int>(type: "int", nullable: false),
                    Turno = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Tamanho_Uniforme = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Data_de_contratacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Certificacoes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncionarioLimpeza", x => x.N_Processo);
                    table.ForeignKey(
                        name: "FK_FuncionarioLimpeza_Pessoa_N_Processo",
                        column: x => x.N_Processo,
                        principalTable: "Pessoa",
                        principalColumn: "N_Processo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medico",
                columns: table => new
                {
                    N_Processo = table.Column<int>(type: "int", nullable: false),
                    Especialidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Numero_de_ordem = table.Column<int>(type: "int", nullable: false),
                    Anos_de_experiencia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medico", x => x.N_Processo);
                    table.ForeignKey(
                        name: "FK_Medico_Pessoa_N_Processo",
                        column: x => x.N_Processo,
                        principalTable: "Pessoa",
                        principalColumn: "N_Processo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Utilizador",
                columns: table => new
                {
                    ID_Utilizador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome_Utilizador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Data_Criacao_Conta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pessoa_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizador", x => x.ID_Utilizador);
                    table.ForeignKey(
                        name: "FK_Utilizador_Pessoa_Pessoa_Id",
                        column: x => x.Pessoa_Id,
                        principalTable: "Pessoa",
                        principalColumn: "N_Processo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LimpezaSala",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Produto1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Produto2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Produto3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hora = table.Column<TimeSpan>(type: "time", nullable: false),
                    Funcionario_Id = table.Column<int>(type: "int", nullable: false),
                    Sala_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LimpezaSala", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LimpezaSala_FuncionarioLimpeza_Funcionario_Id",
                        column: x => x.Funcionario_Id,
                        principalTable: "FuncionarioLimpeza",
                        principalColumn: "N_Processo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LimpezaSala_Sala_Sala_Id",
                        column: x => x.Sala_Id,
                        principalTable: "Sala",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Utente",
                columns: table => new
                {
                    N_Processo = table.Column<int>(type: "int", nullable: false),
                    Estado_clinico = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Grupo_Sanguineo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Alergias = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Seguro_de_Saude = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Data_de_Registo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Medico_Associado_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utente", x => x.N_Processo);
                    table.ForeignKey(
                        name: "FK_Utente_Medico_Medico_Associado_Id",
                        column: x => x.Medico_Associado_Id,
                        principalTable: "Medico",
                        principalColumn: "N_Processo");
                    table.ForeignKey(
                        name: "FK_Utente_Pessoa_N_Processo",
                        column: x => x.N_Processo,
                        principalTable: "Pessoa",
                        principalColumn: "N_Processo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Consulta",
                columns: table => new
                {
                    Episodio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hora = table.Column<TimeSpan>(type: "time", nullable: false),
                    Diagnostico = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Tratamento = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Medico_Id = table.Column<int>(type: "int", nullable: false),
                    Utente_Id = table.Column<int>(type: "int", nullable: false),
                    Gabinete_Id = table.Column<int>(type: "int", nullable: false),
                    GabineteID = table.Column<int>(type: "int", nullable: true),
                    MedicoN_Processo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consulta", x => x.Episodio);
                    table.ForeignKey(
                        name: "FK_Consulta_Medico_MedicoN_Processo",
                        column: x => x.MedicoN_Processo,
                        principalTable: "Medico",
                        principalColumn: "N_Processo");
                    table.ForeignKey(
                        name: "FK_Consulta_Medico_Medico_Id",
                        column: x => x.Medico_Id,
                        principalTable: "Medico",
                        principalColumn: "N_Processo");
                    table.ForeignKey(
                        name: "FK_Consulta_Sala_GabineteID",
                        column: x => x.GabineteID,
                        principalTable: "Sala",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Consulta_Sala_Gabinete_Id",
                        column: x => x.Gabinete_Id,
                        principalTable: "Sala",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Consulta_Utente_Utente_Id",
                        column: x => x.Utente_Id,
                        principalTable: "Utente",
                        principalColumn: "N_Processo");
                });

            migrationBuilder.CreateTable(
                name: "Internamento",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data_Hora_Entrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data_Hora_Saida = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Utente_Id = table.Column<int>(type: "int", nullable: false),
                    Quarto_Id = table.Column<int>(type: "int", nullable: false),
                    Consulta_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Internamento", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Internamento_Consulta_Consulta_Id",
                        column: x => x.Consulta_Id,
                        principalTable: "Consulta",
                        principalColumn: "Episodio");
                    table.ForeignKey(
                        name: "FK_Internamento_Sala_Quarto_Id",
                        column: x => x.Quarto_Id,
                        principalTable: "Sala",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Internamento_Utente_Utente_Id",
                        column: x => x.Utente_Id,
                        principalTable: "Utente",
                        principalColumn: "N_Processo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consulta_Gabinete_Id",
                table: "Consulta",
                column: "Gabinete_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Consulta_GabineteID",
                table: "Consulta",
                column: "GabineteID");

            migrationBuilder.CreateIndex(
                name: "IX_Consulta_Medico_Id",
                table: "Consulta",
                column: "Medico_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Consulta_MedicoN_Processo",
                table: "Consulta",
                column: "MedicoN_Processo");

            migrationBuilder.CreateIndex(
                name: "IX_Consulta_Utente_Id",
                table: "Consulta",
                column: "Utente_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Internamento_Consulta_Id",
                table: "Internamento",
                column: "Consulta_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Internamento_Quarto_Id",
                table: "Internamento",
                column: "Quarto_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Internamento_Utente_Id",
                table: "Internamento",
                column: "Utente_Id");

            migrationBuilder.CreateIndex(
                name: "IX_LimpezaSala_Funcionario_Id",
                table: "LimpezaSala",
                column: "Funcionario_Id");

            migrationBuilder.CreateIndex(
                name: "IX_LimpezaSala_Sala_Id",
                table: "LimpezaSala",
                column: "Sala_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Utente_Medico_Associado_Id",
                table: "Utente",
                column: "Medico_Associado_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Utilizador_Pessoa_Id",
                table: "Utilizador",
                column: "Pessoa_Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Internamento");

            migrationBuilder.DropTable(
                name: "LimpezaSala");

            migrationBuilder.DropTable(
                name: "Utilizador");

            migrationBuilder.DropTable(
                name: "Consulta");

            migrationBuilder.DropTable(
                name: "FuncionarioLimpeza");

            migrationBuilder.DropTable(
                name: "Sala");

            migrationBuilder.DropTable(
                name: "Utente");

            migrationBuilder.DropTable(
                name: "Medico");

            migrationBuilder.DropTable(
                name: "Pessoa");
        }
    }
}
