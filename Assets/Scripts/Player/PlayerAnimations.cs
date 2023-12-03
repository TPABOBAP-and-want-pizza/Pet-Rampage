using Photon.Pun;
using UnityEngine;

public class PlayerAnimations : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private Animator animator;
    private int animNumber;

    private void Start()
    {
        if (photonView.IsMine)
        {
            animNumber = Random.Range(1, 5); // Генерация случайного числа для анимации от 1 до 4
            photonView.RPC("SetAnimationNumber", RpcTarget.AllBuffered, animNumber);
        }
    }

    [PunRPC]
    private void SetAnimationNumber(int number)
    {
        animNumber = number;
        animator.SetInteger("Anim_Number", animNumber);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(animNumber);
        }
        else
        {
            animNumber = (int)stream.ReceiveNext();
            animator.SetInteger("Anim_Number", animNumber);
        }
    }
}
