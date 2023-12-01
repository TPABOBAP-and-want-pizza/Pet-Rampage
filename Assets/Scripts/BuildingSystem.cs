using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviourPunCallbacks
{
    private Inventory playerInventory;
    [SerializeField] private GameObject buildingPrefab_item;
    [SerializeField] private GameObject buildingPrefab_block;

    private void Start()
    {
        if (photonView.IsMine)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
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
            BuildBlock();
        }
    }

    private void BuildBlock()
    {
        if (playerInventory.HasItem(buildingPrefab_item.GetComponent<PickableItem>().Item))
        {
            Vector3 newPosition = GetRoundedMousePositionInWorld();

            if (!IsPositionOccupied(newPosition))
            {
                PhotonNetwork.Instantiate($"Items/{buildingPrefab_block.name}", newPosition, Quaternion.identity);
                playerInventory.RemoveItem(buildingPrefab_item.GetComponent<PickableItem>().Item);

                // ���������� ���������� � ����� ����� ���� �������
                photonView.RPC("RPC_AddNewBlock", RpcTarget.All, newPosition);
            }
            else
            {
                Debug.Log("���� ��� ���������� � ���� �������.");
            }
        }
    }

    private Vector3 GetRoundedMousePositionInWorld()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.transform.position.z;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        float roundedX = Mathf.Round(worldPos.x / 1) * 1;
        float roundedY = Mathf.Round(worldPos.y / 1) * 1;

        return new Vector3(roundedX, roundedY, 0f);
    }

    private bool IsPositionOccupied(Vector3 position)
    {
        // ���������, ������ �� ����� �� ������ �������
        return PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(position.ToString());
    }

    [PunRPC]
    private void RPC_AddNewBlock(Vector3 position)
    {
        // ��������� ����� ���� � Custom Properties, ����� ��������� ������ �����, ��� ��� ������� ������
        ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
        properties.Add(position.ToString(), true);
        PhotonNetwork.CurrentRoom.SetCustomProperties(properties);
    }
}
