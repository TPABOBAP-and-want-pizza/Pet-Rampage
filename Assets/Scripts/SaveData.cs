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
            // ѕолучаем список игроков в комнате
            Photon.Realtime.Player[] players = PhotonNetwork.PlayerList;

            string allPlayerNames = "";

            if (players.Length == 1)
            {
                // ≈сли в комнате только один игрок, устанавливаем его им€
                allPlayerNames = players[0].NickName;
            }
            else
            {
                // ≈сли в комнате несколько игроков, перебираем их имена и добавл€ем их к строке
                foreach (Photon.Realtime.Player player in players)
                {
                    allPlayerNames += player.NickName + " ";
                }
            }

            // ”станавливаем полученные имена игроков в myName
            myName.text = allPlayerNames;
        }
    }

    void Update()
    {
        // »спользуем currentScore вместо PlayerPrefs.GetInt("highscore")
        myScore.text = $"YOUR DAYS SCORE:  {currentScore}";
    }


    public void SendScore()
    {

        PlayerPrefs.SetInt("highscore", currentScore);
        HighScores.UploadScore(myName.text, currentScore);

    }
    void OnEnable()
    {
        // ѕолучаем сохраненное значение текущего дн€ из GameManager и присваиваем его currentScore
        currentScore = GameManager.CurrentDayScore;
    }
}
