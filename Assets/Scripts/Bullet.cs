using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPunCallbacks
{
    public int Damage { get; set; }
    public float bulletSpeed;

    void Start()
    {
        if (photonView.IsMine)
        {
            Vector2 shootDirection = (Vector2)photonView.InstantiationData[0];
            GetComponent<Rigidbody2D>().velocity = -shootDirection.normalized * bulletSpeed;
            Invoke("ToDestroy", 1f);
        }

    }

    [PunRPC]
    public void SetBulletProperties(int damageValue, Vector2 shootDirection)
    {
        Damage = damageValue;
        bulletSpeed = 50f;
        GetComponent<Rigidbody2D>().velocity = shootDirection.normalized * -1 * bulletSpeed;
    }

    [PunRPC]
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;
        Debug.Log($"target = {target}");
        if (target.GetComponent<ISloweable>() != null)
        {
            PhotonView targetPhotonView = target.GetComponent<PhotonView>();
            if (targetPhotonView != null)
            {
                targetPhotonView.RPC("SlowDown", RpcTarget.AllBuffered);
            }
        }
        if (target.GetComponent<IDamageTaker>() != null)
        {
            PhotonView targetPhotonView = target.GetComponent<PhotonView>();
            if (targetPhotonView != null)
            {
                
                targetPhotonView.RPC("TakeDamage", RpcTarget.AllBuffered, Damage);
            }
        }
        ToDestroy();
    }
    private void ToDestroy()
    {
        PhotonNetwork.Destroy(gameObject);
    }

}
