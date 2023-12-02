using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Apple : MonoBehaviourPun
{
    [SerializeField] private int healing = 10;
    [SerializeField] private float requiredHoldTime = 1f;
    private bool holding = false;
    private float clickTime = 0f;

    void Update()
    {
        //if (photonView.IsMine)
        //    return;

        if (Input.GetMouseButton(1))
        {
            if (!holding)
            {
                Debug.Log("StartHolding");
                holding = true;
                clickTime = Time.time;
            }

            //висвітлення партиклів
            //програвання звуку їждення
            if (requiredHoldTime <= Time.time - clickTime)
            {
                Healing();
                holding = false;
            }
        }
        else holding = false;
    }
    private void Healing()
    {
        PhotonView photonView = transform?.parent.GetComponent<PhotonView>();
        if (transform.parent.GetComponent<IDamageTaker>() != null)
        {
            if (photonView != null)
            {
                photonView.RPC("TakeDamage", RpcTarget.AllBuffered, -healing);
            }
        }
        transform?.parent?.GetComponent<Player>().RemoveSelectedItem();
        PhotonView.Destroy(gameObject);
    }
}
