using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPunCallbacks
{
    public int Damage { get; set; }
    public float bulletSpeed = 10f;
    public float destroyTime = 2f; // ����� �� ����������� ����

    void Start()
    {
        StartCoroutine(SetVelocity());
        Invoke("DestroyBullet", destroyTime); // ����� ����������� ���� ����� ����� destroyTime
    }

    IEnumerator SetVelocity()
    {
        yield return new WaitForFixedUpdate(); // ���� ���������� FixedUpdate ��� ����������� ������� ������

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = (mousePosition - (Vector2)transform.position).normalized;
        Debug.Log("Shoot Direction: " + shootDirection); // ����� �������� � ���
        GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IHitHandler>() != null)
        {
            if (collision.gameObject.GetComponent<IDamageTaker>() != null)
            {
                collision.gameObject.GetComponent<IDamageTaker>().TakeDamage(Damage);
            }
            photonView.RPC("DestroyBullet", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
