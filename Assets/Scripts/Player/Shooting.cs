using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shooting : MonoBehaviourPunCallbacks
{
    [SerializeField] int damage = 34;
    [SerializeField] float delay = 1f;
    [SerializeField] bool canShoot = true;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] GameObject bulletPrefab;

    private void Update()
    {
        if (photonView.IsMine)
        {
            CustRay();
        }
    }

    private void CustRay()
    {
        Ray2D ray = new Ray2D(transform.position, Input.mousePosition);
        Debug.DrawRay(transform.position, Input.mousePosition, Color.red);

        if (canShoot && Input.GetMouseButton(0))
        {
            canShoot = false;
            Invoke("CanShootTrue", delay);

            Vector2 shootDirection = new Vector2(transform.right.x, transform.right.y);
            GameObject bullet = PhotonNetwork.Instantiate("Bullet", transform.GetChild(0).position, Quaternion.identity, 0, new object[] { shootDirection });

            bullet.GetComponent<Bullet>().photonView.RPC("SetBulletProperties", RpcTarget.AllBuffered, damage, shootDirection);
        }
    }
    private void CanShootTrue()
    {
        canShoot = true;
    }
}
