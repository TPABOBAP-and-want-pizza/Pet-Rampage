using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Sound : MonoBehaviourPun
{
    public AudioClip[] sounds;

    private AudioSource audioSrc => GetComponent<AudioSource>();
    public void PlaySound(AudioClip clip, float volume = 1f, bool destroyed = false, float p1 = 0.85f, float p2 = 1.2f) 
    {
        audioSrc.pitch = Random.Range(p1, p2);
        audioSrc.volume = volume;
        audioSrc.clip = clip;
        audioSrc.Play();
    }
}