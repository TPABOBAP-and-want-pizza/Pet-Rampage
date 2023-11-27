using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<PickableItem> item = new List<PickableItem>();
    [SerializeField] Transform inventory;
    void Start()
    {
        int countParentofCells = inventory.childCount;
        for(int i = 0; i < countParentofCells; i++)
        {
            Transform cellParent = inventory.GetChild(i);
            if(cellParent.tag == "Cell")
            {
                int count = cellParent.childCount;
                for(int j = 0; j < count; j++)
                {
                    PickableItem temp = cellParent.GetChild(j).GetComponent<PickableItem>();
                    if(temp!= null)
                    {
                        item.Add(temp);
                    }
                }
            }
        }
    }


    void Update()
    {
        
    }
}
