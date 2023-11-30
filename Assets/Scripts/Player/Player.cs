using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviourPun
{
    public Inventory inventory = new Inventory();
    private int highlightedSlotIndex = 0;
    private InventoryDisplay display;
    private GameObject selectedObject;

    void Start()
    {
        if (photonView.IsMine)
        {
            display = FindObjectOfType<InventoryDisplay>();
            display.AssignInventory(inventory);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            PhotonNetwork.Instantiate("Items/BananaGun", transform.position + Vector3.left, Quaternion.identity);

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0f)
        {
            Debug.Log($"scrollInput = {scrollInput}");

            highlightedSlotIndex += (scrollInput > 0f) ? 1 : -1;

            highlightedSlotIndex = (highlightedSlotIndex + inventory.Slots.Length) % inventory.Slots.Length;
            InstantiateItemInHand();
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
                selectedObject = Instantiate(Resources.Load("Prefabs/" + name) as GameObject, transform.position, Quaternion.identity, transform);
                transform.GetComponent<PlayerMovement>().SetSelectedTransform(selectedObject?.transform);
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

            Photon.Pun.PhotonNetwork.Destroy(item.gameObject);
        }
    }
}
