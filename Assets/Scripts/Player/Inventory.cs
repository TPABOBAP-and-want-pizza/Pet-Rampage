using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

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
        foreach (ItemSlot slot in slots)
        {
            ItemInfo slotInfo = slot.Item;
            if (slotInfo == null)
            {
                slot.AddItem(info, count);
                break;
            }
            if (info.stackable)
            {
                if (slotInfo.id == info.id)
                {
                    if (slot.Count < slotInfo.maxCount)
                    {
                        slot.AddItem(info, count);
                        break;
                    }
                }
            }
        }
    }
}
