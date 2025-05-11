using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class LimpezaSala
    {
        [Key]
        public int ID { get; set; }
        [StringLength(100)]
        public string Produto1 { get; set; }

        [StringLength(100)]
        public string Produto2 { get; set; }

        [StringLength(100)]
        public string Produto3 { get; set; }

        [DataType(DataType.Date)]
        public DateTime Data { get; set; }

        [DataType(DataType.Time)]
        public TimeSpan Hora { get; set; }

        // Foreign keys
        public int Funcionario_Id { get; set; }

        [ForeignKey("Funcionario_Id")]
        public virtual FuncionarioLimpeza? Funcionario { get; set; }

        public int Sala_Id { get; set; }

        [ForeignKey("Sala_Id")]
        public virtual Sala? Sala { get; set; }
    }

}
