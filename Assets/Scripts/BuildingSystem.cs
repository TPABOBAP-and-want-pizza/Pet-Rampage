using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    [SerializeField] private Inventory playerInventory; // Ссылка на инвентарь игрока
    [SerializeField] private GameObject buildingPrefab; // Префаб блока для строительства

    private void Start()
    {
        playerInventory = FindObjectOfType<Player>().inventory;
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
        // Проверяем наличие блока в инвентаре игрока
        if (playerInventory.HasItem(buildingPrefab.GetComponent<PickableItem>().Item))
        {
            Vector3 newPosition = GetMousePositionInWorld();
            newPosition.z = 0f;
            PhotonNetwork.Instantiate(buildingPrefab.name, newPosition, Quaternion.identity);
            playerInventory.RemoveItem(buildingPrefab.GetComponent<PickableItem>().Item);

        }
    }

    private Vector3 GetMousePositionInWorld()
    {
        // Получаем позицию мыши в мировых координатах
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
