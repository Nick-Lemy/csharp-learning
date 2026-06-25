using System;
using System.Threading;
using Confluent.Kafka;
using Confluent.Kafka.Admin;

class Consumer
{
    static void Main(string[] args)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:45717",
            GroupId = "kafka-dotnet-getting-started",
            AutoOffsetReset = AutoOffsetReset.Earliest,
        };

        const string topic = "purchases";

        CancellationTokenSource cts = new ();
        Console.CancelKeyPress += (_, e) =>
        {
            e.Cancel = true;
            cts.Cancel();
        };

        using (var consumer = new ConsumerBuilder<string, string>(config).Build())
        {
            consumer.Subscribe(topic);
            try
            {
                while (true)
                {
                    var cr = consumer.Consume(cts.Token);
                    Console.WriteLine($"Consumed event from topic {topic}: key = {cr.Message.Key, -10} value = {cr.Message.Value}");
                    
                }
            }
            catch (OperationCanceledException)
            {
                
            }
            finally
            {
                consumer.Close();
            }
        }
    }
}