using Questao5.Domain.Enumerators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questao5.Domain.Entities
{
    [Table("movimento")]
    public class Movimento
    {
        private decimal _valor;

        [Key]
        [Column("idmovimento")]
        public string IdMovimento { get; set; } = Guid.NewGuid().ToString();

        [Column("idcontacorrente")]
        [Required]
        public string IdContaCorrente { get; set; }

        [ForeignKey("IdContaCorrente")]
        public ContaCorrente ContaCorrente { get; set; }

        [Column("datamovimento")]
        [Required]
        [MaxLength(25)]
        public DateTime DataMovimento { get; set; } = DateTime.Now;

        [Column("tipomovimento")]
        [Required]
        [MaxLength(1)]
        public string TipoMovimento { get; set; }

        [Column("valor")]
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "O valor deve ser positivo.")]
        public decimal Valor
        {
            get => _valor;
            set
            {
                _valor = Math.Round(value, 2);
            }
        }
    }
}
