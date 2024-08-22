using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Queries.Responses;

namespace Questao5.Application.Handlers.Interfaces
{
    public interface ISaldoContaCorrenteService
    {
        Task<SaldoContaCorrenteResponse> ConsultarSaldoAsync(int idContaCorrente);
    }
}