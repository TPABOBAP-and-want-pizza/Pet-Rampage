using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviourPunCallbacks
{
    private Inventory playerInventory; // Ссылка на инвентарь игрока
    [SerializeField] private GameObject buildingPrefab;

    private void Start()
    {
        // Проверяем, что текущий объект BuildingSystem принадлежит локальному игроку
        if (photonView.IsMine)
        {
            // Находим текущего игрока
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

            // Получаем компонент Player на объекте игрока
            Player localPlayer = playerObj.GetComponent<Player>();

            // Если игрок найден, получаем его инвентарь
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
