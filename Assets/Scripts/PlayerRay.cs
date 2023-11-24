using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRay : MonoBehaviour
{
    [SerializeField] float force = 100;
    [SerializeField] Transform camTrans;

    void Update()
    {
        CustRay();
    }

    private void CustRay()
    {
        Ray ray = new Ray(camTrans.position, camTrans.forward);
        Debug.DrawRay(camTrans.position, camTrans.forward*100, Color.red);

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            GameObject target = hit.collider.gameObject;
            if (Input.GetMouseButtonDown(0))
            {
                var hithandlers = target.GetComponents<IHitHandler>();
                foreach(var i in hithandlers)
                    i?.Handlehit(new HitInfo() { point = hit.point, force = force, direction = ray.direction, normalno = hit.normal });
            }
            //if (currentSelected.gameObject && currentSelected.gameObject != target)
            //{
            //    currentSelected.select = false;
            //    currentSelected.CheckColor();
            //    currentSelected.gameObject = null;
            //}
            //if (target.tag == "Target")
            //{
            //    currentSelected.gameObject = target;
            //    currentSelected.select = true;
            //    currentSelected.CheckColor();
            //    //Debug.Log("Boo");
            //}
        }
    }
}
