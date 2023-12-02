using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviourPunCallbacks
{
    private Inventory playerInventory;
    [SerializeField] private LayerMask ignoreLayer;
    [SerializeField] private GameObject buildingPrefab_item;
    [SerializeField] private GameObject buildingPrefab_block;
    [SerializeField] private int resursesCount = 4;
    [SerializeField] private float buildDistance = 4;
    private Player playerScript;

    private void Start()
    {
        if (photonView.IsMine)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            playerScript = playerObj.GetComponent<Player>();

            Player localPlayer = playerObj.GetComponent<Player>();

            if (localPlayer != null)
            {
                playerInventory = localPlayer.inventory;
                
            }
            else
            {
                Debug.LogError("Local player inventory not found!");
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Mouse(1)");
            BuildBlock();
        }
    }

    private void BuildBlock()
    {
        if (playerInventory.HasItem(buildingPrefab_item.GetComponent<PickableItem>().Item, resursesCount))
        {

            

            int activeSlotIndex = playerScript.GetHighlightedSlotIndex();
            string itemName = playerScript.inventory.CheckSelectedItem(activeSlotIndex);

            if (itemName == "Wood") // �������� "tree" �� ���� ������, ������������ ������
            {
                Vector3 newPosition = GetMousePositionInWorld();
                if(newPosition != Vector3.zero)
            {
                PhotonNetwork.Instantiate($"Items/{buildingPrefab_block.name}", newPosition, Quaternion.identity);
                playerInventory.RemoveItem(buildingPrefab_item.GetComponent<PickableItem>().Item, resursesCount);
            }
            }
        }
    }

    private Vector3 GetMousePositionInWorld()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 v2p = new Vector2(transform.position.x, transform.position.y);
        Vector2 v2m = new Vector2(worldPos.x, worldPos.y);

        if (Vector2.Distance(v2p, v2m) > buildDistance)
            return Vector3.zero;

        Vector3 raycastDirection = -Vector3.forward;

        RaycastHit2D hit = Physics2D.Raycast(worldPos, raycastDirection, Mathf.Infinity, ~ignoreLayer); //8 = trigger area
        Debug.Log($"hit.collider = {hit.collider}, {Time.time}");
        if (hit.collider == null)
        {
            float roundedX = Mathf.Round(worldPos.x / 1) * 1;
            float roundedY = Mathf.Round(worldPos.y / 1) * 1;

            return new Vector3(roundedX, roundedY, 0f);
        }
        return Vector3.zero;
    }

}
