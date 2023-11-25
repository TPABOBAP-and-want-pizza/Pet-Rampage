using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageTaker
{
    [SerializeField] int maxHealth = 1000;
    private int currentHealth;
    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"currentPlayerHealth = {currentHealth}");
    }
}
