using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shooting : MonoBehaviour
{
    [SerializeField] float force = 100f;
    [SerializeField] int damage = 34;
    [SerializeField] float delay = 1f;
    [SerializeField] bool canShoot = true;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] GameObject bulletPrefab;

    void Update()
    {
        CustRay();
    }

    private void CustRay()
    {
        Ray2D ray = new Ray2D(transform.position, Input.mousePosition);
        Debug.DrawRay(transform.position, Input.mousePosition, Color.red);

        if (canShoot && Input.GetMouseButtonDown(0))
        {
            canShoot = false;
            Invoke("CanShootTrue", delay);

            // Создание пули через сеть с помощью PhotonNetwork.Instantiate
            GameObject bullet = PhotonNetwork.Instantiate("Bullet", transform.position, Quaternion.identity);

            bullet.GetComponent<Bullet>().Damage = damage;

            Vector2 shootDirection = new Vector2(transform.right.x, transform.right.y);
            bullet.GetComponent<Rigidbody2D>().velocity = -shootDirection.normalized * bulletSpeed;
        }
        //bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(
        //    transform.forward.x + transform.rotation.z,
        //    transform.forward.y + transform.rotation.z) * bulletSpeed;
    
        //var hithandlers = target.GetComponents<IHitHandler>();
        //foreach (var i in hithandlers)
        //    i?.Handlehit(new HitInfo() { point = hit.point, force = force, direction = ray.direction, normalno = hit.normal });



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
    private void CanShootTrue()
    {
        canShoot = true;
    }

}
