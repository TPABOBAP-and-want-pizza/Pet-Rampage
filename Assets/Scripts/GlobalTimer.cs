using Photon.Pun;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class GlobalTimer : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField]
    private Light2D globalLight;
    [SerializeField] GameManager manager;

    public float maxDayIntensity = 1f;
    public float maxNightIntensity = 0f;

    public Text dayCounterText;
    public Text timerText;

    private float dayDuration = 10f; // Длительность дня
    private float nightDuration = 5f; // Длительность ночи
    private bool isDayTime = true;
    private float timeOfDay = 10f; // Начальное значение времени
    public bool IsDayTime => isDayTime;
    private int daysPassed = 0;

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            float normalizedTime;
            if (isDayTime)
            {
                if (timeOfDay > 15f) // Если время суток меняется от 30 до 15
                {
                    normalizedTime = Mathf.Lerp(0.5f, 1f, 1f - ((timeOfDay - 15f) / 15f));
                }
                else // Если время суток меняется от 15 до 0
                {
                    normalizedTime = Mathf.Lerp(1f, 0.5f, 1f - (timeOfDay / 15f));
                }


                
                UpdateTimerUI();
                photonView.RPC("SyncTimerAndLightIntensity", RpcTarget.Others, timeOfDay, globalLight.intensity, isDayTime, daysPassed);

            }
            else // Для ночи
            {
                if (timeOfDay > 10f) // Если время суток меняется от 20 до 10
                {
                    normalizedTime = Mathf.Lerp(0.5f, 0f, 1f - ((timeOfDay - 10f) / 10f));
                }
                else // Если время суток меняется от 10 до 0
                {
                    normalizedTime = Mathf.Lerp(0f, 0.5f, 1f - (timeOfDay / 10f));
                }
                if (timeOfDay <= 1f)
                {

                }
            }

            float targetIntensity = Mathf.Lerp(maxNightIntensity, maxDayIntensity, normalizedTime);
            globalLight.intensity = targetIntensity;

            timeOfDay -= Time.deltaTime;

            if (timeOfDay <= 1f)
            {
                isDayTime = !isDayTime;
                manager.SetNightState(!isDayTime);
                timeOfDay = isDayTime ? dayDuration : nightDuration;

                if(isDayTime)
                {
                    daysPassed++;
                    UpdateDayCounter();
                }

            }

            UpdateTimerUI();

            photonView.RPC("SyncTimerAndLightIntensity", RpcTarget.Others, timeOfDay, globalLight.intensity, isDayTime, daysPassed);
        }
    }

    private void UpdateTimerUI()
    {
        string timeOfDayText = isDayTime ? "Day" : "Night";
        int minutes = Mathf.FloorToInt(timeOfDay / 60F);
        int seconds = Mathf.FloorToInt(timeOfDay - minutes * 60);
        string formatTime = string.Format("{0} {1:0}:{2:00}", timeOfDayText, minutes, seconds);
        timerText.text = formatTime;
        GameManager.Day = daysPassed;

    }

    [PunRPC]
    private void SyncTimerAndLightIntensity(float timeOfDayValue, float lightIntensity, bool isDay, int days)
    {
        timeOfDay = timeOfDayValue;
        globalLight.intensity = lightIntensity;
        isDayTime = isDay;
        daysPassed = days; // Обновляем значение daysPassed при получении данных от других игроков

        UpdateTimerUI();
        UpdateDayCounter();

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Send data to other players
            stream.SendNext(timeOfDay);
            stream.SendNext(globalLight.intensity);
            stream.SendNext(isDayTime);
            stream.SendNext(daysPassed); // Добавляем daysPassed в поток для передачи информации другим игрокам
        }
        else
        {
            // Receive data from the network
            timeOfDay = (float)stream.ReceiveNext();
            globalLight.intensity = (float)stream.ReceiveNext();
            isDayTime = (bool)stream.ReceiveNext();
            daysPassed = (int)stream.ReceiveNext(); // Принимаем информацию о daysPassed от других игроков

            UpdateTimerUI();
            UpdateDayCounter();
        }
    }

    private void UpdateDayCounter()
    {
        if (dayCounterText != null)
        {
            dayCounterText.text = "Day: " + daysPassed.ToString();
        }
    }

}
