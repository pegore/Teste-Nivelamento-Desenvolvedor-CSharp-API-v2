using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questao5.Domain.Entities
{
    [Table("idempotencia")]
    public class Idempotencia
    {
        [Key]
        [Column("chave_idempotencia")]
        public Guid ChaveIdempotencia { get; set; }

        [Column("requisicao")]
        [MaxLength(1000)]
        public string Requisicao { get; set; } = string.Empty;

        [Column("resultado")]
        [MaxLength(1000)]
        public string Resultado { get; set; } = string.Empty;
    }
}
