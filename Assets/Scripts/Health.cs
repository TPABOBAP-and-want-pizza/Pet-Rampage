using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Health : MonoBehaviourPun, IDamageTaker
{
    [SerializeField] int maxHealth = 100;
    private int currentHealth;
    private Image healthBar;
    private bool isPlayer; // Переменная для проверки, является ли объект игроком

    private void Start()
    {
        currentHealth = maxHealth;

        isPlayer = GetComponent<PlayerMovement>() != null; // Проверяем, есть ли на объекте компонент PlayerMovement (это может быть компонент, который есть только у игрока)
        if (isPlayer && photonView.IsMine)
        {
            if (healthBar == null)
            {
                healthBar = GameObject.Find("Bar")?.GetComponent<Image>();
                if (healthBar == null)
                {
                    Debug.LogError("HealthBar image is missing!");
                }
                else
                {
                    UpdateHealthBar();
                }
            }
        }
    }

    [PunRPC]
    public void TakeDamage(int damage)
    {
        if (photonView.IsMine)
        {
            currentHealth -= damage;
            Debug.Log($"currentPlayerHealth = {currentHealth}");

            if (isPlayer)
            {
                UpdateHealthBar();
            }

            if (currentHealth <= 0)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    private void UpdateHealthBar()
    {
        if (isPlayer && healthBar != null && photonView.IsMine)
        {
            float healthRatio = (float)currentHealth / maxHealth;
            healthBar.fillAmount = healthRatio;
        }
    }
}
