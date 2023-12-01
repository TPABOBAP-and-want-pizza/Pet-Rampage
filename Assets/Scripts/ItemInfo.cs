using UnityEngine;
[CreateAssetMenu(fileName = "EmptyItem", menuName = "ScriptableObject/ItemConfig", order = 0)]
public class ItemInfo:ScriptableObject
{
    public string name;
    public int id;
    public Sprite sprite;
    public bool stackable;
    public int maxCount;
    public bool placeable;
}
