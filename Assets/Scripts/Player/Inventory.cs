using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private ItemSlot[] slots = new ItemSlot[9];

    public Inventory()
    {
        for (int i = 0; i < slots.Length; i++)
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

    public string CheckSelectedItem(int selected)
    {
        if (selected >= 0 && selected < slots.Length)
        {
            if (slots[selected] != null && slots[selected].Item != null)
            {
                return slots[selected].Item.name;
            }
        }
        return null; // Возвращаем null, если выбранная ячейка пуста или недействительна
    }

    // Проверка наличия предмета в инвентаре
    public bool HasItem(ItemInfo info)
    {
        foreach (ItemSlot slot in slots)
        {
            if (slot.Item == info)
            {
                return true;
            }
        }
        return false;
    }
    public bool HasItem(ItemInfo info, int count)
    {
        foreach (ItemSlot slot in slots)
        {
            if (slot.Item == info && slot.Count >= count)
            {
                return true;
            }
        }
        return false;
    }
    public void RemoveItem(ItemInfo info, int count)
    {
        foreach (ItemSlot slot in slots)
        {
            if (slot.Item == info)
            {
                slot.RemoveOneItem(count);
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
