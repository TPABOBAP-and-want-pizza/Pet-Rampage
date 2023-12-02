using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviourPunCallbacks
{
    private Inventory playerInventory; // ������ �� ��������� ������
    [SerializeField] private GameObject buildingPrefab_item;
    [SerializeField] private GameObject buildingPrefab_block;
    [SerializeField] private int resursesCount = 4;

    private void Start()
    {
        // ���������, ��� ������� ������ BuildingSystem ����������� ���������� ������
        if (photonView.IsMine)
        {
            // ������� �������� ������
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

            // �������� ��������� Player �� ������� ������
            Player localPlayer = playerObj.GetComponent<Player>();

            // ���� ����� ������, �������� ��� ���������
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
        // ��������� ������� ����� � ��������� ������
        if (playerInventory.HasItem(buildingPrefab_item.GetComponent<PickableItem>().Item, resursesCount))
        {
            Vector3 newPosition = GetMousePositionInWorld();
            newPosition.z = 0f;
            PhotonNetwork.Instantiate($"Items/{buildingPrefab_block.name}", newPosition, Quaternion.identity);
            playerInventory.RemoveItem(buildingPrefab_item.GetComponent<PickableItem>().Item, resursesCount);
        }
    }

    private Vector3 GetMousePositionInWorld()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.transform.position.z;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        // ��������� ���������� �� ��������� ��������, ������� 10
        float roundedX = Mathf.Round(worldPos.x / 1) * 1;
        float roundedY = Mathf.Round(worldPos.y / 1) * 1;

        // ���������� ����������� ����������
        return new Vector3(roundedX, roundedY, 0f);
    }

}
