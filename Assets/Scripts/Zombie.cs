using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour, IDamageTaker
{
    [SerializeField] int damage = 10;
    [SerializeField] float attackDelay = 2f;
    private bool canAttack = true;
    [SerializeField] float maxSpeed = 2f;
    [SerializeField] float slowdown = 2f;
    private float currentSpeed;
    [SerializeField] int maxHealth = 100;
    private Transform pursuedTransform;
    private int currentHealth;
    void Start()
    {
        currentSpeed = maxSpeed;
        currentHealth = maxHealth;
        pursuedTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (pursuedTransform != null)
        {
            Following();
        }
    }

    private void Following()
    {
        Vector3 direction = pursuedTransform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PLayer")
            pursuedTransform = collision.transform;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PLayer")
            Invoke("Calm", 3f);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (canAttack)
        {
            Debug.Log("Attack");
            if (collision.gameObject.GetComponent<IDamageTaker>() != null)
            {
                Invoke("NormaliseSpeed", 1f);
                Invoke("SetCanAttackTrue", attackDelay);
                canAttack = false;
                currentSpeed /= slowdown;
                collision.gameObject.GetComponent<IDamageTaker>().TakeDamage(damage);
            }
        }
    }

    private void Calm()
    {
        pursuedTransform = null;
    }

    public void TakeDamage(int damage)
    {
        currentSpeed /= slowdown;
        Invoke("NormaliseSpeed", 0.5f);
        currentHealth -= damage;
        if (currentHealth <= 0)
            Destroy(gameObject);
    }
    private void NormaliseSpeed()
    {
        currentSpeed = maxSpeed;
    }
    private void SetCanAttackTrue()
    {
        canAttack = true;
    }
}
