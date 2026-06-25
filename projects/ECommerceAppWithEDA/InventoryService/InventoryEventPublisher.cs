  using System.Text.Json;
  using Confluent.Kafka;
  using InventoryService.Models;

  namespace InventoryService;

  public class InventoryEventPublisher : IDisposable
  {
      private readonly IProducer<string, string> _producer;
      private const string Topic = "inventory-events";

      public InventoryEventPublisher(string bootstrapServers)
      {
          var config = new ProducerConfig { BootstrapServers = bootstrapServers };
          _producer = new ProducerBuilder<string, string>(config).Build();
      }

      public async Task PublishAsync(OrderProcessed evt)
      {
          var json = JsonSerializer.Serialize(evt);
          await _producer.ProduceAsync(Topic,
              new Message<string, string> { Key = evt.OrderId.ToString(), Value = json });
      }

      public void Dispose()
      {
          _producer.Flush(TimeSpan.FromSeconds(5));
          _producer.Dispose();
      }
  }