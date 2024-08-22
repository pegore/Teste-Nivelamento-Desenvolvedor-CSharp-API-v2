using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Handlers.Interfaces
{
    public interface IMovimentoService
    {
        Task<bool> VerificarContaCorrenteAsync(int contaCorrenteId);
        Task<CreateTransacaoResponse> CriarMovimentoAsync(CriarMovimentoRequest request);
    }
}