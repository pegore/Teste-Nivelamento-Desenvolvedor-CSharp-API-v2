using Questao5.Domain.Enumerators;

namespace Questao5.Infrastructure.Services.Base
{
    public class Notification
    {
        public string Message { get; set; }
        public string TipoErro { get; set; }

        public Notification(string message, ETipoErro tipoErro)
        {
            Message = message;
            TipoErro = tipoErro.ToString();
        }
    }
}