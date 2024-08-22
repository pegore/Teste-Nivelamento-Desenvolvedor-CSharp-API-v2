namespace Questao5.Infrastructure.Services.Base
{
    public interface INotifier
    {
        bool HasNotifications();

        List<Notification> GetNotifications();

        void Handle(Notification notificacao);
    }
}
