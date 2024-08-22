using NSubstitute;
using Questao5.Application.Handlers;
using Questao5.Domain.Entities;
using Questao5.Domain.Repositories.Queries;

namespace Questao5.Tests
{
    public class SaldoContaCorrenteServiceTests
    {
        private readonly SaldoContaCorrenteService _saldoContaCorrenteService;
        private readonly IContaCorrenteQueryRepository _contaCorrenteQueryRepository;
        private readonly IMovimentoQueryRepository _movimentoQueryRepository;


        public SaldoContaCorrenteServiceTests()
        {
            _movimentoQueryRepository = Substitute.For<IMovimentoQueryRepository>();
            _contaCorrenteQueryRepository = Substitute.For<IContaCorrenteQueryRepository>();
            _saldoContaCorrenteService = new SaldoContaCorrenteService(_contaCorrenteQueryRepository, _movimentoQueryRepository);
        }


        [Fact]
        public async Task ConsultarSaldoAsync_DeveRetornarErro_QuandoContaNaoExistir()
        {
            // Arrange
            int numeroContaCorrente = 123;
            _contaCorrenteQueryRepository.GetByNumeroContaCorrenteIdAsync(numeroContaCorrente).Returns(Task.FromResult<ContaCorrente>(null));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _saldoContaCorrenteService.ConsultarSaldoAsync(numeroContaCorrente));
            Assert.Equal("Conta não encontrada.|INVALID_ACCOUNT", exception.Message);
        }

        [Fact]
        public async Task ConsultarSaldoAsync_DeveRetornarErro_QuandoContaEstiverInativa()
        {
            // Arrange
            int numeroContaCorrente = 123;
            var contaCorrente = new ContaCorrente
            {
                IdContaCorrente = Guid.NewGuid().ToString(),
                Numero = numeroContaCorrente,
                Nome = "João",
                Ativo = 0
            };
            _contaCorrenteQueryRepository.GetByNumeroContaCorrenteIdAsync(numeroContaCorrente).Returns(contaCorrente);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _saldoContaCorrenteService.ConsultarSaldoAsync(numeroContaCorrente));
            Assert.Equal("Conta inativa.|INACTIVE_ACCOUNT", exception.Message);
        }

        [Fact]
        public async Task ConsultarSaldoAsync_DeveRetornarSaldoCorreto()
        {
            // Arrange
            int numeroContaCorrente = 123;
            var contaCorrente = new ContaCorrente
            {
                IdContaCorrente = Guid.NewGuid().ToString(),
                Numero = numeroContaCorrente,
                Nome = "João",
                Ativo = 1
            };
            _contaCorrenteQueryRepository.GetByNumeroContaCorrenteIdAsync(numeroContaCorrente).Returns(contaCorrente);

            _movimentoQueryRepository.SomarCreditosAsync(contaCorrente.IdContaCorrente).Returns(300.00m);
            _movimentoQueryRepository.SomarDebitosAsync(contaCorrente.IdContaCorrente).Returns(100.00m);

            // Act
            var result = await _saldoContaCorrenteService.ConsultarSaldoAsync(numeroContaCorrente);

            // Assert
            Assert.Equal(numeroContaCorrente, result.Numero);
            Assert.Equal("João", result.Nome);
            Assert.Equal(200.00m, result.Saldo);
        }
    }
}
