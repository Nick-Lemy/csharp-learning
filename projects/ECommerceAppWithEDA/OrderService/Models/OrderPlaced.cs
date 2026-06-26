namespace OrderService.Models;

public record OrderPlaced(
    Guid OrderId,
    Guid CustomerId,
    string CustomerEmail,
    string Item,
    int Quantity,
    decimal Price,
    DateTimeOffset PlacedAt);