using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Respawn : MonoBehaviourPunCallbacks
{
    [SerializeField] float cooldown = 10f;
    [SerializeField] RespawnTimer timer;

    private PhotonView _photonView;

    private void Start()
    {
        // Получение ссылки на photonView после респауна
        _photonView = GetComponent<PhotonView>();

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
        if (_photonView.IsMine && !GameManager.IsGameOver)
        {
            timer.transform.parent.gameObject.SetActive(true);
            timer.StartTimer(cooldown);
        }
    }
}
