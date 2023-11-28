using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPunCallbacks
{
    public int Damage { get; set; }
    public float bulletSpeed;
    private int playerPhotonID;

    private Collider2D playerCollider;

    void Start()
    {
        if (photonView.IsMine)
        {
            Vector2 shootDirection = (Vector2)photonView.InstantiationData[0];
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = -shootDirection.normalized * bulletSpeed;
            Invoke("ToDestroy", 1f);

            // ������������� �������� ���� � ����������� ������
            if (playerCollider != null)
            {
                Physics2D.IgnoreCollision(playerCollider, GetComponent<Collider2D>());
            }
        }
    }

    [PunRPC]
    public void SetBulletProperties(int damageValue, Vector2 shootDirection, int playerPhotonID)
    {
        Damage = damageValue;
        bulletSpeed = 50f;
        GetComponent<Rigidbody2D>().velocity = shootDirection.normalized * -1 * bulletSpeed;

        // ��������� playerPhotonID ��� ���������� �������� ������������
        this.playerPhotonID = playerPhotonID;
    }


    [PunRPC]
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;
        PhotonView targetPhotonView = target.GetComponent<PhotonView>();

        if (targetPhotonView != null)
        {
            int targetPlayerPhotonID = targetPhotonView.ViewID;

            if (targetPlayerPhotonID == playerPhotonID)
            {
                Debug.Log("Bullet hit the same player. Ignoring damage and slow down.");
                return; // ���������� ������ RPC
            }
        }

        if (target.GetComponent<ISloweable>() != null)
        {
            if (targetPhotonView != null)
            {
                targetPhotonView.RPC("SlowDown", RpcTarget.AllBuffered);
                Debug.Log("Slowing down target.");
            }
        }
        if (target.GetComponent<IDamageTaker>() != null)
        {
            if (targetPhotonView != null)
            {
                targetPhotonView.RPC("TakeDamage", RpcTarget.AllBuffered, Damage);
                Debug.Log("Dealing damage to target.");
            }
        }
        ToDestroy();
    }
    private void ToDestroy()
    {
        PhotonNetwork.Destroy(gameObject);
    }

    public void InitializeBullet(int damageValue, Vector2 shootDirection, Collider2D playerCol)
    {
        Damage = damageValue;
        bulletSpeed = 50f;
        GetComponent<Rigidbody2D>().velocity = shootDirection.normalized * -1 * bulletSpeed;

        playerCollider = playerCol; // ��������� ������ �� ��������� ������
    }
}
