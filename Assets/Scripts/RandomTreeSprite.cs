using Photon.Pun;
using UnityEngine;

public class RandomTreeSprite : MonoBehaviourPunCallbacks, IPunObservable
{
    private SpriteRenderer treeSpriteRenderer;

    void Start()
    {
        treeSpriteRenderer = GetComponent<SpriteRenderer>();

        SyncTreeColor(); // ��������� ���� ����� ��� ���� �������
    }

    private void SyncTreeColor()
    {
        if (treeSpriteRenderer != null)
        {
            Sprite[] treeSprites = Resources.LoadAll<Sprite>("Grafic_Resources/Trees");

            if (treeSprites != null && treeSprites.Length > 0)
            {
                int randomIndex = Random.Range(0, treeSprites.Length);
                treeSpriteRenderer.sprite = treeSprites[randomIndex];

                // ���������� ���������� � ��������� ����� ������ ����� Photon
                photonView.RPC("SyncColorRPC", RpcTarget.AllBuffered, randomIndex);
            }
            else
            {
                Debug.LogError("No tree sprites found in the Grafic_Resources/Trees folder.");
            }
        }
        else
        {
            Debug.LogError("SpriteRenderer not found on the parent object.");
        }
    }

    [PunRPC]
    private void SyncColorRPC(int colorIndex)
    {
        Sprite[] treeSprites = Resources.LoadAll<Sprite>("Grafic_Resources/Trees");
        if (treeSprites != null && treeSprites.Length > colorIndex)
        {
            treeSpriteRenderer.sprite = treeSprites[colorIndex];
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // ���� �� ������ ���������� �������������� ������, �������� �� �����,
            // ����������� stream.SendNext() ��� ������ ���� ������.
        }
        else
        {
            // ���� �� ������ ������� �������������� ������, �������� �� �����,
            // ����������� stream.ReceiveNext() ��� ������ ���� ������.
        }
    }
}
