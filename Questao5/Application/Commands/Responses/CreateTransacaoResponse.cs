using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Responses
{
    public class CreateTransacaoResponse
    {
        public Guid Id { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public ETipoErro TipoErro { get; internal set; }
    }
}