namespace NotificationService.Models;

public record OrderProcessed(
    Guid OrderId,
    string CustomerEmail,
    string Item,
    int Quantity,
    bool Reserved,
    int Available); 