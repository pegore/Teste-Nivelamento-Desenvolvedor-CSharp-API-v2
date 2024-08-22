using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Handlers.Interfaces;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Services.Base;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaldoController : ExercicioBaseController
    {
        private readonly ISaldoContaCorrenteService _saldoService;

        public SaldoController(INotifier notifier, ISaldoContaCorrenteService saldoService) : base(notifier)
        {
            _saldoService = saldoService;
        }

        [HttpGet("{numerContaCorrente}")]
        public async Task<IActionResult> ConsultarSaldo(int numerContaCorrente)
        {
            try
            {
                var response = await _saldoService.ConsultarSaldoAsync(numerContaCorrente);
                return CustomResponse(response);
            }
            catch (Exception ex)
            {
                NotificarErro(ex.Message);
                return CustomResponse();
            }
        }
    }
}
