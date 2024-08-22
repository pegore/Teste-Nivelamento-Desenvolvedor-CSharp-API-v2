using Questao5.Domain.Entities;

namespace Questao5.Domain.Repositories.Queries
{
    public interface IContaCorrenteQueryRepository
    {
        Task<ContaCorrente> GetByNumeroContaCorrenteIdAsync(int contaCorrenteId);
    }
}
