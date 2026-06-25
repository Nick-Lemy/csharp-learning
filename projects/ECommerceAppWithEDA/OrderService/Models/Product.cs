namespace OrderService.Models;

public record Product(string Name, decimal Price);

public static class Catalog
{
    public static readonly IReadOnlyList<Product> Products = new List<Product>
    {
        new("Book",       12.99m),
        new("Mug",         8.50m),
        new("Keyboard",   45.00m),
        new("Pen",         2.25m),
        new("Headphones", 79.99m),
    };
}