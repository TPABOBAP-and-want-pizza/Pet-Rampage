using System.Collections;
using UnityEngine;
using TMPro;

public class RespawnTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private SpawnPlayers spawnPlayers;

    public void StartTimer(float time)
    {
        StartCoroutine(CountdownTimer(time));
    }

    private IEnumerator CountdownTimer(float time)
    {
        float remainingTime = time;

        while (remainingTime > 0)
        {
            text.text = $"{Mathf.RoundToInt(remainingTime)}";
            yield return new WaitForSeconds(1f);
            remainingTime--;
        }
        spawnPlayers.SpawnPlayer();
    }
}
