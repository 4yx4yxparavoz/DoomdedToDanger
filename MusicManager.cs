using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioClip[] musicTracks;
    private AudioSource audioSource;
    private int currentTrackIndex = 0;
    private bool gameEnded = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // музыка не пропадает при смене сцен
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        PlayNextTrack();
    }

    void Update()
    {
        if (!audioSource.isPlaying && !gameEnded)
        {
            PlayNextTrack();
        }
    }

    void PlayNextTrack()
    {
        if (musicTracks.Length == 0) return;

        currentTrackIndex = (currentTrackIndex + 1) % musicTracks.Length;
        audioSource.clip = musicTracks[currentTrackIndex];
        audioSource.Play();
    }

    public void StopMusic()
    {
        gameEnded = true;
        audioSource.Stop();
    }
}
