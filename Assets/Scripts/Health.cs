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
    private bool isPlayer; // ���������� ��� ��������, �������� �� ������ �������

    private void Start()
    {
        currentHealth = maxHealth;

        isPlayer = GetComponent<PlayerMovement>() != null; // ���������, ���� �� �� ������� ��������� PlayerMovement (��� ����� ���� ���������, ������� ���� ������ � ������)
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
            if(gameObject.tag == "Player")
                Debug.Log($"currentPlayerHealth = {currentHealth}");

            if (isPlayer)
            {
                UpdateHealthBar();
            }

            if (currentHealth <= 0)
            {
                PhotonNetwork.Destroy(gameObject);
            }

            if (currentHealth > maxHealth) currentHealth = maxHealth;
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
