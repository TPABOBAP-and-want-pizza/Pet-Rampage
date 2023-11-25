using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, IDamageTaker
{
    [SerializeField] int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"currentHealth = {currentHealth}");
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
