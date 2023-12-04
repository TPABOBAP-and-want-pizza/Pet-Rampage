using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class SceneLoader : MonoBehaviourPunCallbacks
{
    public void LoadLobbyScene()
    {
        PhotonNetwork.LeaveRoom(); // Отключение от комнаты

        // Очистка информации о комнате и других данных PUN

            PhotonNetwork.LeaveRoom(); // Отключение от комнаты


                PhotonNetwork.Disconnect(); // Отключение от сервера Photon

        // Новые методы для сброса данных Photon PUN (доступные начиная с Photon PUN 2.37)

        SceneManager.LoadScene("LoadindScreen", LoadSceneMode.Single); // Указывайте сцены в нужном порядке
        // После загрузки SceneA, загружаем SceneB дополнительно (Additive)
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);

        
    }
}