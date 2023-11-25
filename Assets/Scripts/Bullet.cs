using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage { get; set; }
    private void Start()
    {
        Invoke("ToDestroy", 1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IHitHandler>() != null)
        {
            if(collision.gameObject.GetComponent<IDamageTaker>() != null)
            {
                collision.gameObject.GetComponent<IDamageTaker>().TakeDamage(Damage);
            }
            Destroy(gameObject);
        }
    }
    private void ToDestroy()
    {
        Destroy(gameObject);
    }
}
