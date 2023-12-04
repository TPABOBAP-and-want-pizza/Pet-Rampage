using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private Sound sound;

    private void Start()
    {
        if (!sound.IsPlaying)
        {
            sound.PlayRandomSound();
        }
    }
}
