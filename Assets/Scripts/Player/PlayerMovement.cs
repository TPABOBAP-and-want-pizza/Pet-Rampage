using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    void Update()
    {
        Move();
        RotateTowardsMouse();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 1, 0) * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -1, 0) * speed * Time.deltaTime;
        }
    }
    private void RotateTowardsMouse()
    {
        Vector3 mousePositionScreen = Input.mousePosition;

        mousePositionScreen.z = 10f; 

        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);

        Vector3 directionToMouse = mousePositionWorld - transform.position;
        directionToMouse.z = 0f; 

        // Поворот гравця в напрямку миші
        if (directionToMouse != Vector3.zero)
        {
            transform.up = directionToMouse.normalized;
        }
    }
}
