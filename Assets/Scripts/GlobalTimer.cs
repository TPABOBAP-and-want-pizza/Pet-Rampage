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

    private float dayDuration = 30f; // ������������ ���
    private float nightDuration = 20f; // ������������ ����
    private bool isDayTime = true;
    private float timeOfDay = 30f; // ��������� �������� �������

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            float normalizedTime;
            if (isDayTime)
            {
                if (timeOfDay > 15f) // ���� ����� ����� �������� �� 30 �� 15
                {
                    normalizedTime = Mathf.Lerp(0.5f, 1f, 1f - ((timeOfDay - 15f) / 15f));
                }
                else // ���� ����� ����� �������� �� 15 �� 0
                {
                    normalizedTime = Mathf.Lerp(1f, 0.5f, 1f - (timeOfDay / 15f));
                }
            }
            else // ��� ����
            {
                if (timeOfDay > 10f) // ���� ����� ����� �������� �� 20 �� 10
                {
                    normalizedTime = Mathf.Lerp(0.5f, 0f, 1f - ((timeOfDay - 10f) / 10f));
                }
                else // ���� ����� ����� �������� �� 10 �� 0
                {
                    normalizedTime = Mathf.Lerp(0f, 0.5f, 1f - (timeOfDay / 10f));
                }
            }

            float targetIntensity = Mathf.Lerp(maxNightIntensity, maxDayIntensity, normalizedTime);
            globalLight.intensity = targetIntensity;

            timeOfDay -= Time.deltaTime;

            if (timeOfDay <= 0f)
            {
                isDayTime = !isDayTime;
                timeOfDay = isDayTime ? dayDuration : nightDuration;
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
