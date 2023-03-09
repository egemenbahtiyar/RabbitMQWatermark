using System.Text;
using System.Text.Json;

namespace RabbitMQWatermark.Services;

public class RabbitMQPublisher
{
    private readonly RabbitMQClientService _rabbitMQClientService;

    public RabbitMQPublisher(RabbitMQClientService rabbitMQClientService)
    {
        _rabbitMQClientService = rabbitMQClientService;
    }

    public void Publish(ProductImageCreatedEvent productImageCreatedEvent)
    {
        var channel = _rabbitMQClientService.Connect();

        var bodyString = JsonSerializer.Serialize(productImageCreatedEvent);

        var bodyByte = Encoding.UTF8.GetBytes(bodyString);
        
        channel.BasicPublish(exchange: RabbitMQClientService.ExchangeName,
            routingKey: RabbitMQClientService.RoutingWatermark,
            basicProperties: null,
            body: bodyByte,
            mandatory: false);
    }
}