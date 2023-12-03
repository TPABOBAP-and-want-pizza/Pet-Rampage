using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GlobalTimer : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    private Light2D globalLight;

    private float dayTimer = 30f; // 3 минуты
    private float nightTimer = 20f; // 1.5 минуты
    private bool isDayTime = true;
    private float updateInterval = 1.0f; // »нтервал обновлени€ таймера
    public float dayIntensity = 1f;
    public float nightIntensity = 0f;

    public Text timerText;

    private float timer;

    private void Start()
    {
        timer = updateInterval;
    }

    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = updateInterval;

            if (isDayTime)
            {
                dayTimer -= updateInterval;
                if (dayTimer <= 0)
                {
                    isDayTime = false;
                    dayTimer = 0;
                    nightTimer = 20f; // —бросить таймер ночи при начале дн€
                    UpdateLightIntensity();
                }
            }
            else
            {
                nightTimer -= updateInterval;
                if (nightTimer <= 0)
                {
                    isDayTime = true;
                    nightTimer = 0;
                    dayTimer = 30f; // —бросить таймер дн€ при начале ночи
                    UpdateLightIntensity();
                }
            }

            if (isDayTime)
            {
                UpdateTimerUI("Day", dayTimer);
            }
            else
            {
                UpdateTimerUI("Night", nightTimer);
            }
        }
    }

    private void UpdateTimerUI(string timeOfDay, float timer)
    {
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        string formatTime = string.Format("{0} {1:0}:{2:00}", timeOfDay, minutes, seconds);
        timerText.text = formatTime;

        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("SyncTimer", RpcTarget.Others, timeOfDay, timer);
        }
    }

    [PunRPC]
    private void SyncTimer(string timeOfDay, float timer)
    {
        UpdateTimerUI(timeOfDay, timer);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // Add serialization logic here if necessary
    }
    private void UpdateLightIntensity()
    {
        globalLight.intensity = isDayTime ? dayIntensity : nightIntensity;
    }
}