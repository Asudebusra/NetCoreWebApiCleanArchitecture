using App.Domain.Events;
using MassTransit;

namespace App.Bus.Consumers
{//buraada herhangi bir repository e erişim yapmayacağız sadece event i dinleyip loglama yapacağız veya başka bir servisle iletişime geçeceğiz.IProductRepository kullanmayacağız 
    public class ProductAddedEventConsumer : IConsumer<ProductAddedEvent>
    {
        public Task Consume(ConsumeContext<ProductAddedEvent> context)
        {
            Console.WriteLine($"Gelen Event: {context.Message.Id} - {context.Message.Name} - {context.Message.Price}");

            return Task.CompletedTask;
        }//Kuyruk sistemleri binary formatta data beklerler
    }
}
