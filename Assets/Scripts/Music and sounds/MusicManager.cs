using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] musicTracks;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Перевірка, чи призначено які-небудь музичні треки
        if (musicTracks.Length > 0)
        {
            StartCoroutine(PlayMusic());
        }
        else
        {
            Debug.LogError("no music for MusicManager.");
        }
    }

    private IEnumerator PlayMusic()
    {
        while (true)
        {
            AudioClip randomTrack = musicTracks[Random.Range(0, musicTracks.Length)];

            audioSource.clip = randomTrack;
            audioSource.Play();

            Debug.Log($"randomTrack.length = {randomTrack.length}");
            yield return new WaitForSeconds(randomTrack.length);
        }
    }
}