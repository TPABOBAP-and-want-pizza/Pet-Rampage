using UnityEngine;

public class PositionBinder : MonoBehaviour
{
    public Transform targetObject; // Объект, позиция которого мы хотим привязать

    void Update()
    {
        if (targetObject != null)
        {
            // Установка позиции текущего объекта равной позиции целевого объекта
            transform.position = targetObject.position;
        }
        else
        {
            Debug.LogWarning("Target object is not assigned to the PositionBinder script.");
        }
    }
}
