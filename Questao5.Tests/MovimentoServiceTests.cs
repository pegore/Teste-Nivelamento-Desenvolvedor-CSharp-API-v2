using NSubstitute;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Handlers;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Repositories.Commands;
using Questao5.Domain.Repositories.Queries;

namespace Questao5.Tests
{
    public class MovimentoServiceTests
    {
        private readonly MovimentoService _movimentoService;
        private readonly IMovimentoCommandRepository _movimentoCommandRepository;
        private readonly IContaCorrenteQueryRepository _contaCorrenteQueryRepository;

        public MovimentoServiceTests()
        {
            _movimentoCommandRepository = Substitute.For<IMovimentoCommandRepository>();
            _contaCorrenteQueryRepository = Substitute.For<IContaCorrenteQueryRepository>();
            _movimentoService = new MovimentoService(_movimentoCommandRepository, _contaCorrenteQueryRepository);
        }

        [Fact]
        public async Task CriarMovimentoAsync_DeveRetornarErroParaContaInvalida()
        {
            // Arrange
            var request = new CriarMovimentoRequest
            {
                Numero = 0,
                Valor = 100,
                TipoMovimento = (ETipoMovimento)'C'
            };

            // Act
            var result = await _movimentoService.CriarMovimentoAsync(request);

            // Assert
            Assert.False(result.Success);
            Assert.True(result.TipoErro == ETipoErro.INVALID_ACCOUNT);
        }

        [Fact]
        public async Task CriarMovimentoAsync_DeveRetornarErroParaContaInativa()
        {
            // Arrange
            var request = new CriarMovimentoRequest
            {
                Numero = 123,
                Valor = 100,
                TipoMovimento = (ETipoMovimento)'C'
            };
            _contaCorrenteQueryRepository.GetByNumeroContaCorrenteIdAsync(Arg.Any<int>())
           .Returns(Task.FromResult(new ContaCorrente
           {
               Numero = 123,
               Ativo = 0,
               IdContaCorrente = "B6BAFC09-6967-ED11-A567-055DFA4A16C9",
               Nome = "Katherine Sanchez"
           }));
            // Act
            var result = await _movimentoService.CriarMovimentoAsync(request);

            // Assert
            Assert.False(result.Success);
            Assert.True(result.TipoErro == ETipoErro.INACTIVE_ACCOUNT);
        }

        [Theory]
        [InlineData(-100)]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task CriarMovimentoAsync_DeveRetornarErroParaValoresAbaixoDeZero(int valor)
        {
            // Arrange
            _contaCorrenteQueryRepository.GetByNumeroContaCorrenteIdAsync(Arg.Any<int>())
           .Returns(Task.FromResult(new ContaCorrente
           {
               Numero = 123,
               Ativo = 1,
               IdContaCorrente = "B6BAFC10-6967-ED11-A567-055DFA4A16C9",
               Nome = "Nome Teste"
           }));

            var request = new CriarMovimentoRequest
            {
                Numero = 123,
                Valor = valor,
                TipoMovimento = (ETipoMovimento)'C'
            };
            // Act
            var result = await _movimentoService.CriarMovimentoAsync(request);

            // Assert
            Assert.False(result.Success);
            Assert.True(result.TipoErro == ETipoErro.INVALID_VALUE);
        }
    }
}