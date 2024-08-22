using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Questao5.Domain.Enumerators;

namespace Questao5.Infrastructure.Services.Base
{
    [ApiController]
    public class ExercicioBaseController : ControllerBase
    {
        private readonly INotifier _notifier;

        public ExercicioBaseController(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected bool IsValidOperation()
        {
            return !_notifier.HasNotifications();
        }

        protected void NotificarErro(string mensagem, ETipoErro tipoErro)
        {
            _notifier.Handle(new Notification(mensagem, tipoErro));
        }

        protected void NotificarErro(List<ValidationFailure> mensagens)
        {
            foreach (var mensagem in mensagens)
            {
                var partes = mensagem.ErrorMessage.Split('|');
                var strMensagem = partes.Length > 0 ? partes[0] : string.Empty;
                var tipoErro = partes.Length > 1 ? Enum.Parse<ETipoErro>(partes[1]) : ETipoErro.INVALID_REQUEST;
                _notifier.Handle(new Notification(strMensagem, tipoErro));
            }
        }
        protected void NotificarErro(string mensagem)
        {            
                var partes = mensagem.Split('|');
                var strMensagem = partes.Length > 0 ? partes[0] : string.Empty;
                var tipoErro = partes.Length > 1 ? Enum.Parse<ETipoErro>(partes[1]) : ETipoErro.INVALID_REQUEST;
                _notifier.Handle(new Notification(strMensagem, tipoErro));            
        }
        protected ActionResult CustomResponse(object? result = null)
        {
            if (IsValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = _notifier.GetNotifications().Select(m => m.Message),
                tipoErro = _notifier.GetNotifications().Select(t => t.TipoErro)
            });
        }
    }
}
