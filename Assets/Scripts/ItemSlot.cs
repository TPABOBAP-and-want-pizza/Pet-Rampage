using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot
{
    public ItemInfo Item { get; private set; }
    public int Count { get; private set; }
    public event System.Action<ItemInfo, int> InfoUpdated;
    public event System.Action ItemRemoved;
    public void AddItem(ItemInfo item, int count = 1)
    {
        Item = item;
        Count += count;
        InfoUpdated?.Invoke(Item, Count);
    }

    // ����� ��� �������� �������� �� �����
    public void RemoveItem()
    {
        Item = null;
        Count = 0;
        InfoUpdated?.Invoke(Item, Count);
    }

    public void RemoveOneItem(int count)
    {
        if (Count > 0)
        {
            Count -= count; // ��������� ���������� ��������� � ����� �� ����
            InfoUpdated?.Invoke(Item, Count);
            if (Count == 0)
            {
                Item = null;
            }
        }
    }

}
