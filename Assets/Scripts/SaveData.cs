using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SaveData : MonoBehaviour
{
    public TMPro.TextMeshProUGUI myScore;
    public TMPro.TextMeshProUGUI myName;
    public int currentScore;

    void Start()
    {
        if (PhotonNetwork.InRoom)
        {
            // Получаем список игроков в комнате
            Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;

            string allPlayerNames = "";
            foreach (Photon.Realtime.Player player in players)
            {
                allPlayerNames += player.NickName + " ";
            }

            // Устанавливаем полученные имена игроков в myName
            myName.text = allPlayerNames;
        }
    }
    void Update()
    {
        // Используем currentScore вместо PlayerPrefs.GetInt("highscore")
        myScore.text = $"YOUR DAYS SCORE:  {currentScore}";
    }


    public void SendScore()
    {

            PlayerPrefs.SetInt("highscore", currentScore);
            HighScores.UploadScore(myName.text, currentScore);
        
    }
    void OnEnable()
    {
        // Получаем сохраненное значение текущего дня из GameManager и присваиваем его currentScore
        currentScore = GameManager.CurrentDayScore;
    }
}








