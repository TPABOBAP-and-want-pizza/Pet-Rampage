using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private ItemDisplay itemDisplayPrefab;
    private Inventory assignedInventory;
    private List<ItemDisplay> displays = new List<ItemDisplay>();
    
    public void AssignInventory(Inventory inventory)
    {
        assignedInventory = inventory;
        ShowInventory();
    }
    private void ShowInventory()
    {
        Clear();
        for (int i = 0; i < assignedInventory.Slots.Length; i++)
        {
            ItemSlot slot = assignedInventory.Slots[i];
            AddAtHotBar(slot);

        }
    }

    private void AddAtHotBar(ItemSlot slot)
    {
        ItemDisplay itemDisplay = CreateDisplay(transform);
        WireUpEvents(itemDisplay, slot);
        displays.Add(itemDisplay);
    }

    private void WireUpEvents(ItemDisplay display, ItemSlot slot)
    {
        slot.InfoUpdated += display.DisplayItem;
    }

    private ItemDisplay CreateDisplay(Transform parent = null)
    {
        return Instantiate(itemDisplayPrefab, parent);
    }

    private void Clear()
    {
        foreach(ItemDisplay display in displays)
        {
            Destroy(display.gameObject);
        }
        displays.Clear();
    }
}
