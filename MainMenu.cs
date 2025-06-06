using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;

    public Slider effectsSlider;

    private AudioSource effectsSource;
    void Start()
    {
        settingsPanel.SetActive(false);

        effectsSource = GameObject.FindGameObjectWithTag("Menu")?.GetComponent<AudioSource>();

        if (effectsSource != null)
            effectsSlider.value = effectsSource.volume;

        effectsSlider.onValueChanged.AddListener(SetEffectsVolume);
    }

    public void PlayGame()
    {
        MenuMusicManager music = FindObjectOfType<MenuMusicManager>();
        if (music != null)
        {
            music.StopMusic();
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void SetEffectsVolume(float value)
    {
        if (effectsSource != null)
            effectsSource.volume = value;
    }
}
