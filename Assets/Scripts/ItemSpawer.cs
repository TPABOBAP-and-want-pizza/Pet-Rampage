using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemSpawer : MonoBehaviour
{
    [SerializeField] private List<GameObject> items = new List<GameObject>();

    private void OnDestroy()
    {
        foreach(GameObject item in items)
        {
            PhotonNetwork.Instantiate(item.name, transform.position, Quaternion.identity, 0);
        }
    }
}
