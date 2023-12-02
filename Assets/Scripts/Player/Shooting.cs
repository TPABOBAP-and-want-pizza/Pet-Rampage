using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Shooting : MonoBehaviourPunCallbacks
{
    [SerializeField] int damage = 34;
    [SerializeField] float delay = 1f;
    private bool canShoot = true;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] ItemInfo bulletInfo;
    [SerializeField] int resursesCount = 1;
    private Inventory playerInventory;

    private void Start()
    {
        if (photonView.IsMine)
        {
            Player localPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            if (localPlayer != null)
            {
                playerInventory = localPlayer.inventory;

            }
            else
            {
                Debug.LogError("Local player inventory not found!");
            }
        }
    }
    private void Update()
    {
        if (photonView.IsMine)
        {
            CustRay();
        }
    }

    private void CustRay()
    {
        if (canShoot && Input.GetMouseButtonDown(0))
        {
            if (playerInventory.HasItem(bulletInfo, resursesCount))
            {
                canShoot = false;
                Invoke("CanShootTrue", delay);

                Vector2 shootDirection = new Vector2(transform.right.x, transform.right.y);
                GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, 
                    transform.GetChild(0).position, 
                    Quaternion.identity, 0, 
                    new object[] { shootDirection });

                bullet.GetComponent<Bullet>().photonView.RPC("SetBulletProperties", 
                    RpcTarget.AllBuffered, 
                    damage, 
                    shootDirection, 
                    photonView.ViewID);

                playerInventory.RemoveItem(bulletInfo, resursesCount);
            }
        }
    }
    private void CanShootTrue()
    {
        canShoot = true;
    }
}
