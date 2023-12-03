using UnityEngine;

public class Helicopter : MonoBehaviour
{
    public Vector3 targetDayPosition; // ������ ��������� ��� ���
    public Vector3 targetNightPosition; // ������ ��������� ��� ����
    public Vector3 nightTeleportPosition; // ���������� ��� ������������ � ������ ����
    public float speed = 5.0f;

    [SerializeField] private GlobalTimer globalTimer; // ������ �� ������ GlobalTimer ����� ���������

    private bool hasTeleportedAtNight = false; // ���� ��� ������������ ������������ � ������ ����
    private bool lastDayState = true; // ���������� ��� �������� ���������� ��������� ���

    void Update()
    {
        if (globalTimer != null)
        {
            bool isDay = globalTimer.IsDayTime;

            // ���� ��������� ��������� ���� ����, � ������ ��������� ����, �� ������������� ���� ������������
            if (lastDayState && !isDay)
            {
                hasTeleportedAtNight = false;
            }

            // ��������� ������� ��������� ��� ���������� �����
            lastDayState = isDay;

            // ���� ����, ����� � ���� ��� ���, ����� � ���� ��� ����
            Vector3 target = isDay ? targetDayPosition : targetNightPosition;

            float step = speed * Time.deltaTime;

            if (!isDay)
            {
                // ���� ��������� ���� � �������� ��� �� ��� ��������, ��������������� ��������
                if (!hasTeleportedAtNight)
                {
                    transform.position = nightTeleportPosition;
                    hasTeleportedAtNight = true; // ������������� ���� ������������ � ������ ����
                }
                else
                {
                    // ����� ��������� � ���� ��� ����
                    transform.position = Vector3.MoveTowards(transform.position, target, step*3);
                }
            }
            else
            {
                // �������� � ���� ��� ���
                transform.position = Vector3.MoveTowards(transform.position, target, step*3);
            }
        }
    }
}
