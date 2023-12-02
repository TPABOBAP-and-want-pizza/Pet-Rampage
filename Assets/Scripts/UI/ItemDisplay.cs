using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite cell_image;
    [SerializeField] private TMP_Text text;


    public void DisplayItem(ItemInfo info, int count = 0)
    {
        Sprite sprite = count > 0 ? info.sprite : cell_image;
        string temp = count > 0 ? count.ToString() : "";
        ShowInfo(sprite, temp, info.stackable);
    }

    private void ShowInfo(Sprite sprite, string str, bool showText)
    {
        image.sprite = sprite;
        if (showText)
            text.text = str;
    }

    // Установка масштаба элемента
    public void SetScale(Vector3 scale)
    {
        transform.localScale = scale;
    }
}

