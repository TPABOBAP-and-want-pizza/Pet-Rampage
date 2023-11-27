using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour, ISloweable
{
    PhotonView view;
    [SerializeField] float maxSpeed = 2f;
    [SerializeField] float slowdown = 1.5f;
    private float currentSpeed;

    [Header("Player Animation Settings")]
    public Animator animator;
    

    private bool isMoving = false;


    private void Start()
    {
        currentSpeed = maxSpeed;
        view = GetComponent<PhotonView>();
        if (view.Owner.IsLocal)
        {
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

            isMoving = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D));
            animator.SetFloat("Move", isMoving ? 1 : 0); // ���� ����� ��������, ������������� �������� 1, ����� 0
            
        }
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0) * currentSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0) * currentSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 1, 0) * currentSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -1, 0) * currentSpeed * Time.deltaTime;
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
                float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
                transform.GetChild(0).rotation = Quaternion.Euler(0f, 0f, angle + 180);
            }
        }
    }




    [PunRPC]
    public void SlowDown()
    {
        currentSpeed /= slowdown;
        Invoke("NormaliseSpeed", 0.3f);
    }
    private void NormaliseSpeed()
    {
        currentSpeed = maxSpeed;
    }
}
