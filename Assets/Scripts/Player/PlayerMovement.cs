using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks, ISloweable
{
    PhotonView view;
    [SerializeField] float maxSpeed = 2f;
    [SerializeField] float slowdown = 1.5f;
    private float currentSpeed;

    [Header("Player Animation Settings")]
    public Animator animator;

    [Header("Sprite")]
    public GameObject spriteObject;
    private SpriteRenderer spriteRenderer;

    private bool isMoving = false;

<<<<<<< HEAD
    [SerializeField] private Transform selectedTransform;
=======
    private Transform selectedTransform;
>>>>>>> dev


    private void Start()
    {
        spriteRenderer = spriteObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found in the child object!");
        }

        currentSpeed = maxSpeed;
        view = GetComponent<PhotonView>();
        if (view.Owner.IsLocal)
        {
            CameraFollow2D cameraFollow = Camera.main.GetComponent<CameraFollow2D>();
            if (cameraFollow != null)
            {
                cameraFollow.player = gameObject.transform; // Устанавливаем игрока как объект, за которым следит камера
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
            animator.SetFloat("Move", isMoving ? 1 : 0); // Если игрок движется, устанавливаем значение 1, иначе 0
            
        }
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.D))
        {
            spriteRenderer.flipX = false;
            transform.position += new Vector3(1, 0, 0) * currentSpeed * Time.deltaTime;
            photonView.RPC("FlipSprite", RpcTarget.All, false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            spriteRenderer.flipX = true;
            transform.position += new Vector3(-1, 0, 0) * currentSpeed * Time.deltaTime;
            photonView.RPC("FlipSprite", RpcTarget.All, true);
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

    [PunRPC]
    private void FlipSprite(bool flip)
    {
        spriteRenderer.flipX = flip;
    }

    private void RotateTowardsMouse()
    {
        if (view.IsMine && selectedTransform != null)
        {
            Vector3 mousePositionScreen = Input.mousePosition;
            mousePositionScreen.z = 10f;

            Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(mousePositionScreen);

            Vector3 directionToMouse = mousePositionWorld - transform.position;
            directionToMouse.z = 0f;

            // Поворот гравця в напрямку миші
            if (directionToMouse != Vector3.zero)
            {
                float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
                selectedTransform.rotation = Quaternion.Euler(0f, 0f, angle + 180);
            }
        }
    }

    [PunRPC]
    public void SlowDown()
    {
        if (view != null && view.IsMine)
        {
            // Если цель - сам игрок, игнорируем замедление
            return;
        }

        currentSpeed /= slowdown;
        Invoke("NormaliseSpeed", 0.3f);
    }

    public void SetSelectedTransform(Transform transform)
    {
        selectedTransform = transform;
    }
    private void NormaliseSpeed()
    {
        currentSpeed = maxSpeed;
    }
}
