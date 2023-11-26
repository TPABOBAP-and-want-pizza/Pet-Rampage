using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    PhotonView view;
    Rigidbody2D rb;
    [SerializeField] float speed = 1f;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        if (view.Owner.IsLocal)
        {
            rb = transform.GetComponent<Rigidbody2D>();
            CameraFollow2D cameraFollow = Camera.main.GetComponent<CameraFollow2D>();
            if (cameraFollow != null)
            {
                cameraFollow.player = gameObject.transform; // ������������� ������ ��� ������, �� ������� ������ ������
            }
        }
    }

    void Update()
    {
        if (view.IsMine)
        {
            Move();
            RotateTowardsMouse();
        }
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
        if (view.IsMine)
        {
            Vector3 mousePositionScreen = Input.mousePosition;

            mousePositionScreen.z = 10f;

            Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);

            Vector3 directionToMouse = mousePositionWorld - transform.position;
            directionToMouse.z = 0f;

            // ������� ������ � �������� ����
            if (directionToMouse != Vector3.zero)
            {
                transform.up = directionToMouse.normalized;
            }
        }
    }
}
