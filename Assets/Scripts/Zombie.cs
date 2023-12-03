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
    private bool night = false;
    private bool vertolot = false;
    [SerializeField] Transform vecVertorlot;
    [Header("Zombie Animation Settings")]
    public Animator zombieAnimator;
    [SerializeField] SpriteRenderer zombieSpriteRenderer;

    void Start()
    {
        currentSpeed = maxSpeed;
        //pursuedTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (zombieAnimator == null)
        {
            zombieAnimator = GetComponent<Animator>();
            if (zombieAnimator == null)
            {
                Debug.LogError("Animator component not found!");
            }
        }
        if (zombieSpriteRenderer == null)
        {
            zombieSpriteRenderer = GetComponentInChildren<SpriteRenderer>(); // ����� ������������� �������� ������ ������
            if (zombieSpriteRenderer == null)
            {
                Debug.LogError("SpriteRenderer component not found!");
            }
        }
    }

    void Update()
    {
        if(GameManager.IsNight && !night)
        {
            night = true;
            vertolot = Random.Range(1, 3) == 1;
            currentSpeed = nightSpeed;
        }
        else if(!GameManager.IsNight && night)
        {
            night = false;
            currentSpeed = maxSpeed;
        }


        if (pursuedTransform != null)
        {
            Following();
            zombieAnimator.SetBool("Is_walk", true); // ������������� ���������� ��� �������� ������

            if (canAttack)
            {
                zombieAnimator.SetBool("Is_attack", false); // ���������� ���������� ��� �������� �����
                float rotationZ = transform.eulerAngles.z;
                zombieSpriteRenderer.flipY = (rotationZ >= -270 && rotationZ <= -90) || (rotationZ >= 90 && rotationZ <= 270);
            }
        }
        else
        {
            // ��� ���� ��� �������������, ������ ����� ����� �� �����
            zombieAnimator.SetBool("Is_walk", false); // ���������� ���������� ��� �������� ������

            if (canAttack)
            {
                zombieAnimator.SetBool("Is_attack", false); // ���������� ���������� ��� �������� �����
            }
        }
    }

    private void Following()
    {
        Vector3 direction = pursuedTransform.position - transform.position;
        if (GameManager.IsNight && vertolot)
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
                canAttack = false;
                if(!GameManager.IsNight)
                    currentSpeed = maxSpeed;

                collision.gameObject.GetComponent<IDamageTaker>().TakeDamage(damage);
                StartCoroutine(AttackCooldown());
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
        if (!GameManager.IsNight)
            currentSpeed = maxSpeed;
        else currentSpeed = nightSpeed;
    }
    private void SetCanAttackTrue()
    {
        canAttack = true;

    }
    private IEnumerator AttackCooldown()
    {
        zombieAnimator.SetBool("Is_attack", true);
        yield return new WaitForSeconds(0.5f); // ������ �� 2 �������
        zombieAnimator.SetBool("Is_attack", false);
        canAttack = true;
    }
}
