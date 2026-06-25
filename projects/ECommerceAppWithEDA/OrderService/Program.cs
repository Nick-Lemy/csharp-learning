using System.Text.Json;
using Confluent.Kafka;

namespace OrderService;

class Program
{
    static async Task Main(string[] args)
    {
        var config = new ProducerConfig 
        {
        BootstrapServers = "localhost:9092", 
        Acks = Acks.All
        };
        using var producer = new ProducerBuilder<string, string>(config).Build();

        var items = new[] { "Book", "Mug", "Keyboard", "Pen", "Headphones" };
        var rng = new Random();

        Console.WriteLine("Press ENTER to place an order, or 'q' then ENTER to quit.");

        while (true)
        {
            var input = Console.ReadLine();
            if (input == "q") break;

            var customerId = Guid.NewGuid();
            var order = new OrderPlaced(
                OrderId: Guid.NewGuid(),
                CustomerId: customerId,
                CustomerEmail: $"customer{rng.Next(1000)}@example.com",
                Item: items[rng.Next(items.Length)],
                Quantity: rng.Next(1, 5),
                Price: rng.Next(5, 100),
                PlacedAt: DateTimeOffset.UtcNow
                );

            var json = JsonSerializer.Serialize(order);

            await producer.ProduceAsync("orders",
                new Message<string, string> { Key = customerId.ToString(), Value = json });

            Console.WriteLine($"Placed order: {order.Item} x{order.Quantity}");
        }

        producer.Flush(TimeSpan.FromSeconds(5));
    }
}

record OrderPlaced(
    Guid OrderId,
    Guid CustomerId,
    string CustomerEmail,
    string Item,
    int Quantity,
    decimal Price,
    DateTimeOffset PlacedAt);
