using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Respawn : MonoBehaviourPun
{
    [SerializeField] float cooldown = 10f;
    [SerializeField] RespawnTimer timer;
    private void Start()
    {
        GameObject timerObject = GameObject.FindGameObjectWithTag("Timer");

        if (timerObject != null)
        {
            timer = timerObject.GetComponent<RespawnTimer>();
            timer.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Timer object not found!");
        }
    }
    private void OnDestroy()
    {
        if (photonView.IsMine)
        {
            timer.transform.parent.gameObject.SetActive(true);
            timer.StartTimer(cooldown);
        }
    }
}
