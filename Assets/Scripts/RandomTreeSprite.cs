using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTreeSprite : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer treeSpriteRenderer = transform.GetComponent<SpriteRenderer>();

        if (treeSpriteRenderer != null)
        {
            Sprite[] treeSprites = Resources.LoadAll<Sprite>("Grafic_Resources/Trees");

            if (treeSprites != null && treeSprites.Length > 0)
            {
                int randomIndex = Random.Range(0, treeSprites.Length);
                treeSpriteRenderer.sprite = treeSprites[randomIndex];
            }
            else
            {
                Debug.LogError("No tree sprites found in the Grafic_Resources/Trees folder.");
            }
        }
        else
        {
            Debug.LogError("SpriteRenderer not found on the parent object.");
        }
    }
}
