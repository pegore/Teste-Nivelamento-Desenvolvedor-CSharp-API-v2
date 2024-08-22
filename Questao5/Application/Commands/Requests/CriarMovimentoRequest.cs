using FluentValidation;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests
{
    public class CriarMovimentoRequest
    {
        public int Numero { get; set; }

        public DateTime DataMovimento { get; set; } = DateTime.Now;

        public ETipoMovimento TipoMovimento { get; set; }

        public decimal Valor { get; set; }
    }
    public class CriarMovimentoRequestValidator : AbstractValidator<CriarMovimentoRequest>
    {
        public CriarMovimentoRequestValidator()
        {
            RuleFor(x => x)
                .NotNull().WithMessage("Requisição inválida. | INVALID_REQUEST");

            RuleFor(x => x.Numero)
                .NotEmpty().GreaterThan(0).WithMessage("Conta corrente inválida. | INVALID_ACCOUNT");

            RuleFor(x => x.Valor)
                .GreaterThan(0).WithMessage("Valor deve ser positivo. | INVALID_VALUE");

            RuleFor(x => x.TipoMovimento)
                .Must(tipo => Enum.IsDefined(typeof(ETipoMovimento), tipo))
                .WithMessage("Tipo de movimento inválido. Use 'C' para Crédito ou 'D' para Débito. | INVALID_TYPE");
        }
    }
}