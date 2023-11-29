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
            Debug.Log("Transform Position: " + transform.position);

            PhotonNetwork.Instantiate(item.name, transform.position+new Vector3(Random.Range(-0.5f,0.5f), Random.Range(-0.5f, 0.5f),0.00f), Quaternion.identity, 0);
        }
    }
}
