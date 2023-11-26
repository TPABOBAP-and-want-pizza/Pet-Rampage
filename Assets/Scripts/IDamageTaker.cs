using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
[PunRPC]
public interface IDamageTaker
{
    public void TakeDamage(int damage);
}
