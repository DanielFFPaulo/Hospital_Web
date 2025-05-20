using Hospital_Web.Models;
using System.ComponentModel.DataAnnotations;

public class Administrador : Pessoa
{
    [Required]
    public string Departamento { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Função Principal")]
    public string Funcao { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    [Display(Name = "Data de Início de Funções")]
    public DateTime DataInicio { get; set; } = DateTime.Now;
}
