namespace Questao5.Domain.Repositories.Queries
{
    public interface IMovimentoQueryRepository
    {
        Task<decimal> SomarCreditosAsync(string numeroContaCorrente);
        Task<decimal> SomarDebitosAsync(string numeroContaCorrente);
    }
}
