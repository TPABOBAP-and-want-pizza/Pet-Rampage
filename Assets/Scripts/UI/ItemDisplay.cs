using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text text;

    public void DisplayItem(ItemInfo info, int count = 0)
    {
        Sprite sprite = info != null ? info.sprite : null;
        string temp = count > 0 ? count.ToString() : "";
        ShowInfo(sprite, temp);
    }

    private void ShowInfo(Sprite sprite, string str)
    {
        image.sprite = sprite;
        text.text = str;
    }
}
