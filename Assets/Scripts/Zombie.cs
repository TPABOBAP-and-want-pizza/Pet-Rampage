using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Zombie : MonoBehaviourPun, ISloweable, IPursue
{
    private Transform pursuedTransform;
    [SerializeField] float pursuedTime = 8f;
    [SerializeField] int damage = 10;
    [SerializeField] float attackDelay = 2f;
    private bool canAttack = true;
    [SerializeField] float maxSpeed = 2f;
    [SerializeField] float slowdown = 2f;
    private float currentSpeed;
    public Transform PursueTransform { get { return pursuedTransform; } set { pursuedTransform = value; } }

    public bool isInPursueZone { get; set; } = false;

    void Start()
    {
        currentSpeed = maxSpeed;
        //pursuedTransform = GameObject.FindGameObjectWithTag("Player").transform;
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
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (canAttack)
        {
            if (collision.gameObject.GetComponent<IDamageTaker>() != null)
            {
                Debug.Log("Attack");
                Invoke("NormaliseSpeed", 1f);
                Invoke("SetCanAttackTrue", attackDelay);
                canAttack = false;
                currentSpeed = 0f;
                collision.gameObject.GetComponent<IDamageTaker>().TakeDamage(damage);
            }
        }
    }
    public void StartTimer()
    {
        Invoke("Calm", pursuedTime);
    }
    private void Calm()
    {
        if (!isInPursueZone)
            pursuedTransform = null;
    }
    [PunRPC]
    public void SlowDown()
    {
        currentSpeed /= slowdown;
        Invoke("NormaliseSpeed", 0.5f);
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
