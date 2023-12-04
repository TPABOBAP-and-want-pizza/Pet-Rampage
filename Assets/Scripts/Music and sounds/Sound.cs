using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Sound : MonoBehaviourPun
{
    public AudioClip[] sounds;

    public bool IsPlaying { get; set; } = false;
    private AudioSource audioSrc => GetComponent<AudioSource>();
    private Coroutine soundCoroutine;
    public void PlayRandomSound(float volume = 1f, bool destroyed = false, float p1 = 0.85f, float p2 = 1.2f)
    {
        if (!IsPlaying)
        {
            Debug.Log("PlaySound");
            IsPlaying = true;
            if(audioSrc == null)
            {
                Debug.Log("bruh");
                return;
            }
                
            audioSrc.pitch = Random.Range(p1, p2);
            audioSrc.volume = volume;
            audioSrc.clip = sounds[Random.Range(0, sounds.Length)];
            audioSrc.Play();

            soundCoroutine = StartCoroutine(WaitForSound());
        }
    }

    private IEnumerator WaitForSound()
    {
        yield return new WaitForSeconds(audioSrc.clip.length);

        IsPlaying = false;

        soundCoroutine = null;
    }
}
