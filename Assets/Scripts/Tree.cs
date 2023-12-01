using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    const float Transparency = 0.5f;
    private SpriteRenderer renderer;

    private void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (renderer.material.HasProperty("_Color"))
            {
                Color currentColor = renderer.material.color;
                currentColor.a = Transparency; 

                renderer.material.color = currentColor;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (renderer.material.HasProperty("_Color"))
            {
                Color currentColor = renderer.material.color;
                currentColor.a = 1;

                renderer.material.color = currentColor;
            }
        }
    }
}
