using UnityEngine;

public class MenuMusicManager : MonoBehaviour
{
    private static MenuMusicManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); 
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    public void StopMusic()
    {
        GetComponent<AudioSource>().Stop();
        Destroy(gameObject); 
    }
}
