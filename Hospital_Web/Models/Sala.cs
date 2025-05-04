using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hospital_Web.Models
{
    public class Sala
    {
        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string Bloco { get; set; }

        [StringLength(10)]
        public string Andar { get; set; }

        [StringLength(10)]
        public string Numero { get; set; }

        // Navigation properties
        public virtual ICollection<LimpezaSala>? LimpezasSalas { get; set; }
    }
}
