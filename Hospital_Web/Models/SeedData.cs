using Microsoft.EntityFrameworkCore;
using Hospital_Web.Data;


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
                    genero = Pessoa.Sexo.Masculino,
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
                    genero = Pessoa.Sexo.Feminino,
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
                    genero = Pessoa.Sexo.Masculino,
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
                    genero = Pessoa.Sexo.Feminino,
                },

                new Utente
                {
                    Estado_clinico = "Paciente diabético tipo 2, com controlo glicémico irregular.",
                    Grupo_Sanguineo = Utente.GrupoSanguineo.A_Negativo,
                    Alergias = "Penicilina, Marisco",
                    Seguro_de_Saude = "Fidelidade",
                    Nome = "Pedro Santos",
                    Data_de_Registo = DateTime.Parse("2025-04-19"),
                    DataDeNascimento = DateTime.Parse("1975-12-10"),
                    Morada = "Rua Nova, 67, Aveiro",
                    Telemovel = "918765432",
                    TelemovelAlt = "913456789",
                    Email = "pedro.santos@email.com",
                    NIF = "245678901",
                    Cod_Postal = "3800-123 AVEIRO",
                    Localidade = "Aveiro",
                    genero = Pessoa.Sexo.Masculino,
                },
                new Utente
                {
                    Estado_clinico = "Fraturas múltiplas no braço direito após acidente de viação.",
                    Grupo_Sanguineo = Utente.GrupoSanguineo.O_Negativo,
                    Alergias = "Látex",
                    Seguro_de_Saude = "Multicare",
                    Nome = "Sofia Rodrigues",
                    Data_de_Registo = DateTime.Parse("2025-04-20"),
                    DataDeNascimento = DateTime.Parse("1992-08-23"),
                    Morada = "Avenida Central, 200, Viseu",
                    Telemovel = "916543210",
                    TelemovelAlt = "912987654",
                    Email = "sofia.rodrigues@email.com",
                    NIF = "289012345",
                    Cod_Postal = "3500-789 VISEU",
                    Localidade = "Viseu",
                    genero = Pessoa.Sexo.Feminino,
                },
                new Utente
                {
                    Estado_clinico = "Hipertensão arterial e colesterol elevado, em acompanhamento regular.",
                    Grupo_Sanguineo = Utente.GrupoSanguineo.B_Negativo,
                    Alergias = "Aspirina, Ibuprofeno",
                    Seguro_de_Saude = "Médis",
                    Nome = "António Oliveira",
                    Data_de_Registo = DateTime.Parse("2025-04-21"),
                    DataDeNascimento = DateTime.Parse("1965-05-15"),
                    Morada = "Rua do Campo, 89, Setúbal",
                    Telemovel = "914321098",
                    TelemovelAlt = "917890123",
                    Email = "antonio.oliveira@email.com",
                    NIF = "234890123",
                    Cod_Postal = "2900-456 SETÚBAL",
                    Localidade = "Setúbal",
                    genero = Pessoa.Sexo.Masculino,
                },
                new Utente
                {
                    Estado_clinico = "Gravidez de 32 semanas, sem complicações.",
                    Grupo_Sanguineo = Utente.GrupoSanguineo.AB_Negativo,
                    Alergias = "Nenhuma conhecida",
                    Seguro_de_Saude = "AdvanceCare",
                    Nome = "Catarina Pereira",
                    Data_de_Registo = DateTime.Parse("2025-04-22"),
                    DataDeNascimento = DateTime.Parse("1995-02-28"),
                    Morada = "Rua da Paz, 156, Funchal",
                    Telemovel = "915678901",
                    TelemovelAlt = "918234567",
                    Email = "catarina.pereira@email.com",
                    NIF = "278345612",
                    Cod_Postal = "9000-123 FUNCHAL",
                    Localidade = "Funchal",
                    genero = Pessoa.Sexo.Feminino,
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
                    genero = Pessoa.Sexo.Feminino,
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
                    genero = Pessoa.Sexo.Masculino,
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
                    genero = Pessoa.Sexo.Masculino,
                    DataDeNascimento = DateTime.Parse("1979-03-26"),
                    Grupo_Sanguineo = Pessoa.GrupoSanguineo.A_Positivo
                },
                new Medico
                {
                    Especialidade = "Endocrinologia",
                    Numero_de_ordem = "21095674",
                    Anos_de_experiencia = 12,
                    Nome = "Dr. Ricardo Alves",
                    Morada = "Rua dos Médicos, 78, Porto",
                    Telemovel = "963741852",
                    TelemovelAlt = "918520741",
                    Email = "ricardoalves@hospital.tomar.pt",
                    NIF = "295123456",
                    Cod_Postal = "4200-567 PORTO",
                    Localidade = "Porto",
                    genero = Pessoa.Sexo.Masculino,
                    DataDeNascimento = DateTime.Parse("1980-11-03"),
                    Grupo_Sanguineo = Pessoa.GrupoSanguineo.A_Negativo
                },
                new Medico
                {
                    Especialidade = "Ginecologia",
                    Numero_de_ordem = "22103458",
                    Anos_de_experiencia = 18,
                    Nome = "Dra. Helena Cardoso",
                    Morada = "Avenida da Saúde, 134, Lisboa",
                    Telemovel = "967412580",
                    TelemovelAlt = "914785296",
                    Email = "helenacardoso@hospital.tomar.pt",
                    NIF = "285741963",
                    Cod_Postal = "1700-890 LISBOA",
                    Localidade = "Lisboa",
                    genero = Pessoa.Sexo.Feminino,
                    DataDeNascimento = DateTime.Parse("1975-07-19"),
                    Grupo_Sanguineo = Pessoa.GrupoSanguineo.O_Positivo
                },
                new Medico
                {
                    Especialidade = "Medicina Geral",
                    Numero_de_ordem = "19876543",
                    Anos_de_experiencia = 6,
                    Nome = "Dr. Miguel Torres",
                    Morada = "Rua da Clínica, 45, Coimbra",
                    Telemovel = "961234567",
                    TelemovelAlt = "916789012",
                    Email = "migueltorres@hospital.tomar.pt",
                    NIF = "276543210",
                    Cod_Postal = "3030-234 COIMBRA",
                    Localidade = "Coimbra",
                    genero = Pessoa.Sexo.Masculino,
                    DataDeNascimento = DateTime.Parse("1988-04-12"),
                    Grupo_Sanguineo = Pessoa.GrupoSanguineo.AB_Positivo
                },
                new Medico
                {
                    Especialidade = "Pediatria",
                    Numero_de_ordem = "23456789",
                    Anos_de_experiencia = 9,
                    Nome = "Dra. Joana Ribeiro",
                    Morada = "Travessa Infantil, 23, Braga",
                    Telemovel = "968901234",
                    TelemovelAlt = "913456789",
                    Email = "joanaribeiro@hospital.tomar.pt",
                    NIF = "289456123",
                    Cod_Postal = "4710-345 BRAGA",
                    Localidade = "Braga",
                    genero = Pessoa.Sexo.Feminino,
                    DataDeNascimento = DateTime.Parse("1984-09-28"),
                    Grupo_Sanguineo = Pessoa.GrupoSanguineo.B_Negativo
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
                    genero = Pessoa.Sexo.Masculino,
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
                    genero = Pessoa.Sexo.Feminino,
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
                    genero = Pessoa.Sexo.Masculino,
                    DataDeNascimento = DateTime.Parse("1996-09-14"),
                    Grupo_Sanguineo = Pessoa.GrupoSanguineo.A_Negativo
                },
                new FuncionarioLimpeza
                {
                    Turno = FuncionarioLimpeza.Turnos.Manha,
                    Tamanho_Uniforme = FuncionarioLimpeza.Uniformes.L,
                    Data_de_contratacao = DateTime.Parse("2024-06-10"),
                    Certificacoes = "Certificado de Desinfecção Hospitalar, Curso de Manuseamento de Resíduos",
                    Nome = "Sandra Mendes",
                    Morada = "Rua da Limpeza, 44, Évora",
                    Telemovel = "214567890",
                    TelemovelAlt = "915432109",
                    Email = "sandramendes@gmail.com",
                    NIF = "201234567",
                    Cod_Postal = "7000-234 ÉVORA",
                    Localidade = "Évora",
                    genero = Pessoa.Sexo.Feminino,
                    DataDeNascimento = DateTime.Parse("1989-06-15"),
                    Grupo_Sanguineo = Pessoa.GrupoSanguineo.B_Positivo
                },
                new FuncionarioLimpeza
                {
                    Turno = FuncionarioLimpeza.Turnos.Tarde,
                    Tamanho_Uniforme = FuncionarioLimpeza.Uniformes.XL,
                    Data_de_contratacao = DateTime.Parse("2023-02-20"),
                    Certificacoes = "Certificado Internacional de Higiene Hospitalar",
                    Nome = "José Fernandes",
                    Morada = "Avenida da Higiene, 88, Viana do Castelo",
                    Telemovel = "217890123",
                    TelemovelAlt = "912345678",
                    Email = "josefernandes@gmail.com",
                    NIF = "212345678",
                    Cod_Postal = "4900-567 VIANA DO CASTELO",
                    Localidade = "Viana do Castelo",
                    genero = Pessoa.Sexo.Masculino,
                    DataDeNascimento = DateTime.Parse("1985-12-03"),
                    Grupo_Sanguineo = Pessoa.GrupoSanguineo.A_Positivo
                },
                new FuncionarioLimpeza
                {
                    Turno = FuncionarioLimpeza.Turnos.Noite,
                    Tamanho_Uniforme = FuncionarioLimpeza.Uniformes.M,
                    Data_de_contratacao = DateTime.Parse("2024-11-15"),
                    Certificacoes = "Formação em Limpeza de Blocos Operatórios",
                    Nome = "Marta Silva",
                    Morada = "Rua Noturna, 33, Guarda",
                    Telemovel = "213456789",
                    TelemovelAlt = "918765432",
                    Email = "martasilva@gmail.com",
                    NIF = "207890123",
                    Cod_Postal = "6300-123 GUARDA",
                    Localidade = "Guarda",
                    genero = Pessoa.Sexo.Feminino,
                    DataDeNascimento = DateTime.Parse("1993-03-22"),
                    Grupo_Sanguineo = Pessoa.GrupoSanguineo.O_Negativo
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
                    new Gabinete
                    {
                        Bloco = "B",
                        Andar = 1,
                        Numero = 03,
                        Descricao = "Gabinete B103 : utilizado maioritariamente por Ines Moura e Luis Martins",
                        Equipamento = "Monitor, Computador, Cadeira, Mesa",
                        Disponivel = true
                    },
                    new Gabinete
                    {
                        Bloco = "A",
                        Andar = 1,
                        Numero = 05,
                        Descricao = "Gabinete A105 : gabinete de endocrinologia, utilizado pelo Dr. Ricardo Alves",
                        Equipamento = "Monitor, Computador, Cadeira, Mesa, Balança digital, Medidor de glicose",
                        Disponivel = true
                    },
                    new Gabinete
                    {
                        Bloco = "C",
                        Andar = 2,
                        Numero = 07,
                        Descricao = "Gabinete C207 : gabinete de ginecologia, utilizado pela Dra. Helena Cardoso",
                        Equipamento = "Monitor, Computador, Cadeira, Mesa, Marquesa ginecológica, Ecógrafo",
                        Disponivel = true
                    },
                    new Gabinete
                    {
                        Bloco = "A",
                        Andar = 2,
                        Numero = 08,
                        Descricao = "Gabinete A208 : gabinete de medicina geral, utilizado pelo Dr. Miguel Torres",
                        Equipamento = "Monitor, Computador, Cadeira, Mesa, Tensiómetro, Estetoscópio",
                        Disponivel = false
                    },
                    new Gabinete
                    {
                        Bloco = "D",
                        Andar = 1,
                        Numero = 09,
                        Descricao = "Gabinete D109 : gabinete de pediatria, utilizado pela Dra. Joana Ribeiro",
                        Equipamento = "Monitor, Computador, Cadeiras coloridas, Mesa baixa, Brinquedos, Balança pediátrica",
                        Disponivel = true
                    }
                 );

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
                    },
                    new QuartosInternagem
                    {
                        Bloco = "B",
                        Andar = 2,
                        Numero = 02,
                        Descricao = "Quarto B202 : quarto duplo para internamento geral",
                        Tipo = QuartosInternagem.TipoQuarto.Normal,
                        Capacidade = 2,
                    },
                    new QuartosInternagem
                    {
                        Bloco = "C",
                        Andar = 3,
                        Numero = 03,
                        Descricao = "Quarto C303 : enfermaria feminina com 6 camas",
                        Tipo = QuartosInternagem.TipoQuarto.SemiPrivado,
                        Capacidade = 6,
                    },
                    new QuartosInternagem
                    {
                        Bloco = "A",
                        Andar = 3,
                        Numero = 04,
                        Descricao = "Quarto A304 : quarto VIP com todas as comodidades",
                        Tipo = QuartosInternagem.TipoQuarto.Privado,
                        Capacidade = 1,
                    },
                    new QuartosInternagem
                    {
                        Bloco = "B",
                        Andar = 3,
                        Numero = 05,
                        Descricao = "Quarto B305 : quarto de isolamento para casos especiais",
                        Tipo = QuartosInternagem.TipoQuarto.Privado,
                        Capacidade = 1,
                    },
                    new QuartosInternagem
                    {
                        Bloco = "D",
                        Andar = 2,
                        Numero = 06,
                        Descricao = "Quarto D206 : enfermaria pediátrica com decoração colorida",
                        Tipo = QuartosInternagem.TipoQuarto.SemiPrivado,
                        Capacidade = 4,
                    }
                );
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





            // Additional Consulta (Consultations) entries
            if (context.Consulta.Any())
            {
                return;   // DB has been seeded
            }
            else
            {
                // Get specific patients and doctors for realistic consultations
                var joaoSilva = context.Utente.FirstOrDefault(u => u.Nome == "João Silva");
                var mariaFerreira = context.Utente.FirstOrDefault(u => u.Nome == "Maria Ferreira");
                var carlosGomes = context.Utente.FirstOrDefault(u => u.Nome == "Carlos Gomes");
                var anaCosta = context.Utente.FirstOrDefault(u => u.Nome == "Ana Costa");
                var pedroSantos = context.Utente.FirstOrDefault(u => u.Nome == "Pedro Santos");
                var sofiaRodrigues = context.Utente.FirstOrDefault(u => u.Nome == "Sofia Rodrigues");
                var antonioOliveira = context.Utente.FirstOrDefault(u => u.Nome == "António Oliveira");
                var catarinaPereira = context.Utente.FirstOrDefault(u => u.Nome == "Catarina Pereira");

                var drInesNeuro = context.Medico.FirstOrDefault(m => m.Nome == "Inês Moura");
                var drLuisOrtopedia = context.Medico.FirstOrDefault(m => m.Nome == "Luís Martins");
                var drFernandoCardio = context.Medico.FirstOrDefault(m => m.Nome == "Fernando Teixeira");
                var drRicardoEndo = context.Medico.FirstOrDefault(m => m.Nome == "Dr. Ricardo Alves");
                var draHelenaGine = context.Medico.FirstOrDefault(m => m.Nome == "Dra. Helena Cardoso");
                var drMiguelGeral = context.Medico.FirstOrDefault(m => m.Nome == "Dr. Miguel Torres");

                var gabineteB103 = context.Gabinete.FirstOrDefault(g => g.Bloco == "B" && g.Numero == 03);
                var gabineteA105 = context.Gabinete.FirstOrDefault(g => g.Bloco == "A" && g.Numero == 05);
                var gabineteC207 = context.Gabinete.FirstOrDefault(g => g.Bloco == "C" && g.Numero == 07);
                var gabineteA208 = context.Gabinete.FirstOrDefault(g => g.Bloco == "A" && g.Numero == 08);

                var consultas = new List<Consulta>();

                // João Silva - Ortopedia consultation for toe injury
                if (joaoSilva != null && drLuisOrtopedia != null && gabineteB103 != null)
                {
                    consultas.Add(new Consulta
                    {
                        Data = DateTime.Parse("2025-04-16"),
                        Hora = TimeSpan.Parse("09:00"),
                        Diagnostico = "Contusão no 5º dedo do pé direito com hematoma",
                        Tratamento = "Aplicação de gelo, anti-inflamatório tópico e repouso",
                        Observacoes = "Paciente orientado para elevação do membro e retorno se não melhorar em 5 dias",
                        Utente_Id = joaoSilva.N_Processo,
                        Utente = joaoSilva,
                        Medico_Id = drLuisOrtopedia.N_Processo,
                        Medico = drLuisOrtopedia,
                        Gabinete_Id = gabineteB103.ID,
                        Gabinete = gabineteB103
                    });
                }

                // Maria Ferreira - Neurologia consultation for headaches
                if (mariaFerreira != null && drInesNeuro != null && gabineteB103 != null)
                {
                    consultas.Add(new Consulta
                    {
                        Data = DateTime.Parse("2025-04-18"),
                        Hora = TimeSpan.Parse("14:30"),
                        Diagnostico = "Cefaleia tensional crónica com componente de insónia",
                        Tratamento = "Prescrição de analgésico e orientações de higiene do sono",
                        Observacoes = "Paciente aconselhada a manter diário de dores de cabeça e retornar em 1 mês",
                        Utente_Id = mariaFerreira.N_Processo,
                        Utente = mariaFerreira,
                        Medico_Id = drInesNeuro.N_Processo,
                        Medico = drInesNeuro,
                        Gabinete_Id = gabineteB103.ID,
                        Gabinete = gabineteB103
                    });
                }

                // Carlos Gomes - Medicina Geral for abdominal pain
                if (carlosGomes != null && drMiguelGeral != null && gabineteA208 != null)
                {
                    consultas.Add(new Consulta
                    {
                        Data = DateTime.Parse("2025-04-17"),
                        Hora = TimeSpan.Parse("11:15"),
                        Diagnostico = "Gastrite aguda, possivelmente relacionada com stress",
                        Tratamento = "Prescrição de protetor gástrico e orientações dietéticas",
                        Observacoes = "Paciente orientado para dieta branda e evitar alimentos irritantes. Reavaliação em 2 semanas",
                        Utente_Id = carlosGomes.N_Processo,
                        Utente = carlosGomes,
                        Medico_Id = drMiguelGeral.N_Processo,
                        Medico = drMiguelGeral,
                        Gabinete_Id = gabineteA208.ID,
                        Gabinete = gabineteA208
                    });
                }

                // Pedro Santos - Endocrinologia for diabetes
                if (pedroSantos != null && drRicardoEndo != null && gabineteA105 != null)
                {
                    consultas.Add(new Consulta
                    {
                        Data = DateTime.Parse("2025-04-19"),
                        Hora = TimeSpan.Parse("10:45"),
                        Diagnostico = "Diabetes mellitus tipo 2 descompensada",
                        Tratamento = "Ajuste de medicação antidiabética e orientação nutricional",
                        Observacoes = "HbA1c elevada (9.2%). Paciente encaminhado para nutricionista e retorno em 1 mês",
                        Utente_Id = pedroSantos.N_Processo,
                        Utente = pedroSantos,
                        Medico_Id = drRicardoEndo.N_Processo,
                        Medico = drRicardoEndo,
                        Gabinete_Id = gabineteA105.ID,
                        Gabinete = gabineteA105
                    });
                }

                // Sofia Rodrigues - Ortopedia for fractures
                if (sofiaRodrigues != null && drLuisOrtopedia != null && gabineteB103 != null)
                {
                    consultas.Add(new Consulta
                    {
                        Data = DateTime.Parse("2025-04-20"),
                        Hora = TimeSpan.Parse("08:30"),
                        Diagnostico = "Fraturas múltiplas de rádio e ulna direitos",
                        Tratamento = "Imobilização com tala gessada e prescrição de analgésicos",
                        Observacoes = "Paciente necessita de cirurgia ortopédica. Internamento programado para redução e fixação",
                        Utente_Id = sofiaRodrigues.N_Processo,
                        Utente = sofiaRodrigues,
                        Medico_Id = drLuisOrtopedia.N_Processo,
                        Medico = drLuisOrtopedia,
                        Gabinete_Id = gabineteB103.ID,
                        Gabinete = gabineteB103
                    });
                }

                // António Oliveira - Cardiologia for hypertension
                if (antonioOliveira != null && drFernandoCardio != null && gabineteB103 != null)
                {
                    consultas.Add(new Consulta
                    {
                        Data = DateTime.Parse("2025-04-21"),
                        Hora = TimeSpan.Parse("15:00"),
                        Diagnostico = "Hipertensão arterial não controlada com dislipidemia",
                        Tratamento = "Ajuste de medicação anti-hipertensiva e estatina",
                        Observacoes = "PA: 160/95 mmHg. Paciente orientado para dieta hipossódica e exercícios leves. Retorno em 15 dias",
                        Utente_Id = antonioOliveira.N_Processo,
                        Utente = antonioOliveira,
                        Medico_Id = drFernandoCardio.N_Processo,
                        Medico = drFernandoCardio,
                        Gabinete_Id = gabineteB103.ID,
                        Gabinete = gabineteB103
                    });
                }

                // Catarina Pereira - Ginecologia for pregnancy
                if (catarinaPereira != null && draHelenaGine != null && gabineteC207 != null)
                {
                    consultas.Add(new Consulta
                    {
                        Data = DateTime.Parse("2025-04-22"),
                        Hora = TimeSpan.Parse("16:30"),
                        Diagnostico = "Gravidez de 32 semanas sem complicações",
                        Tratamento = "Suplementação vitamínica e orientações pré-parto",
                        Observacoes = "Ecografia normal. Feto com crescimento adequado. Próxima consulta em 2 semanas",
                        Utente_Id = catarinaPereira.N_Processo,
                        Utente = catarinaPereira,
                        Medico_Id = draHelenaGine.N_Processo,
                        Medico = draHelenaGine,
                        Gabinete_Id = gabineteC207.ID,
                        Gabinete = gabineteC207
                    });
                }

                // Ana Costa - Follow-up for asthma
                if (anaCosta != null && drMiguelGeral != null && gabineteA208 != null)
                {
                    consultas.Add(new Consulta
                    {
                        Data = DateTime.Parse("2025-04-23"),
                        Hora = TimeSpan.Parse("09:30"),
                        Diagnostico = "Asma brônquica moderada em controlo",
                        Tratamento = "Manutenção da medicação inalatória atual",
                        Observacoes = "Paciente sem crises recentes. Técnica inalatória correta. Retorno em 3 meses",
                        Utente_Id = anaCosta.N_Processo,
                        Utente = anaCosta,
                        Medico_Id = drMiguelGeral.N_Processo,
                        Medico = drMiguelGeral,
                        Gabinete_Id = gabineteA208.ID,
                        Gabinete = gabineteA208
                    });
                }

                context.Consulta.AddRange(consultas);
                context.SaveChanges();
            }

            // Additional Internamento (Inpatient stays) entries
            if (context.Internamento.Any())
            {
                return;   // DB has been seeded
            }
            else
            {
                var internamentos = new List<Internamento>();

                // Get specific consultations that would lead to hospitalization
                var sofiaConsulta = context.Consulta.FirstOrDefault(c => c.Utente != null && c.Utente.Nome == "Sofia Rodrigues");
                var pedroConsulta = context.Consulta.FirstOrDefault(c => c.Utente != null && c.Utente.Nome == "Pedro Santos");
                var antonioConsulta = context.Consulta.FirstOrDefault(c => c.Utente != null && c.Utente.Nome == "António Oliveira");
                var catarinaConsulta = context.Consulta.FirstOrDefault(c => c.Utente != null && c.Utente.Nome == "Catarina Pereira");

                // Get available rooms
                var quartoPrivadoA = context.QuartosInternagem.FirstOrDefault(q => q.Bloco == "A" && q.Numero == 04);
                var quartoDuplo = context.QuartosInternagem.FirstOrDefault(q => q.Bloco == "B" && q.Numero == 02);
                var quartoVIP = context.QuartosInternagem.FirstOrDefault(q => q.Bloco == "A" && q.Numero == 04);
                var enfermaria = context.QuartosInternagem.FirstOrDefault(q => q.Bloco == "C" && q.Numero == 03);

                // Sofia Rodrigues - Surgery for fractures
                if (sofiaConsulta != null && quartoPrivadoA != null)
                {
                    internamentos.Add(new Internamento
                    {
                        DataHoraEntrada = DateTime.Parse("2025-04-20 18:00"),
                        DataHoraSaida = DateTime.Parse("2025-04-25 12:00"),
                        Utente_Id = sofiaConsulta.Utente?.N_Processo ?? 0,
                        Utente = sofiaConsulta.Utente,
                        Quarto_Id = quartoPrivadoA.ID,
                        Quarto = quartoPrivadoA,
                        Consulta = sofiaConsulta,
                        Consulta_Id = sofiaConsulta.Episodio
                    });
                }

                // Pedro Santos - Diabetes complications
                if (pedroConsulta != null && quartoDuplo != null)
                {
                    internamentos.Add(new Internamento
                    {
                        DataHoraEntrada = DateTime.Parse("2025-04-19 20:30"),
                        DataHoraSaida = DateTime.Parse("2025-04-22 10:00"),
                        Utente_Id = pedroConsulta.Utente?.N_Processo ?? 0,
                        Utente = pedroConsulta.Utente,
                        Quarto_Id = quartoDuplo.ID,
                        Quarto = quartoDuplo,
                        Consulta = pedroConsulta,
                        Consulta_Id = pedroConsulta.Episodio
                    });
                }

                // António Oliveira - Hypertensive crisis
                if (antonioConsulta != null && enfermaria != null)
                {
                    internamentos.Add(new Internamento
                    {
                        DataHoraEntrada = DateTime.Parse("2025-04-21 22:15"),
                        DataHoraSaida = DateTime.Parse("2025-04-24 14:30"),
                        Utente_Id = antonioConsulta.Utente?.N_Processo ?? 0,
                        Utente = antonioConsulta.Utente,
                        Quarto_Id = enfermaria.ID,
                        Quarto = enfermaria,
                        Consulta = antonioConsulta,
                        Consulta_Id = antonioConsulta.Episodio
                    });
                }

                // Catarina Pereira - Pregnancy monitoring
                if (catarinaConsulta != null && quartoVIP != null)
                {
                    internamentos.Add(new Internamento
                    {
                        DataHoraEntrada = DateTime.Parse("2025-04-26 08:00"),
                        DataHoraSaida = null, // Still hospitalized
                        Utente_Id = catarinaConsulta.Utente?.N_Processo ?? 0,
                        Utente = catarinaConsulta.Utente,
                        Quarto_Id = quartoVIP.ID,
                        Quarto = quartoVIP,
                        Consulta = catarinaConsulta,
                        Consulta_Id = catarinaConsulta.Episodio
                    });
                }

                // Additional internamento without prior consultation (emergency admission)
                var carlosGomes = context.Utente.FirstOrDefault(u => u.Nome == "Carlos Gomes");
                var quartoIsolamento = context.QuartosInternagem.FirstOrDefault(q => q.Bloco == "B" && q.Numero == 05);

                if (carlosGomes != null && quartoIsolamento != null)
                {
                    internamentos.Add(new Internamento
                    {
                        DataHoraEntrada = DateTime.Parse("2025-04-18 03:30"),
                        DataHoraSaida = DateTime.Parse("2025-04-19 16:00"),
                        Utente_Id = carlosGomes.N_Processo,
                        Utente = carlosGomes,
                        Quarto_Id = quartoIsolamento.ID,
                        Quarto = quartoIsolamento,
                        Consulta = null,
                        Consulta_Id = null
                    });
                }

                context.Internamento.AddRange(internamentos);
                context.SaveChanges();
            }
        }
    }
}