using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool IsNight { get; private set; }
    [SerializeField] private bool night;

    public void SetNightState(bool isNight)
    {
        IsNight = isNight;
        night = IsNight;
    }
}
