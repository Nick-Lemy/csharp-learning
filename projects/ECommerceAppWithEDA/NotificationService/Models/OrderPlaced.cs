namespace NotificationService.Models;

record OrderPlaced(
    Guid OrderId,
    Guid CustomerId,
    string CustomerEmail,
    string Item,
    int Quantity,
    decimal Price,
    DateTime PlacedAt);
