using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] musicTracks;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayMusic());
    }

    private IEnumerator PlayMusic()
    {
        while (true)
        {
            AudioClip randomTrack = musicTracks[Random.Range(0, musicTracks.Length)];

            audioSource.clip = randomTrack;

            audioSource.Play();
            yield return new WaitForSeconds(randomTrack.length);
        }
    }
}
