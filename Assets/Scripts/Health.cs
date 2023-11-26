using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Health : MonoBehaviourPun, IDamageTaker
{
    [SerializeField] int maxHealth = 100;
    private int currentHealth;
    private void Start()
    {
        currentHealth = maxHealth;
    }
    [PunRPC]
    public void TakeDamage(int damage)
    {
        if (photonView.IsMine)
        {
            currentHealth -= damage;
            //Debug.Log($"currentPlayerHealth = {currentHealth}");
            if (currentHealth <= 0)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }
}