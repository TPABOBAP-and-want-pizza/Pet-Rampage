using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Apple : MonoBehaviourPun
{
    [SerializeField] private int healing = 10;
    [SerializeField] private float requiredHoldTime = 1f;
    [SerializeField] private int countToRemove = 1;
    [SerializeField] private Sound sound;
    [SerializeField] AudioClip clip;
    private bool holding = false;
    private float clickTime = 0f;

    void Update()
    {
        if (!photonView.IsMine)
            return;

        if (Input.GetMouseButton(1))
        {
            if (!holding)
            {
                Debug.Log("StartHolding");
                holding = true;
                clickTime = Time.time;
            }
            if(!sound.IsPlaying)
                sound.PlayRandomSound();
            //висвітлення партиклів
            
            if (requiredHoldTime <= Time.time - clickTime)
            {
                sound.PlaySound(clip);
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
        transform?.parent?.GetComponent<Player>().RemoveSelectedItem(countToRemove);
        PhotonView.Destroy(gameObject);
    }
    private void OnDestroy()
    {
        PhotonView.Destroy(gameObject);
    }
}
