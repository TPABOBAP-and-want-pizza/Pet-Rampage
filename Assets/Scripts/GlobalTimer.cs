using Photon.Pun;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GlobalTimer : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    private Light2D globalLight;

    public float maxDayIntensity = 1f;
    public float maxNightIntensity = 0f;

    public Text timerText;

    private float dayDuration = 30f; // Длительность дня
    private float nightDuration = 20f; // Длительность ночи
    private bool isDayTime = true;
    private float timeOfDay = 0f; // Текущее время суток

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (isDayTime)
            {
                if (timeOfDay <= dayDuration / 2) // Половина дня
                {
                    float normalizedTime = timeOfDay / (dayDuration / 2);
                    float targetIntensity = Mathf.Lerp(maxNightIntensity, maxDayIntensity, normalizedTime);
                    globalLight.intensity = targetIntensity;
                }
                else
                {
                    globalLight.intensity = maxDayIntensity;
                }

                timeOfDay += Time.deltaTime;
                if (timeOfDay >= dayDuration)
                {
                    isDayTime = false;
                    timeOfDay = 0f;
                }
            }
            else
            {
                if (timeOfDay <= nightDuration / 2) // Половина ночи
                {
                    float normalizedTime = timeOfDay / (nightDuration / 2);
                    float targetIntensity = Mathf.Lerp(maxDayIntensity, maxNightIntensity, normalizedTime);
                    globalLight.intensity = targetIntensity;
                }
                else
                {
                    globalLight.intensity = maxNightIntensity;
                }

                timeOfDay += Time.deltaTime;
                if (timeOfDay >= nightDuration)
                {
                    isDayTime = true;
                    timeOfDay = 0f;
                }
            }

            UpdateTimerUI();

            photonView.RPC("SyncTimerAndLightIntensity", RpcTarget.Others, timeOfDay, globalLight.intensity, isDayTime);
        }
    }

    private void UpdateTimerUI()
    {
        string timeOfDayText = isDayTime ? "Day" : "Night";
        int minutes = Mathf.FloorToInt(timeOfDay / 60F);
        int seconds = Mathf.FloorToInt(timeOfDay - minutes * 60);
        string formatTime = string.Format("{0} {1:0}:{2:00}", timeOfDayText, minutes, seconds);
        timerText.text = formatTime;
    }

    [PunRPC]
    private void SyncTimerAndLightIntensity(float timeOfDayValue, float lightIntensity, bool isDay)
    {
        timeOfDay = timeOfDayValue;
        globalLight.intensity = lightIntensity;
        isDayTime = isDay;

        UpdateTimerUI();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Send data to other players
            stream.SendNext(timeOfDay);
            stream.SendNext(globalLight.intensity);
            stream.SendNext(isDayTime);
        }
        else
        {
            // Receive data from the network
            timeOfDay = (float)stream.ReceiveNext();
            globalLight.intensity = (float)stream.ReceiveNext();
            isDayTime = (bool)stream.ReceiveNext();

            UpdateTimerUI();
        }
    }
}
