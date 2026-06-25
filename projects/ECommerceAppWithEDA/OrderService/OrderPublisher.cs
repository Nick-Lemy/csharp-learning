using System.Text.Json;
using Confluent.Kafka;
using OrderService.Models;

namespace OrderService;

public class OrderPublisher : IDisposable
{
    private readonly IProducer<string, string> _producer;
    private const string Topic = "orders";

    public OrderPublisher(string bootstrapServers)
    {
        var config = new ProducerConfig { BootstrapServers = bootstrapServers };
        _producer = new ProducerBuilder<string, string>(config).Build();
    }

    public async Task PublishAsync(OrderPlaced order)
    {
        var json = JsonSerializer.Serialize(order);
        await _producer.ProduceAsync(Topic,
            new Message<string, string> { Key = order.CustomerId.ToString(), Value = json });
    }

    public void Dispose()
    {
        _producer.Flush(TimeSpan.FromSeconds(5));
        _producer.Dispose();
    }
}