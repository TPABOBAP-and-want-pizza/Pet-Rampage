using UnityEngine;

public class PositionBinder : MonoBehaviour
{
    public Transform targetObject; // ������, ������� �������� �� ����� ���������

    void Update()
    {
        if (targetObject != null)
        {
            // ��������� ������� �������� ������� ������ ������� �������� �������
            transform.position = targetObject.position;
        }
        else
        {
            Debug.LogWarning("Target object is not assigned to the PositionBinder script.");
        }
    }
}
