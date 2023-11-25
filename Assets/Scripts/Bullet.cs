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
            // Локальная инициализация объекта пули, только на клиенте, который выпустил пулю
            Vector2 shootDirection = (Vector2)photonView.InstantiationData[0];
            GetComponent<Rigidbody2D>().velocity = shootDirection.normalized * -1 * bulletSpeed;
        }
    }

    [PunRPC]
    public void SetBulletProperties(int damageValue, Vector2 shootDirection)
    {
        Damage = damageValue;
        bulletSpeed = 10f; // Установка скорости пули на сервере
        GetComponent<Rigidbody2D>().velocity = shootDirection.normalized * -1 * bulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (photonView.IsMine)
        {
            if (collision.gameObject.GetComponent<IHitHandler>() != null)
            {
                if (collision.gameObject.GetComponent<IDamageTaker>() != null)
                {
                    collision.gameObject.GetComponent<IDamageTaker>().TakeDamage(Damage);
                }
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}
