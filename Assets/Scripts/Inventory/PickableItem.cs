using UnityEngine;

public class PickableItem : MonoBehaviour
{
    [SerializeField] private ItemInfo item;
    [SerializeField] private int count;
    public ItemInfo Item => item;
    public int Count => count;

}
