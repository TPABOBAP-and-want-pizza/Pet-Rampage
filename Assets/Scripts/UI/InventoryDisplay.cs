using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private ItemDisplay itemDisplayPrefab;
    private Inventory assignedInventory;
    private List<ItemDisplay> displays = new List<ItemDisplay>();

    private int selectedCellIndex = -1; // »ндекс выбранной €чейки

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
            AddAtHotBar(slot, i); // ѕередача индекса €чейки
        }
    }

    private void AddAtHotBar(ItemSlot slot, int index)
    {
        ItemDisplay itemDisplay = CreateDisplay(transform);
        WireUpEvents(itemDisplay, slot);
        displays.Add(itemDisplay);

        // ≈сли текущий индекс совпадает с выбранным, установите масштаб в 1.2
        if (index == selectedCellIndex)
        {
            itemDisplay.transform.localScale = new Vector3(1.2f, 1.2f, 1f);
        }
        else
        {
            itemDisplay.transform.localScale = Vector3.one; // —брос масштаба дл€ других €чеек
        }
    }

    private void WireUpEvents(ItemDisplay display, ItemSlot slot)
    {
        slot.InfoUpdated += display.DisplayItem;
    }

    private ItemDisplay CreateDisplay(Transform parent = null)
    {
        ItemDisplay newItemDisplay = Instantiate(itemDisplayPrefab, parent);
        newItemDisplay.transform.localScale = Vector3.one; // ”становка масштаба по умолчанию при создании
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

    // ћетод дл€ установки выбранной €чейки
    public void SetSelectedCell(int cellIndex)
    {
        selectedCellIndex = cellIndex;

        // ѕройдите по списку отображени€ и установите масштаб только дл€ выбранной €чейки
        for (int i = 0; i < displays.Count; i++)
        {
            if (i == selectedCellIndex)
            {
                displays[i].SetScale(new Vector3(1.2f, 1.2f, 1f));
            }
            else
            {
                displays[i].SetScale(Vector3.one); // —брос масштаба дл€ других €чеек
            }
        }
    }
}