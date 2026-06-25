using System.Text.Json;
using Confluent.Kafka;

namespace InventoryService;
public class Program
{
    public static void Main()
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "inventory-service",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using var consumer = new ConsumerBuilder<string, string>(config).Build();
        consumer.Subscribe("orders");


        var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };

        Console.WriteLine("InventoryService listening...");

        try
        {
            while (true)
            {
                var result = consumer.Consume(cts.Token);
                var order = JsonSerializer.Deserialize<OrderPlaced>(result.Message.Value);
                Console.WriteLine($"Reserved {order!.Quantity}x {order.Item} for order {order.OrderId}");
            }
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Shutting down...");
        }
        finally
        {
            consumer.Close();
        }
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