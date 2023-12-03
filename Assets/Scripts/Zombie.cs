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
    [SerializeField] float nightSpeed = 4f;
    [SerializeField] float slowdown = 2f;
    private float currentSpeed;
    public Transform PursueTransform { get { return pursuedTransform; } set { pursuedTransform = value; } }

    public bool isInPursueZone { get; set; } = false;
    public bool isNight { get; set; } = false;
    private bool night = false;
    private bool vertolot = false;
    [SerializeField] Transform vecVertorlot;

    void Start()
    {
        currentSpeed = maxSpeed;
        //pursuedTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(isNight && !night)
        {
            night = true;
            vertolot = Random.Range(1, 3) == 1;
            currentSpeed = nightSpeed;
        }
        else if(!isNight && night)
        {
            night = false;
            currentSpeed = maxSpeed;
        }

        if (pursuedTransform != null)
        {
            Following();
        }
    }

    private void Following()
    {
        Vector3 direction = pursuedTransform.position - transform.position;
        if (isNight && vertolot)
        {
            direction = vecVertorlot.position - transform.position; 
        }
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (canAttack)
        {
            GameObject temp = collision.gameObject;
            if (temp.GetComponent<IDamageTaker>() != null && (temp.tag == "Player" || temp.layer == 14)) //14 = block
            {
                Debug.Log("Attack");
                Invoke("NormaliseSpeed", 1f);
                Invoke("SetCanAttackTrue", attackDelay);
                canAttack = false;
                if(!isNight)
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
        if (!isNight)
            currentSpeed = maxSpeed;
        else currentSpeed = nightSpeed;
    }
    private void SetCanAttackTrue()
    {
        canAttack = true;
    }
}
