using Questao5.Domain.Entities;

namespace Questao5.Domain.Repositories.Commands
{
    public interface IMovimentoCommandRepository
    {
        Task<Movimento> AddMovimentoAsync(Movimento movimento);
        Task UpdateMovimentoAsync(Movimento movimento);
    }
}
