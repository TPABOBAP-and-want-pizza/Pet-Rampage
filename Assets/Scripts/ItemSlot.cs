using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemSlot
{
    public ItemInfo Item { get; private set; }
    public int Count { get; private set; }
    public event Action<ItemInfo, int> InfoUpdated;

    public void AddItem(ItemInfo item, int count = 1)
    {
        if(Item == null)
            Item = item;
        Count += count;
        InfoUpdated?.Invoke(Item, Count);
    }   
}
