using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Questao5.Domain.Entities
{
    [Table("contacorrente")]
    public class ContaCorrente
    {
        [Key]
        [Column("idcontacorrente")]
        public string IdContaCorrente { get; set; }

        [Column("numero")]
        [Required]
        public int Numero { get; set; }

        [Column("nome")]
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty;

        [Column("ativo")]
        [Required]
        [Range(0, 1)]
        public int Ativo { get; set; } = 0;
    }
}
