namespace InventoryService;

public class InventoryStore
{
    private readonly Dictionary<string, int> _stock = new()
    {
        ["Book"]       = 10,
        ["Mug"]        = 5,
        ["Keyboard"]   = 3,
        ["Pen"]        = 20,
        ["Headphones"] = 2,
    };

    public bool TryReserve(string item, int quantity, out int remaining)
    {
        if (!_stock.TryGetValue(item, out int available))
        {
            remaining = 0;
            return false;
        }

        if (available < quantity)
        {
            remaining = available;
            return false;
        }

        _stock[item] = available - quantity;
        remaining = _stock[item];
        return true;
    }
}