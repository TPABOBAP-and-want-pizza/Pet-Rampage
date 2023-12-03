using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursuedArea : MonoBehaviour
{
    [SerializeField] private CircleCollider2D pursuedAreaCollider;
    [SerializeField] private float multiplier = 3f;
    private bool night = false;
    private void Update()
    {
        if (GameManager.IsNight && !night)
        {
            night = true;
            pursuedAreaCollider.radius *= multiplier;
        }
        else if (!GameManager.IsNight && night)
        {
            night = false;
            pursuedAreaCollider.radius /= multiplier;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject temp = collision.gameObject;
        if (temp.layer == 9) //9 = enemy
        {
            IPursue pursue = temp.GetComponent<IPursue>();
            if (pursue != null)
            {
                pursue.PursueTransform = transform.parent;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject temp = collision.gameObject;
        if (temp.layer == 9) //9 = enemy
        {
            IPursue pursue = temp.GetComponent<IPursue>();
            if (pursue != null)
            {
                pursue.isInPursueZone = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject temp = collision.gameObject;
        if (temp.layer == 9) //9 = enemy
        {
            IPursue pursue = temp.GetComponent<IPursue>();
            if (pursue != null)
            {
                pursue.StartTimer();
                pursue.isInPursueZone = false;
            }
        }
    }
}
