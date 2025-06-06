using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public GameObject taskPanel;
    public CameraController cameraController;
    public Slider musicSlider;

    private bool isPaused = false;

    private AudioSource musicAudioSource;

    void Start()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        taskPanel.SetActive(false);

        musicAudioSource = GameObject.FindWithTag("Game").GetComponent<AudioSource>();

        if (musicAudioSource != null)
            musicSlider.value = musicAudioSource.volume;

        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsPanel.activeSelf)
            {
                CloseSettings();
            }
            else
            {
                TogglePause();
            }
        }

        if (Input.GetKey(KeyCode.Tab))
        {
            taskPanel.SetActive(true);
        }
        else
        {
            taskPanel.SetActive(false);
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;
        cameraController.canLook = !isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
    }

    public void OnRestartClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnSettingsClicked()
    {
        settingsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    private void OnMusicVolumeChanged(float value)
    {
        if (musicAudioSource != null)
            musicAudioSource.volume = value;
    }

}
