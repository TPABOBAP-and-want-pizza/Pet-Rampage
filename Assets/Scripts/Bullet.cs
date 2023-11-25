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
            // ��������� ������������� ������� ����, ������ �� �������, ������� �������� ����
            Vector2 shootDirection = (Vector2)photonView.InstantiationData[0];
            GetComponent<Rigidbody2D>().velocity = shootDirection.normalized * -1 * bulletSpeed;
            //   public float Force { get; set; }
            //   private void Start()
            //   {
            //       Invoke("ToDestroy", 1f);
        }

    }

        [PunRPC]
        public void SetBulletProperties(int damageValue, Vector2 shootDirection)
        {
            Damage = damageValue;
            bulletSpeed = 10f; // ��������� �������� ���� �� �������
            GetComponent<Rigidbody2D>().velocity = shootDirection.normalized * -1 * bulletSpeed;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (photonView.IsMine)
            {
                if (collision.gameObject.GetComponent<IDamageTaker>() != null)
                {
                    collision.gameObject.GetComponent<IDamageTaker>().TakeDamage(Damage);
                }
                Destroy(gameObject);
            
            }
        }
    
}
