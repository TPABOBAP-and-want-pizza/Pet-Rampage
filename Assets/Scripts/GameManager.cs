using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int day = 0;
    [SerializeField] private bool night;

    public static bool IsNight { get; private set; }
    public static int Day { get; set; } = 0;
    public void SetNightState(bool isNight)
    {
        IsNight = isNight;
        night = IsNight;
    }
    public void SetDay(int day)
    {
        Day = day;
        this.day = day;
    }
}
