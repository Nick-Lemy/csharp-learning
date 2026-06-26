using System.Text.Json;
using Confluent.Kafka;
using NotificationService.Models;
using NotificationService.Services;


var gmailUser = Environment.GetEnvironmentVariable("GMAIL_USER")
    ?? throw new InvalidOperationException("Set GMAIL_USER");
var gmailAppPassword = Environment.GetEnvironmentVariable("GMAIL_APP_PASSWORD")
    ?? throw new InvalidOperationException("Set GMAIL_APP_PASSWORD");

var emailSender = new EmailSender(
    host: "smtp.gmail.com",
    port: 587,
    user: gmailUser,
    password: gmailAppPassword,
    fromAddress: gmailUser);

var config = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "notification-service",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using var consumer = new ConsumerBuilder<string, string>(config).Build();
consumer.Subscribe("inventory-events");

var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };

Console.WriteLine("NotificationService listening...");

while (true)
{
    try
    {
        var result = consumer.Consume(cts.Token);

        var order = JsonSerializer.Deserialize<OrderProcessed>(result.Message.Value);

        if(order is null) continue;

        if(order.Reserved)
        {
            await emailSender.SendAsync(
                to: order.CustomerEmail,
                subject: "Your order is confirmed",
                body: $"Good news! We've reserved {order.Quantity}x {order.Item}. Thank you!",
                cancellationToken: cts.Token);

                Console.WriteLine($"Sent CONFIRMATION to {order.CustomerEmail} for {order.Item}");
        }
        else
        {
            await emailSender.SendAsync(
                to: order.CustomerEmail,
                subject: "Problem with your order",
                body: $"Sorry, we couldn't fulfill {order.Quantity}x {order.Item} (only {order.Available} in stock).",
                cancellationToken: cts.Token);

                Console.WriteLine($"Sent OUT_OF_STOCK notice to {order.CustomerEmail} for {order.Item}");
        }
    }
    catch (OperationCanceledException)
    {
        break;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Skipping message: {ex.Message}");
    }
}

Console.WriteLine("Shutting down...");
consumer.Close();
