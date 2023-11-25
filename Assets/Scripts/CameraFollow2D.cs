using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform player; // ѕоле player, за которым будет следить камера

    [SerializeField] float movingSpeed = 5f;

    public float MovingSpeed { get => movingSpeed; set => movingSpeed = value; }

    void Update()
    {
        CameraFollow();
    }

    private void CameraFollow()
    {
        if (player)
        {
            Vector3 pos = Vector3.Lerp(transform.position, new Vector3(player.position.x, player.position.y, -10), MovingSpeed * Time.deltaTime);
            transform.position = pos;
        }
    }
}