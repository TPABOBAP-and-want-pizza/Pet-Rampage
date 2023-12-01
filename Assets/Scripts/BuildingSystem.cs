using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviourPunCallbacks
{
    private Inventory playerInventory; // Ссылка на инвентарь игрока
    [SerializeField] private GameObject buildingPrefab_item;
    [SerializeField] private GameObject buildingPrefab_block;

    private Dictionary<Vector3, GameObject> placedBlocks = new Dictionary<Vector3, GameObject>();

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
        if (playerInventory.HasItem(buildingPrefab_item.GetComponent<PickableItem>().Item))
        {
            Vector3 newPosition = GetMousePositionInWorld();
            newPosition.z = 0f;

            // Проверяем наличие блока в этой позиции
            if (!placedBlocks.ContainsKey(newPosition))
            {
                PhotonNetwork.Instantiate($"Items/{buildingPrefab_block.name}", newPosition, Quaternion.identity);
                playerInventory.RemoveItem(buildingPrefab_item.GetComponent<PickableItem>().Item);

                // Добавляем размещенный блок в словарь
                GameObject newBlock = GameObject.Find($"Items/{buildingPrefab_block.name}(Clone)");
                placedBlocks.Add(newPosition, newBlock);
            }
            else
            {
                Debug.Log("Блок уже установлен в этой позиции.");
                // Можно добавить сообщение об ошибке или выполнить другие действия при попытке установить блок в занятую позицию
            }
        }
    }

    private Vector3 GetMousePositionInWorld()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.transform.position.z;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        // Округляем координаты до ближайших значений, кратных 10
        float roundedX = Mathf.Round(worldPos.x / 1) * 1;
        float roundedY = Mathf.Round(worldPos.y / 1) * 1;

        // Возвращаем округленные координаты
        return new Vector3(roundedX, roundedY, 0f);
    }


}
