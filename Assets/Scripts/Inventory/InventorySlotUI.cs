using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    public Image itemIcon = null;
    public TextMeshProUGUI quantityText = null;

    public InventorySlot InventorySlot { get; private set; } = null;

    public void Initialize(InventorySlot slot)
    {
        InventorySlot = slot;
        UpdateUI();
    }

    public void UpdateUI()
    {
        itemIcon.sprite = InventorySlot.ItemData.Sprite;
        
        quantityText.text = InventorySlot.Quantity > 1 ? "x" + InventorySlot.Quantity : "";
    }
}
