using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Helipad : MonoBehaviour
{
    [SerializeField] private List<ItemInfo> infos = new List<ItemInfo>();
    [SerializeField] private int countStackableItems = 3;
    [SerializeField] private int countItems = 3;
    private int currentDay = 0;
    
    private void Update()
    {
        if (currentDay != GameManager.Day)
        {
            Debug.Log("spawn items");
            currentDay = GameManager.Day;
            DropItems();
        }
    }
    private void DropItems()
    {
        for (int j = 0; j < countItems; j++)
        {
            int index = Random.Range(0, infos.Count);
            if (infos[index].stackable && Random.Range(0,5) == 0)
            {
                for (int i = 0; i < countStackableItems; i++)
                {
                    if (infos[index].name == "Bullet")
                        for (int k = 0; k < 2; k++)
                        {
                            PhotonNetwork.Instantiate($"Items/{infos[index].name}Item",
                                transform.position + new Vector3(Random.Range(-0.5f, 0.5f), -2f, 0.00f), Quaternion.identity, 0);
                        }
                    else PhotonNetwork.Instantiate($"Items/{infos[index].name}Item",
                                transform.position + new Vector3(Random.Range(-0.5f, 0.5f), -2f, 0.00f), Quaternion.identity, 0);
                }
            }
            else
            {
                PhotonNetwork.Instantiate($"Items/{infos[index].name}Item",
                       transform.position + new Vector3(Random.Range(-0.5f, 0.5f), -2f, 0.00f), Quaternion.identity, 0);
            }
        }
    }
    public void OnDestroy()
    {
        //гейм овер
        //перех≥д на нову се÷ену
    }
}
