using OrderService;
using OrderService.Models;

using var publisher = new OrderPublisher("localhost:9092");

Console.WriteLine("=== Order Service ===");
Console.Write("Enter your email: ");
var email = Console.ReadLine()?.Trim();

if (string.IsNullOrWhiteSpace(email))
{
    Console.WriteLine("No email entered. Exiting.");
    return;
}

while (true)
{
    Console.WriteLine("\nAvailable products:");
    for (int i = 0; i < Catalog.Products.Count; i++)
        Console.WriteLine($"  {i + 1}. {Catalog.Products[i].Name} - ${Catalog.Products[i].Price}");

    Console.Write("Pick a product number (or 'q' to quit): ");
    var choice = Console.ReadLine()?.Trim();
    if (choice == "q") break;

    if (!int.TryParse(choice, out int index) || index < 1 || index > Catalog.Products.Count)
    {
        Console.WriteLine("Invalid choice, try again.");
        continue;
    }
    var product = Catalog.Products[index - 1];

    Console.Write("Quantity: ");
    if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity < 1)
    {
        Console.WriteLine("Invalid quantity, defaulting to 1.");
        quantity = 1;
    }

    var order = new OrderPlaced(
        OrderId: Guid.NewGuid(),
        CustomerId: Guid.NewGuid(),
        CustomerEmail: email,
        Item: product.Name,
        Quantity: quantity,
        Price: product.Price,
        PlacedAt: DateTime.UtcNow);

    await publisher.PublishAsync(order);
    Console.WriteLine($"Order placed: {order.Quantity}x {order.Item} for {email}");
}

Console.WriteLine("Goodbye!");