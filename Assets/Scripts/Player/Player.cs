using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviourPun
{
    public Inventory inventory = new Inventory();
    private InventoryDisplay display;

    void Start()
    {
        if (photonView.IsMine)
        {
            display = FindObjectOfType<InventoryDisplay>();
            display.AssignInventory(inventory);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13)//13 = pickableItem
        {
            PickableItem item = collision.gameObject.GetComponent<PickableItem>();
            inventory.AddItem(item.Item, item.Count);

            Photon.Pun.PhotonNetwork.Destroy(item.gameObject);
        }
    }
}
