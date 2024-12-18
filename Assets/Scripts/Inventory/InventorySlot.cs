using UnityEngine;

public class InventorySlot
{
    public ItemData ItemData { get; private set; } = null;
    public int Quantity { get; private set; } = 0;

    public InventorySlot(ItemData itemData, int quantity)
    {
        ItemData = itemData;
        Quantity = quantity;
    }

    public void Add(int amount)
    {
        Quantity += amount;
    }

    public void Remove(int amount)
    {
        Quantity = Mathf.Max(0, Quantity - amount);
    }
}
