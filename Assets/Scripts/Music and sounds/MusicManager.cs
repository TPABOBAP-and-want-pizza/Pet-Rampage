using System.Collections;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    public AudioClip[] musicTracks;
    private AudioSource audioSource;
    private bool isMusicPlaying = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayMusic());
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator PlayMusic()
    {
        while (isMusicPlaying)
        {
            AudioClip randomTrack = musicTracks[Random.Range(0, musicTracks.Length)];
            audioSource.clip = randomTrack;
            audioSource.Play();
            yield return new WaitForSeconds(randomTrack.length);
        }
    }

    public void ToggleMusic(bool isMusicOn)
    {
        isMusicPlaying = isMusicOn;
        if (!isMusicOn)
        {
            audioSource.Stop();
        }
        else
        {
            StartCoroutine(PlayMusic());
        }
    }
}
