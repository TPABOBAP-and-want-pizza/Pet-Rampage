using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SceneLoader : MonoBehaviourPunCallbacks
{
    public void LoadLobbyScene()
    {
        PhotonNetwork.LeaveRoom(); // ���������� �� �������

        // ������� ���������� � ������� � ������ ������ PUN

            PhotonNetwork.LeaveRoom(); // ���������� �� �������


                PhotonNetwork.Disconnect(); // ���������� �� ������� Photon

        // ����� ������ ��� ������ ������ Photon PUN (��������� ������� � Photon PUN 2.37)

        SceneManager.LoadScene("LoadindScreen", LoadSceneMode.Single); // ���������� ����� � ������ �������
        // ����� �������� SceneA, ��������� SceneB ������������� (Additive)
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);

        
    }
}