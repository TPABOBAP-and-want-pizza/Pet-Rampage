using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;


public class Player : MonoBehaviourPun
{
    [SerializeField] private ItemInfo info;
    public Inventory inventory = new Inventory();
    private int highlightedSlotIndex = 0;
    private InventoryDisplay display;
    private GameObject selectedObject;
    private PhotonView view;
    private GameObject[] inventorySlots;

    public Text textName;

    void Start()
    {
        view = GetComponent<PhotonView>(); // ���������� �������� view
        if (view != null && view.Owner != null)
        {
            textName.text = view.Owner.NickName;
        }

        if (photonView.IsMine)
        {
            display = FindObjectOfType<InventoryDisplay>();
            display.AssignInventory(inventory);

            inventory.AddItem(info, 1);
            InstantiateItemInHand();
        }
    }
    private void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0f)
        {
            Debug.Log($"scrollInput = {scrollInput}");

            highlightedSlotIndex += (scrollInput > 0f) ? 1 : -1;

            highlightedSlotIndex = (highlightedSlotIndex + inventory.Slots.Length) % inventory.Slots.Length;
            InstantiateItemInHand();
            HighlightSelectedSlot();
        }
    }
    private void InstantiateItemInHand()
    {
        if (photonView.IsMine)
        {
            if (selectedObject != null)
                Destroy(selectedObject);

            string name = inventory.CheckSelectedItem(highlightedSlotIndex);
            Debug.Log($"name = {name}");

            if (name != null)
            {
                object[] instantiationData = { photonView.ViewID };
                selectedObject = PhotonNetwork.Instantiate("Prefabs/" + name, transform.position, Quaternion.identity, 0, instantiationData);
                selectedObject.transform.SetParent(transform.GetComponent<PhotonView>().transform);

                transform.GetComponent<PlayerMovement>().SetSelectedTransform(selectedObject.transform);
                Debug.Log("parent = " + selectedObject.transform.parent);
                HighlightSelectedSlot();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13)//13 = pickableItem
        {
            PickableItem item = collision.gameObject.GetComponent<PickableItem>();
            inventory.AddItem(item.Item, item.Count);
            InstantiateItemInHand();

            PhotonNetwork.Destroy(item.gameObject);
        }
    }
    public void RemoveSelectedItem()
    {
        inventory.RemoveItem(inventory.Slots[highlightedSlotIndex].Item);
    }

    private void ClearAllSlotHighlights()
    {
        foreach (var slot in inventorySlots)
        {
            slot.GetComponent<Image>().color = Color.white; // ���������� ���� ���� ����� ��������� �� ����� (��� �� ��� �����)
        }
    }
    private void HighlightSelectedSlot()
    {
        display.SetSelectedCell(highlightedSlotIndex);
    }

}