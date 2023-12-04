using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Health : MonoBehaviourPun, IDamageTaker
{
    [SerializeField] Sound sound;
    [SerializeField] int maxHealth = 100;
    private int currentHealth;
    private Image healthBar;
    private bool isPlayer; // Переменная для проверки, является ли объект игроком

    private SpriteRenderer[] spriteRenderers;
    private Color originalColor = Color.white;

    private void Start()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        originalColor = spriteRenderers[0].color; // Предполагаем, что основной цвет спрайта находится в первом элементе массива

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
            if (gameObject.tag == "Player")
                Debug.Log($"currentPlayerHealth = {currentHealth}");

            if (isPlayer)
            {
                UpdateHealthBar();
            }
            StartCoroutine(FlashDamageEffect(damage>0));

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

    private IEnumerator FlashDamageEffect(bool red)
    {
        foreach (var renderer in spriteRenderers)
        {
            renderer.color = red ? Color.red : Color.green;

            if (red && isPlayer && sound != null) 
                sound.PlayRandomSound();

            yield return new WaitForSeconds(0.2f);

            renderer.color = originalColor; 
        }

        photonView.RPC("RestoreOriginalColor", RpcTarget.Others);
    }
    [PunRPC]
    private void RestoreOriginalColor()
    {
        Debug.Log("Restoring original color on other players...");

        foreach (var renderer in spriteRenderers)
        {
            renderer.color = originalColor; 
        }
    }
}
