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

    private float dayDuration = 120f; // ������������ ���
    private float nightDuration = 30f; // ������������ ����
    private bool isDayTime = true;
    private float timeOfDay = 120f; // ��������� �������� �������
    public bool IsDayTime => isDayTime;
    private int daysPassed = 0;

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            float normalizedTime;
            if (isDayTime)
            {
                if (timeOfDay > dayDuration/2) // ���� ����� ����� �������� �� 30 �� 15
                {
                    normalizedTime = Mathf.Lerp(0.5f, 1f, 1f - ((timeOfDay - dayDuration / 2) / (dayDuration / 2)));
                }
                else // ���� ����� ����� �������� �� 15 �� 0
                {
                    normalizedTime = Mathf.Lerp(1f, 0.5f, 1f - (timeOfDay / (dayDuration / 2)));
                }


                
                UpdateTimerUI();
                photonView.RPC("SyncTimerAndLightIntensity", RpcTarget.Others, timeOfDay, globalLight.intensity, isDayTime, daysPassed);

            }
            else // ��� ����
            {
                if (timeOfDay > 10f) // ���� ����� ����� �������� �� 20 �� 10
                {
                    normalizedTime = Mathf.Lerp(0.5f, 0f, 1f - ((timeOfDay - (nightDuration/2)) / (nightDuration / 2)));
                }
                else // ���� ����� ����� �������� �� 10 �� 0
                {
                    normalizedTime = Mathf.Lerp(0f, 0.5f, 1f - (timeOfDay / (nightDuration / 2)));
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
        daysPassed = days; // ��������� �������� daysPassed ��� ��������� ������ �� ������ �������

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
            stream.SendNext(daysPassed); // ��������� daysPassed � ����� ��� �������� ���������� ������ �������
        }
        else
        {
            // Receive data from the network
            timeOfDay = (float)stream.ReceiveNext();
            globalLight.intensity = (float)stream.ReceiveNext();
            isDayTime = (bool)stream.ReceiveNext();
            daysPassed = (int)stream.ReceiveNext(); // ��������� ���������� � daysPassed �� ������ �������

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
