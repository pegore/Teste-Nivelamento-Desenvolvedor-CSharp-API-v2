using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Handlers.Interfaces;
using Questao5.Infrastructure.Services.Base;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimentoController : ExercicioBaseController
    {
        private readonly IMovimentoService _movimentoService;
        private readonly IValidator<CriarMovimentoRequest> _validator;

        public MovimentoController(INotifier notifier,
                                   IMovimentoService movimentoService,
                                   IValidator<CriarMovimentoRequest> validator) : base(notifier)
        {
            _movimentoService = movimentoService;
            _validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> CriarMovimento([FromBody] CriarMovimentoRequest request)
        {
            var validationResult = _validator.Validate(request);

            if (!validationResult.IsValid)
            {
                NotificarErro(validationResult.Errors);
            }
            //var contaCorrenteValida = await _movimentoService.VerificarContaCorrenteAsync(request.Numero);
            //if (!contaCorrenteValida)
            //{
            //    NotificarErro(new string[] { $"Conta corrente não encontrada ou inativa.", "INVALID_ACCOUNT" });
            //}
            var response = await _movimentoService.CriarMovimentoAsync(request);

            if (!response.Success)
            {
                NotificarErro(response.Message, response.TipoErro);
                return CustomResponse();
            }
            return CustomResponse(response.Id);
        }
    }
}
