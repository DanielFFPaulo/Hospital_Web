using Hospital_Web.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital_Web.Models
{
    [PrimaryKey(nameof(Funcionario), nameof(Sala))]
    public class LimpezaSala
    {
        [Column(Order = 0)]
        public int Funcionario { get; set; }

        [Column(Order = 1)]
        public int Sala { get; set; }

        [StringLength(100)]
        public string Produto1 { get; set; }

        [StringLength(100)]
        public string Produto2 { get; set; }

        [StringLength(100)]
        public string Produto3 { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Data { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan? Hora { get; set; }

        // Navigation properties
        [ForeignKey("Funcionario")]
        public virtual Funcionario FuncionarioNavigation { get; set; }

        [ForeignKey("Sala")]
        public virtual Sala SalaNavigation { get; set; }
    }
}