using App.Domain.Events;

namespace App.Application.ServiceBus
{
    public interface IServiceBus
    {
        Task PublishAsync<T>(T message,CancellationToken cancellationToken = default) where T : IEventOrMessage;//exchange e gönderiyoruz
        Task SendAsync<T>(T message,string queueName,CancellationToken cancellationToken = default) where T : IEventOrMessage;//direkt kuyruğa gönderir
    }
}
