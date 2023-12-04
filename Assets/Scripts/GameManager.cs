using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int day = 0;
    [SerializeField] private bool night;

    public static bool IsNight { get; private set; }
    public static int Day { get; set; } = 0;
    public static bool IsGameOver { get; set; } = false;

    private static GameManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            IsNight = night;
            Day = day;
        }
    }

    public void SetNightState(bool isNight)
    {
        IsNight = isNight;
        night = IsNight;
    }

    public void SetDay(int newDay)
    {
        Day = newDay;
        day = newDay;
    }

    public static void LiderboardWithDelay(float delay)
    {
        IsGameOver = true;
        Debug.Log("LiderboardWithDelay");
        instance.StartCoroutine(instance.LiderboardCoroutine(delay));
    }

    private IEnumerator LiderboardCoroutine(float delay)
    {
        Debug.Log("LiderboarCoroutine");
        yield return new WaitForSeconds(delay);
        Liderboard();
    }

    private void Liderboard()
    {
        Debug.Log("Liderboar");
        SceneManager.LoadScene("Liderboard");
    }
}
