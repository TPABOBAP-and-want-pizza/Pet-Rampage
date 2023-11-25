using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHitHandler, IDamageTaker
{
    [SerializeField] int maxHealth = 1000;
    private int currentHealth;
    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void Handlehit(HitInfo info)
    {
        throw new System.NotImplementedException();
    }

    void IDamageTaker.TakeDamage(int damage)
    {

        currentHealth -= damage;
        Debug.Log($"currentPlayerHealth = {currentHealth}");
    }
}
