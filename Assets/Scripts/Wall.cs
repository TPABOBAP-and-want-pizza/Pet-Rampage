using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Wall : MonoBehaviour, IDamageTaker
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
        currentHealth -= damage;
        Debug.Log($"currentHealth = {currentHealth}");
        if (currentHealth <= 0)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
