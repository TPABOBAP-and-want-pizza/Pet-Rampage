using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if(canShoot)
        if (Input.GetMouseButton(0))
        {
            canShoot = false;
            Invoke("CanShootTrue", delay);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().Damage = damage;

            Vector2 shootDirection = new Vector2(transform.right.x, transform.right.y);
            bullet.GetComponent<Rigidbody2D>().velocity = -shootDirection.normalized * bulletSpeed;
        }
        //var hithandlers = target.GetComponents<IHitHandler>();
        //foreach (var i in hithandlers)
        //    i?.Handlehit(new HitInfo() { point = hit.point, force = force, direction = ray.direction, normalno = hit.normal });
    }
    private void CanShootTrue()
    {
        canShoot = true;
    }

}
