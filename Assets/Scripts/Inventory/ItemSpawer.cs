using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemSpawer : MonoBehaviour
{
    [SerializeField] private List<GameObject> items = new List<GameObject>();
    [SerializeField] private List<float> chances = new List<float>();
    //   private bool spawned = false;

    private void OnDestroy()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for(int i = 0; i < items.Count; i++)
            {
                Debug.Log("Transform Position: " + transform.position);
                if (Random.Range(1, 1001) <= chances?[i]*10)
                {
                    PhotonNetwork.Instantiate($"Items/{items[i].name}", transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0.00f), Quaternion.identity, 0);
                }
            }
        }
    }
}
