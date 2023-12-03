using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightShadowCaster : MonoBehaviour
{
    private Light2D globalLight;

    private void Start()
    {
        // Находим все объекты с тегом "GlobalLight" и выбираем первый из них (если они есть)
        GameObject[] lights = GameObject.FindGameObjectsWithTag("GlobalLight");

        if (lights.Length > 0)
        {
            globalLight = lights[0].GetComponent<Light2D>();
        }

        if (globalLight == null)
        {
            Debug.LogError("Global Light2D not found in the scene!");
        }
    }

    private void Update()
    {
        // Проверяем существование globalLight перед использованием
        if (globalLight != null)
        {
            if (globalLight.intensity > 0.5f) // Если яркость больше половины, то день
            {
                // Отключаем Shadow Caster 2D объекта
                gameObject.GetComponent<ShadowCaster2D>().enabled = false;
            }
            else
            {
                // Включаем Shadow Caster 2D объекта
                gameObject.GetComponent<ShadowCaster2D>().enabled = true;
            }
        }
    }
}
