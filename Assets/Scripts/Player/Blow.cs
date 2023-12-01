using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

<<<<<<< HEAD
public class Blow : MonoBehaviourPun
=======
public class Blow : MonoBehaviour
>>>>>>> dev
{
    [SerializeField] private int damage = 100;
    private Animator animator;
    private bool canBlow = true;
    private Vector2 raycastDirection;

    private void Start()
    {
        animator = transform.GetComponent<Animator>();
    }
    void Update()
<<<<<<< HEAD
    {
        if (photonView.IsMine)
        {
            if (Input.GetMouseButtonDown(0) && canBlow)
            {
                canBlow = false;
                Invoke("SetCanBlowTrue", 0.8f);
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                raycastDirection = mousePosition - (Vector2)transform.position;

                 animator.Play("Blow");
            }
=======
    {      
        if (Input.GetMouseButtonDown(0) && canBlow)
        {
            canBlow = false;
            Invoke("SetCanBlowTrue", 0.8f);
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            raycastDirection = mousePosition - (Vector2)transform.position;

            animator.Play("Blow");
>>>>>>> dev
        }
    }
    private void StartCircleCast()
    {
       Collider2D[] hits = Physics2D.OverlapCircleAll(transform.GetChild(0).position, 0.6f);

        if (hits[0] != null)
        {
            Attack(hits);
        }
    }
    private void Attack(Collider2D[] hits)
    {
        foreach (Collider2D hit in hits)
        {
            Transform target = hit.transform;
            PhotonView targetPhotonView = target.GetComponent<PhotonView>();

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
                    targetPhotonView.RPC("TakeDamage", RpcTarget.AllBuffered, damage);
                    Debug.Log("Dealing damage to target.");
                }
            }
        }
    }
    private void SetCanBlowTrue()
    {
        canBlow = true;
    }
}
