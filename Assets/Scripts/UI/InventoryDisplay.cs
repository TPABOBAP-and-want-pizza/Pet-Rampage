using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    private const int HotBarSize = 9;
    [SerializeField] private ItemDisplay itemDisplayPrefab;
    [SerializeField] private Transform inventorySlotsParent;
    [SerializeField] private Transform hotBarSlotsParent;
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
            if (i < HotBarSize) { 
                AddAtHotBar(slot);
            }
            else AddAtInventory(slot);
        }
    }

    private void AddAtHotBar(ItemSlot slot)
    {
        ItemDisplay itemDisplay = CreateDisplay(hotBarSlotsParent);
        WireUpEvents(itemDisplay, slot);
        displays.Add(itemDisplay);
    }

    private void AddAtInventory(ItemSlot slot)
    {
        ItemDisplay itemDisplay = CreateDisplay(inventorySlotsParent);
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
