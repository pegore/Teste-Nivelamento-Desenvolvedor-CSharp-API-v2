using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Handlers.Interfaces;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Repositories.Commands;
using Questao5.Domain.Repositories.Queries;

namespace Questao5.Application.Handlers
{
    public class MovimentoService : IMovimentoService
    {
        private readonly IMovimentoCommandRepository _movimentoCommandRepository;
        private readonly IContaCorrenteQueryRepository _contaCorrenteRepository;

        public MovimentoService(IMovimentoCommandRepository movimentoCommandRepository, IContaCorrenteQueryRepository contaCorrenteRepository)
        {
            _movimentoCommandRepository = movimentoCommandRepository;
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        public async Task<bool> VerificarContaCorrenteAsync(int contaCorrenteId)
        {
            ContaCorrente contaCorrente = await _contaCorrenteRepository.GetByNumeroContaCorrenteIdAsync(contaCorrenteId);
            return contaCorrente != null && contaCorrente.Ativo == 1;
        }

        public async Task<CreateTransacaoResponse> CriarMovimentoAsync(CriarMovimentoRequest request)
        {
            var contaCorrente = await _contaCorrenteRepository.GetByNumeroContaCorrenteIdAsync(request.Numero);

            if (contaCorrente == null)
            {
                return new CreateTransacaoResponse
                {
                    Success = false,
                    Message = "Apenas contas correntes cadastradas podem receber movimentação.",
                    TipoErro = ETipoErro.INVALID_ACCOUNT
                };
            }

            if (contaCorrente.Ativo == 0)
            {
                return new CreateTransacaoResponse
                {
                    Success = false,
                    Message = "Apenas contas correntes ativas podem receber movimentação.",
                    TipoErro = ETipoErro.INACTIVE_ACCOUNT
                };
            }

            if (request.Valor <= 0)
            {
                return new CreateTransacaoResponse
                {
                    Success = false,
                    Message = "Apenas valores positivos podem ser recebidos.",
                    TipoErro = ETipoErro.INVALID_VALUE
                };
            }

            if (!Enum.IsDefined(typeof(ETipoMovimento), request.TipoMovimento))
            {
                return new CreateTransacaoResponse
                {
                    Success = false,
                    Message = "Tipo de movimento inválido. Use 'C' para crédito ou 'D' para débito.",
                    TipoErro = ETipoErro.INVALID_TYPE
                };
            }

            var movimento = new Movimento
            {
                IdContaCorrente = contaCorrente.IdContaCorrente.ToString().ToUpper(),
                Valor = request.Valor,
                TipoMovimento = request.TipoMovimento.ToString(),
                DataMovimento = DateTime.Now
            };

            var resultado = await _movimentoCommandRepository.AddMovimentoAsync(movimento);

            return new CreateTransacaoResponse
            {
                Id = Guid.Parse(resultado.IdMovimento),
                Success = true
            };
        }
    }
}
