using System.Text.Json;
using Confluent.Kafka;
using NotificationService.Models;

namespace NotificationService;
public class Program
{
    public static async Task Main()
    {

        var gmailUser = Environment.GetEnvironmentVariable("GMAIL_USER")
            ?? throw new InvalidOperationException("Set GMAIL_USER");
        var gmailAppPassword = Environment.GetEnvironmentVariable("GMAIL_APP_PASSWORD")
            ?? throw new InvalidOperationException("Set GMAIL_APP_PASSWORD");

        IEmailSender emailSender = new SmtpEmailSender(
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
        consumer.Subscribe("orders");

        var cts = new CancellationTokenSource();
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };

        Console.WriteLine("NotificationService listening...");

        try
        {
            while (true)
            {
                var result = consumer.Consume(cts.Token);
                var order = JsonSerializer.Deserialize<OrderPlaced>(result.Message.Value);

                await emailSender.SendAsync(
                    to: order!.CustomerEmail,
                    subject: "Your order is confirmed",
                    body: $"Hi! We've received your order: {order.Quantity}x {order.Item}. Thank you!");

                Console.WriteLine($"Sent confirmation email to {order.CustomerEmail} for {order.Item}");
            }
        }
        catch (OperationCanceledException) {
            Console.WriteLine("Shutting down...");
        }
        finally
        {
            consumer.Close();
        }
    }
}
