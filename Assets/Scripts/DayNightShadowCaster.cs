using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightShadowCaster : MonoBehaviour
{
    private Light2D globalLight;

    private void Start()
    {
        // ������� ��� ������� � ����� "GlobalLight" � �������� ������ �� ��� (���� ��� ����)
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
        // ��������� ������������� globalLight ����� ��������������
        if (globalLight != null)
        {
            if (globalLight.intensity > 0.5f) // ���� ������� ������ ��������, �� ����
            {
                // ��������� Shadow Caster 2D �������
                gameObject.GetComponent<ShadowCaster2D>().enabled = false;
            }
            else
            {
                // �������� Shadow Caster 2D �������
                gameObject.GetComponent<ShadowCaster2D>().enabled = true;
            }
        }
    }
}
