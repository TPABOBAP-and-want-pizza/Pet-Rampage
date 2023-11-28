using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Inventory inventory = new Inventory();
    private InventoryDisplay display;
    void Start()
    {
        display = GameObject.FindObjectOfType<InventoryDisplay>();
        display.AssignInventory(inventory);

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))//нада
            CheckInventory();
    }
    private void CheckInventory()
    {
        display.gameObject.SetActive(!display.gameObject.activeSelf);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13)//13 = pickableItem
        {
            PickableItem item = collision.gameObject.GetComponent<PickableItem>();
            inventory.AddItem(item.Item, item.Count);

            Destroy(item.gameObject);
        }
    }
}
