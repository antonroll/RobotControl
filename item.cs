namespace InventorySystem;

public class Item
{
    public string Name { get; set; } = string.Empty;
    public decimal PricePerUnit { get; set; }
    public uint InventoryLocation { get; set; } // <- bruges af robotten
}
