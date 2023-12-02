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
            StartCoroutine(FlashDamageEffect());

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

    private IEnumerator FlashDamageEffect()
    {
        foreach (var renderer in spriteRenderers)
        {
            renderer.color = Color.red; // Устанавливаем цвет красным

            yield return new WaitForSeconds(0.2f); // Ждем 0.2 секунды

            renderer.color = originalColor; // Возвращаем исходный цвет
        }

        photonView.RPC("RestoreOriginalColor", RpcTarget.Others); // Вызываем RPC для восстановления цвета у других игроков
    }
    [PunRPC]
    private void RestoreOriginalColor()
    {
        Debug.Log("Restoring original color on other players...");

        foreach (var renderer in spriteRenderers)
        {
            renderer.color = originalColor; // Возвращаем исходный цвет у других игроков
        }
    }
}
