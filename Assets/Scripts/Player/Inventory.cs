using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory
{
    private ItemSlot[] slots = new ItemSlot[27];

    public Inventory()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i] = new ItemSlot();
        }
    }

    public ItemSlot[] Slots => slots;

    public void AddItem(ItemInfo info, int count)
    {
        if (info == null)
            return;
        Debug.Log("!null");
        if (info.stackable)
        {
            foreach (ItemSlot slot in slots)
            {
                ItemInfo slotInfo = slot.Item;
                if (CheckStackableItem(slot, slotInfo, info, count)) 
                    return;
            }
        }
        foreach (ItemSlot slot in slots)
        {
            ItemInfo slotInfo = slot.Item;
            if (slotInfo == null)
            {
                slot.AddItem(info, count);
                break;
            }
        }
    }

    private bool CheckStackableItem(ItemSlot slot, ItemInfo slotInfo, ItemInfo info, int count)
    {
        if (slotInfo != null && slotInfo.id == info.id)
        {
            if (slot.Count < slotInfo.maxCount)
            {
                slot.AddItem(info, count);
                return true;
            }
        }
        return false;
    }
}
