using System.Text.Json;
using Confluent.Kafka;
using InventoryService;
using InventoryService.Models;

var store = new InventoryStore();
using var publisher = new InventoryEventPublisher("localhost:9092");   // NEW

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

        if(order is null) continue;

        bool reserved = store.TryReserve(order.Item, order.Quantity, out int remaining);

        if (reserved) Console.WriteLine($"Reserved {order.Quantity}x {order.Item}. {remaining} left.");
        else Console.WriteLine($"Could not reserve {order.Quantity}x {order.Item} ({remaining} available).");

        await publisher.PublishAsync(new OrderProcessed(
            OrderId: order.OrderId,
            CustomerEmail: order.CustomerEmail,
            Item: order.Item,
            Quantity: order.Quantity,
            Reserved: reserved,
            Available: remaining));
    }
}
catch (OperationCanceledException)
{
    Console.WriteLine("Closing InventoryService...");
}
finally
{
    consumer.Close();
}