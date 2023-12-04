using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSounds : MonoBehaviour
{
    [SerializeField] Sound sound;

    private void PlaySound()
    {
        sound.PlayRandomSound(0.2f);
    }
}
