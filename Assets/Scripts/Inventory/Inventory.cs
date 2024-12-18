using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    private List<InventorySlot> m_slots = new();

    private const int MAX_SLOTS = 6;

    public void Initialize()
    {
        var defaultItem = new InventorySlot(ScriptableObject.CreateInstance<ItemData>(), 0);
        InventoryUIManager.Instance.InitUI(defaultItem);
    }

    public void AddItem(ItemData item, int quantity = 1)
    {
        if (m_slots.Count >= MAX_SLOTS && !ContainsItem(item))
        {
            Debug.Log("Inventory is full!");
            return;
        }
        
        var existingSlot = FindSlot(item);
        if (existingSlot != null)
        {
            existingSlot.Add(quantity);

            Debug.Log("Updated existing slot " + existingSlot);
            
            InventoryUIManager.Instance.UpdateSlotUI(existingSlot, m_slots.IndexOf(existingSlot));
            return;
        }

        var newSlot = new InventorySlot(item, quantity);
        m_slots.Add(newSlot);
        
        Debug.Log("Added new slot");
        
        InventoryUIManager.Instance.UpdateSlotUI(newSlot, m_slots.Count - 1);
    }

    public void RemoveItem(ItemData item, int quantity = 1)
    {
        var slot = FindSlot(item);
        
        if (slot == null)
        {
            Debug.Log("Item not found in inventory.");
            return;
        }
        
        if (slot.Quantity < quantity)
        {
            Debug.Log("Not enough quantity to remove.");
            return;
        }
        
        slot.Remove(quantity);
        if (slot.Quantity <= 0)
        {
            var defaultItem = new InventorySlot(ScriptableObject.CreateInstance<ItemData>(), 0);
            InventoryUIManager.Instance.ResetSlotUI(defaultItem, m_slots.IndexOf(slot));
            
            m_slots.Remove(slot);
            return;
        }
        
        InventoryUIManager.Instance.UpdateSlotUI(slot, m_slots.IndexOf(slot));
    }

    private InventorySlot FindSlot(ItemData itemData)
    {
        return m_slots.Find(slot => slot.ItemData == itemData);
    }
    
    private bool ContainsItem(ItemData itemData)
    {
        return m_slots.Any(slot => slot.ItemData == itemData);
    }
}
