using Questao5.Application.Handlers.Interfaces;
using Questao5.Application.Queries.Responses;
using Questao5.Domain.Repositories.Queries;

namespace Questao5.Application.Handlers
{
    public class SaldoContaCorrenteService : ISaldoContaCorrenteService
    {
        private readonly IContaCorrenteQueryRepository _contaCorrenteQueryRepository;
        private readonly IMovimentoQueryRepository _movimentoQueryRepository;

        public SaldoContaCorrenteService(IContaCorrenteQueryRepository contaCorrenteQueryRepository, IMovimentoQueryRepository movimentoQueryRepository)
        {
            _contaCorrenteQueryRepository = contaCorrenteQueryRepository;
            _movimentoQueryRepository = movimentoQueryRepository;
        }

        public async Task<SaldoContaCorrenteResponse> ConsultarSaldoAsync(int numeroContaCorrente)
        {
            var conta = await _contaCorrenteQueryRepository.GetByNumeroContaCorrenteIdAsync(numeroContaCorrente);
            if (conta == null)
                throw new Exception("Conta não encontrada.|INVALID_ACCOUNT");

            if (conta.Ativo == 0)
                throw new Exception("Conta inativa.|INACTIVE_ACCOUNT");

            var creditos = await _movimentoQueryRepository.SomarCreditosAsync(conta.IdContaCorrente);
            var debitos = await _movimentoQueryRepository.SomarDebitosAsync(conta.IdContaCorrente);

            var saldo = creditos - debitos;

            return new SaldoContaCorrenteResponse
            {
                Numero = conta.Numero,
                Nome = conta.Nome,
                Saldo = saldo
            };
        }
    }
}
