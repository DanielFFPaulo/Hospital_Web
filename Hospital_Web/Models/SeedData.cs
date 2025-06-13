using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Hospital_Web.Data;
using System;
using System.Linq;
using static Hospital_Web.Models.Utente;

namespace Hospital_Web.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new Hospital_WebContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<Hospital_WebContext>>()))
        {
            if (context.Utente.Any())
            {
                return;   // DB has been seeded
            }
            context.Utente.AddRange(
                new Utente
                {
                    Estado_clinico = "O paciente tem o mindinho do pé completamente roxo após ter batido na mobilia dentro de casa",
                    Alergias = "Amendoins, Pelo de gato, borracha sintetica",
                    Seguro_de_Saude = "Fidelidade",
                    Data_de_Registo = DateTime.Parse("2025-04-16"),

                    Nome = "João Silva",
                    Morada = "Rua das Flores, 123, Lisboa",
                    Telemovel = "912345678",
                    TelemovelAlt = "919876543",
                    Email = "email@email.com",
                    NIF = "257029345",
                    Cod_Postal = "1000-123 LISBOA",
                    Localidade = "Lisboa",
                    genero = Pessoa.Género.Masculino,
                    DataDeNascimento = DateTime.Parse("1990-04-16"),
                    Grupo_Sanguineo = Utente.GrupoSanguineo.O_Positivo
                },

                new Utente
                {
                    Estado_clinico = "Paciente com queixas de dores de cabeça frequentes e insónia.",
                    Grupo_Sanguineo = Utente.GrupoSanguineo.A_Positivo,
                    Alergias = "Pólen",
                    Seguro_de_Saude = "Multicare",
                    Nome = "Maria Ferreira",
                    Data_de_Registo = DateTime.Parse("2025-04-18"),
                    DataDeNascimento = DateTime.Parse("1985-03-02"),
                    Morada = "Avenida da Liberdade, 45, Lisboa",
                    Telemovel = "913223344",
                    TelemovelAlt = "918112233",
                    Email = "maria.ferreira@email.com",
                    NIF = "256789123",
                    Cod_Postal = "1250-096 LISBOA",
                    Localidade = "Lisboa",
                    genero = Pessoa.Género.Feminino,
                },
                new Utente
                {
                    Estado_clinico = "Relatou dores abdominais persistentes há mais de uma semana.",
                    Grupo_Sanguineo = Utente.GrupoSanguineo.B_Positivo,
                    Alergias = "Nenhuma conhecida",
                    Seguro_de_Saude = "Médis",
                    Nome = "Carlos Gomes",
                    Data_de_Registo = DateTime.Parse("2025-04-17"),
                    DataDeNascimento = DateTime.Parse("1978-07-25"),
                    Morada = "Rua do Sol, 8, Porto",
                    Telemovel = "911234567",
                    TelemovelAlt = "910000001",
                    Email = "carlos.gomes@email.com",
                    NIF = "234567890",
                    Cod_Postal = "4000-123 PORTO",
                    Localidade = "Porto",
                    genero = Pessoa.Género.Masculino,
                },
                new Utente
                {
                    Estado_clinico = "Paciente com asma moderada, em tratamento controlado.",
                    Grupo_Sanguineo = Utente.GrupoSanguineo.AB_Positivo,
                    Alergias = "Poeira, Ácaros",
                    Seguro_de_Saude = "AdvanceCare",
                    Nome = "Ana Costa",
                    Data_de_Registo = DateTime.Parse("2025-04-15"),
                    DataDeNascimento = DateTime.Parse("2001-11-30"),
                    Morada = "Travessa do Parque, 12, Coimbra",
                    Telemovel = "917654321",
                    TelemovelAlt = "912345000",
                    Email = "ana.costa@email.com",
                    NIF = "278901234",
                    Cod_Postal = "3000-456 COIMBRA",
                    Localidade = "Coimbra",
                    genero = Pessoa.Género.Feminino,
                }
            );



            if (context.Medico.Any())
            {
                return;   // DB has been seeded
            }
            context.Medico.AddRange(
                new Medico
                {
                    Especialidade = "Neurologia",
                    Numero_de_ordem = "20083421",
                    Anos_de_experiencia = 15,

                    Nome = "Inês Moura",
                    Morada = "Rua da Ciência, 45, Porto",
                    Telemovel = "965332178",
                    TelemovelAlt = "910223344",
                    Email = "inesmoura@hospital.tomar.pt",
                    NIF = "278902134",
                    Cod_Postal = "4100-123 PORTO",
                    Localidade = "Porto",
                    genero = Pessoa.Género.Feminino,
                    DataDeNascimento = DateTime.Parse("1983-08-12"),
                    Grupo_Sanguineo = Pessoa.GrupoSanguineo.B_Positivo
                },
                new Medico
                {
                    Especialidade = "Ortopedia",
                    Numero_de_ordem = "18827412",
                    Anos_de_experiencia = 8,
            
                    Nome = "Luís Martins",
                    Morada = "Alameda dos Oceanos, 220, Lisboa",
                    Telemovel = "962884112",
                    TelemovelAlt = "915445566",
                    Email = "luismartins@hospital.tomar.pt",
                    NIF = "287654321",
                    Cod_Postal = "1990-423 LISBOA",
                    Localidade = "Lisboa",
                    genero = Pessoa.Género.Masculino,
                    DataDeNascimento = DateTime.Parse("1986-05-17"),
                    Grupo_Sanguineo = Pessoa.GrupoSanguineo.O_Negativo
                },

                new Medico
                {
                    Especialidade = "Cardiologia",
                    Numero_de_ordem = "19204053",
                    Anos_de_experiencia = 10,


                    Nome = "Fernando Teixeira",
                    Morada = "Rua das Flores, 123, Lisboa",
                    Telemovel = "964851265",
                    TelemovelAlt = "917466125",
                    Email = "nurseteixeira@hospital.tomar.pt", // all medics have this email domain
                    NIF = "268183245",
                    Cod_Postal = "2300-420 SANTAREM",
                    Localidade = "Santarem",
                    genero = Pessoa.Género.Masculino,
                    DataDeNascimento = DateTime.Parse("1979-03-26"),
                    Grupo_Sanguineo = Pessoa.GrupoSanguineo.A_Positivo
                }
            );




            if (context.FuncionarioLimpeza.Any())
            {
                return;   // DB has been seeded
            }

            context.FuncionarioLimpeza.AddRange(
                new FuncionarioLimpeza
                {
                    Turno = FuncionarioLimpeza.Turnos.Manha,
                    Tamanho_Uniforme = FuncionarioLimpeza.Uniformes.M,
                    Data_de_contratacao = DateTime.Parse("2025-04-16"),
                    Certificacoes = "Certificado de Limpeza Hospitalar",

                    Nome = "Tim Bradford",
                    Morada = "Rua Ana Laura Travessa, 23, Leiria",
                    Telemovel = "211944942",
                    TelemovelAlt = "211944944",
                    Email = "therookie@gmail.com",
                    NIF = "190475689",
                    Cod_Postal = "2415-369 LEIRIA",
                    Localidade = "Leiria",
                    genero = Pessoa.Género.Masculino,
                    DataDeNascimento = DateTime.Parse("1998-04-01"),
                    Grupo_Sanguineo = Pessoa.GrupoSanguineo.AB_Positivo
                },

                new FuncionarioLimpeza
                {
                    Turno = FuncionarioLimpeza.Turnos.Noite,
                    Tamanho_Uniforme = FuncionarioLimpeza.Uniformes.L,
                    Data_de_contratacao = DateTime.Parse("2024-10-01"),
                    Certificacoes = "Curso Profissional de Limpeza Hospitalar",

                    Nome = "Carla Nogueira",
                    Morada = "Rua do Bem, 11, Braga",
                    Telemovel = "219334455",
                    TelemovelAlt = "910101010",
                    Email = "carlanogueira@gmail.com",
                    NIF = "204567891",
                    Cod_Postal = "4700-123 BRAGA",
                    Localidade = "Braga",
                    genero = Pessoa.Género.Feminino,
                    DataDeNascimento = DateTime.Parse("1991-01-20"),
                    Grupo_Sanguineo = Pessoa.GrupoSanguineo.O_Positivo
                },

                new FuncionarioLimpeza
                {
                    Turno = FuncionarioLimpeza.Turnos.Tarde,
                    Tamanho_Uniforme = FuncionarioLimpeza.Uniformes.S,
                    Data_de_contratacao = DateTime.Parse("2023-07-15"),
                    Certificacoes = "Treinamento Avançado em Higienização",
            
                    Nome = "Rui Almeida",
                    Morada = "Rua Central, 77, Faro",
                    Telemovel = "218776655",
                    TelemovelAlt = "917882211",
                    Email = "ruialmeida@gmail.com",
                    NIF = "198776543",
                    Cod_Postal = "8000-456 FARO",
                    Localidade = "Faro",
                    genero = Pessoa.Género.Masculino,
                    DataDeNascimento = DateTime.Parse("1996-09-14"),
                    Grupo_Sanguineo = Pessoa.GrupoSanguineo.A_Negativo
                }



            );







            if (context.Sala.Any())
            {
                return;   // DB has been seeded
            }

            context.Sala.AddRange(
                new Sala
                {
                    Bloco = "B",
                    Andar = 1,
                    Numero = 02
                },
                new Sala
                {

                    Bloco = "C",
                    Andar = 1,
                    Numero = 03
                },
                new Sala
                {
                    Bloco = "A",
                    Andar = 2,
                    Numero = 01
                }
            );


            if (context.Gabinete.Any())
            {
                return;   // DB has been seeded
            }
            else
            {
                context.Gabinete.AddRange(
                    new Gabinete {
                        Bloco = "B",
                        Andar = 1,
                        Numero = 03,
                        Descricao = "Gabinete B103 : utilizado maioritariamente por Ines Moura e Luis Martins",
                        Equipamento = "Monitor, Computador, Cadeira, Mesa",
                        Disponivel = true
                    });

            }




            if (context.QuartosInternagem.Any())
            {
                return;   // DB has been seeded
            }
            else
            {
                context.QuartosInternagem.AddRange(
                    new QuartosInternagem
                    {
                        Bloco = "A",
                        Andar = 2,
                        Numero = 01,
                        Descricao = "Quarto A201 : utilizado maioritariamente por utentes do sexo masculino",
                        Tipo = QuartosInternagem.TipoQuarto.Privado,
                        Capacidade = 8,
                    });
            }


            context.SaveChanges();






            if (context.LimpezaSala.Any())
            {
                return;   // DB has been seeded
            }
            else
            {

                var newestSala = context.Sala.OrderByDescending(s => s.ID).FirstOrDefault();
                var newestFuncionario = context.FuncionarioLimpeza.OrderByDescending(f => f.N_Processo).FirstOrDefault();

                if (newestSala != null && newestFuncionario != null)
                {
                    context.LimpezaSala.AddRange(
                        new LimpezaSala
                        {
                            Produto1 = "Detergente Desinfetante",
                            Produto2 = "Desinfetante Hospitalar",
                            Produto3 = "Desinfetante de Superfícies",
                            Data = DateTime.Parse("2025-04-16"),
                            Hora = TimeSpan.Parse("10:00"),
                            Sala_Id = newestSala.ID,
                            Sala = newestSala,
                            Funcionario_Id = newestFuncionario.N_Processo,
                            Funcionario = newestFuncionario
                        }
                      );
                }
            }





            if (context.Consulta.Any())
            {
                return;   // DB has been seeded
            }
            else
            {
                var newestUtente = context.Utente.OrderByDescending(u => u.N_Processo).FirstOrDefault();
                var newestMedico = context.Medico.OrderByDescending(m => m.N_Processo).FirstOrDefault();
                var newestGabinete = context.Gabinete.OrderByDescending(g => g.ID).FirstOrDefault();
                if (newestUtente != null && newestMedico != null && newestGabinete != null)
                {
                    context.Consulta.AddRange(
                        new Consulta
                        {
                            Data = DateTime.Parse("2025-04-16"),
                            Hora = TimeSpan.Parse("10:00"),
                            Diagnostico = "Hipertensão arterial controlada",
                            Tratamento = "Prescrição de medicação anti-hipertensiva e recomendações de dieta",
                            Observacoes = "Paciente aconselhado a medir a pressão arterial diariamente e retornar em 2 semanas",
                            Utente_Id = newestUtente.N_Processo,
                            Utente = newestUtente,
                            Medico_Id = newestMedico.N_Processo,
                            Medico = newestMedico,
                            Gabinete_Id = newestGabinete.ID,
                            Gabinete = newestGabinete

                        }
                      );
                }
            }


            context.SaveChanges();





            if (context.Internamento.Any())
            {
                return;   // DB has been seeded
            }
            else
            {
                var newestQuarto = context.QuartosInternagem.OrderByDescending(q => q.ID).FirstOrDefault();
                var newestConsulta = context.Consulta.OrderByDescending(c => c.Episodio).FirstOrDefault();
                var relatedUtente = (newestConsulta != null)? newestConsulta.Utente : context.Utente.OrderByDescending(c => c.N_Processo).FirstOrDefault();
                
                if(newestQuarto!=null && relatedUtente !=null)
                context.Internamento.AddRange(
                    new Internamento
                    {
                        DataHoraEntrada = DateTime.Parse("2025-04-16"),
                        DataHoraSaida = DateTime.Parse("2025-04-20"),
                        Utente_Id = relatedUtente.N_Processo,
                        Utente = relatedUtente,
                        Quarto_Id = newestQuarto.ID,
                        Quarto = newestQuarto,
                        Consulta = newestConsulta,
                        Consulta_Id = (newestConsulta != null) ? newestConsulta.Episodio : 0,
                    });
            }










            context.SaveChanges();
        }
    }
}