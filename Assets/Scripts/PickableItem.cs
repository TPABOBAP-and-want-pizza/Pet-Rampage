using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    [SerializeField] GameObject item;
    [SerializeField] string name;
    [SerializeField] int id = -1;
    [SerializeField] bool stackable = false;
    [SerializeField] int maxCount = 64;
    [SerializeField] bool placeable = false;
}
