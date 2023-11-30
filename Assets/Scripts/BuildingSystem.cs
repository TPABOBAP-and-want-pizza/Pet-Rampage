using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviourPunCallbacks
{
    private Inventory playerInventory; // ������ �� ��������� ������
    [SerializeField] private GameObject buildingPrefab;

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
        if (playerInventory.HasItem(buildingPrefab.GetComponent<PickableItem>().Item))
        {
            Vector3 newPosition = GetMousePositionInWorld();
            newPosition.z = 0f;
            PhotonNetwork.Instantiate($"Items/{buildingPrefab.name}", newPosition, Quaternion.identity);
            playerInventory.RemoveItem(buildingPrefab.GetComponent<PickableItem>().Item);
        }
    }

    private Vector3 GetMousePositionInWorld()
    {
        // �������� ������� ���� � ������� �����������
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
