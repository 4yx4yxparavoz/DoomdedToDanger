using UnityEngine;

public class GameMusicManager : MonoBehaviour
{
    void Start()
    {
        GetComponent<AudioSource>().Play();
    }
}
