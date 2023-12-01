using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private ItemDisplay itemDisplayPrefab;
    private Inventory assignedInventory;
    private List<ItemDisplay> displays = new List<ItemDisplay>();

    private int selectedCellIndex = -1; // ������ ��������� ������

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
            AddAtHotBar(slot, i); // �������� ������� ������
        }
    }

    private void AddAtHotBar(ItemSlot slot, int index)
    {
        ItemDisplay itemDisplay = CreateDisplay(transform);
        WireUpEvents(itemDisplay, slot);
        displays.Add(itemDisplay);

        // ���� ������� ������ ��������� � ���������, ���������� ������� � 1.2
        if (index == selectedCellIndex)
        {
            itemDisplay.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
        }
        else
        {
            itemDisplay.transform.localScale = Vector3.one; // ����� �������� ��� ������ �����
        }
    }

    private void WireUpEvents(ItemDisplay display, ItemSlot slot)
    {
        slot.InfoUpdated += display.DisplayItem;
    }

    private ItemDisplay CreateDisplay(Transform parent = null)
    {
        ItemDisplay newItemDisplay = Instantiate(itemDisplayPrefab, parent);
        newItemDisplay.transform.localScale = Vector3.one; // ��������� �������� �� ��������� ��� ��������
        return newItemDisplay;
    }

    private void Clear()
    {
        foreach (ItemDisplay display in displays)
        {
            Destroy(display.gameObject);
        }
        displays.Clear();
    }

    // ����� ��� ��������� ��������� ������
    public void SetSelectedCell(int cellIndex)
    {
        selectedCellIndex = cellIndex;

        // �������� �� ������ ����������� � ���������� ������� ������ ��� ��������� ������
        for (int i = 0; i < displays.Count; i++)
        {
            if (i == selectedCellIndex)
            {
                displays[i].SetScale(new Vector3(1.2f, 1.2f, 1f));
            }
            else
            {
                displays[i].SetScale(Vector3.one); // ����� �������� ��� ������ �����
            }
        }
    }
}